using Microsoft.AspNetCore.Mvc;
using Architecture.Application.Core.Structure;
using Microsoft.OpenApi.Models;
using Architecture.Infra.IoC;
using Architecture.WebApi.Structure.Extensions;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Architecture.WebApi.Structure.Middlewares;
using Architecture.WebApi.Endpoints;
using Architecture.WebApi.Structure.Filters;
using Microsoft.AspNetCore.Routing;

namespace Architecture.WebApi.Structure;

public class Startup
{
    protected AppSettings appSettings { get; set; }

    public Startup(IConfiguration configuration)


    {
        Configuration = configuration;
        appSettings = new AppSettings();
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvcCore();

        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();


        // Binding model 
        services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);

        Configuration.Bind(appSettings);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
          .AddJwtBearer(options =>
          {
              options.RequireHttpsMetadata = false;
              options.SaveToken = true;
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = false,
                  ValidateAudience = false,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Jwt.Key)),
                  ClockSkew = TimeSpan.Zero,
              };
          });

        /// Learn more about configuring  https://stackoverflow.com/questions/59774566/what-do-the-size-settings-for-memorycache-mean
        services.AddMemoryCache((options) => 
        {
            //options.SizeLimit = 1024 * 1024;
        });

        services.AddAntiforgery();

        services.AddSingleton(appSettings);

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Architecture.WebAPI", Version = "v1" });

           c.RegisterSwaggerDefaultConfig(true, appSettings.Swagger.FlowLogin);

            //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), includeControllerXmlComments: false);
        });

        services.AddSwaggerExamples();

        services.AddInfraStructure(appSettings);

        services.AddAuthorizationBuilder()
            .AddPolicy("admin", policy => policy.RequireRole("admin"));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseMiddleware<RequestResponseLoggingMiddleware>();

        app.UseMiddleware<AuthUnauthorizedMiddleware>();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseSwagger();
        // subir no local host 
        app.UseSwaggerUI(c =>
        {
            c.OAuthClientId("7064bbbf-5d11-4782-9009-95e5a6fd6822");
            c.OAuthClientSecret("dff0bcb8dad7ea803e8d28bf566bcd354b5ec4e96ff4576a1b71ec4a21d56910");
            c.OAuthUsername("lcseverton");
        });

        app.UseAuthentication();

        app.UseAuthorization();
        app.UseAntiforgery();

        app.UseEndpoints(endpoints =>
        {
            var endpointv1 = endpoints.MapGroup("api/v1/")
                .AddEndpointFilter<ValidationFilter>()
                .WithOpenApi();

            endpointv1
                .AddAuthEndpoints("auth/", "Auth");
                //.AddPessoasEndpoints("pessoas/", "Pessoas")
                //.AddUsuariosEndpoint("usuarios/", "Usuarios");
        });
    }
}
