using Architecture.Application.Core.Structure;
using Architecture.Infra.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Architecture.Tests.Structure;

public class BaseTest
{
    public ServiceProvider _serviceProvider { get; private set; }

    public BaseTest()
    {
        var serviceCollection = new ServiceCollection();

        var appSettings = new AppSettings()
        {
            ConnectionStrings = new Connectionstrings()
            {
                SqlConnection = "Data Source=NOTEBOOK\\SQLEXPRESS;Initial Catalog=Architecture;User ID=sa;Password=12345;Integrated Security=True;TrustServerCertificate=True"
            },
            MemoryCache = new Memorycache()
            {
                AbsoluteExpirationInMinutes = 60,
                SlidingExpirationInMinutes = 60,
            },
            Jwt = new Jwt()
            {
                ExpireInMinutes = 60,
                Key = "MOCK_KEY_TESTE"
            },
            Logging = new Logging()
            {
                LogLevel = new Loglevel
                {
                    Default = "Warning",
                    MicrosoftAspNetCore = "Warning"
                }
            },
            Swagger = new Swagger()
            {
                FlowLogin = ""
            }
        };

        serviceCollection.AddInfraStructure(appSettings);

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }
}
