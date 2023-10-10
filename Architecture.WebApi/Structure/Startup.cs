using Microsoft.AspNetCore.Mvc;
using Architecture.Application.Core.Structure;
using Microsoft.OpenApi.Models;
using Architecture.WebApi.Structure.Filters;
using Architecture.Infra.IoC;

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
        services.AddMvc(options =>
        {
            //options.Filters.Add<ValidationFilter>();
            // options.Filters.Add<NotificationFilter>();
        });

        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        // Binding model 
        services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);

        Configuration.Bind(appSettings);

        /// Learn more about configuring  https://stackoverflow.com/questions/59774566/what-do-the-size-settings-for-memorycache-mean
        services.AddMemoryCache((options) => 
        {
            options.SizeLimit = 1024 * 1024;
        });

        services.AddSingleton<AppSettings, AppSettings>();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Architeture.WebAPI", Version = "v1" });
        });

        services.AddInfraStructure(appSettings);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

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
        });

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}