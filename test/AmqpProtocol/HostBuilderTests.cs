using Escendit.Orleans.Clients.RabbitMQ.Abstractions;
using Escendit.Orleans.Clients.RabbitMQ.AmqpProtocol;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Orleans.Runtime;
using RabbitMQ.Client;
using Xunit.Categories;

namespace AmqpProtocol;

public class HostBuilderTests
{
    private readonly IHostBuilder _hostBuilder;
    
    public HostBuilderTests()
    {
        _hostBuilder = Host
            .CreateDefaultBuilder()
            .ConfigureServices(services => services
                .TryAddSingleton(typeof(IKeyedServiceCollection<,>), typeof(KeyedServiceCollection<,>)));
    }
    
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_ConnectionFactory_Default_FromConfigSectionPath()
    {
        var host = _hostBuilder
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json");
            })
            .AddRabbitMqConnectionFactory(ConnectionOptions.DefaultKey, "Path")
            .Build();
        
        Assert.NotNull(host);

        var connectionFactory = host.Services.GetRequiredServiceByName<IConnectionFactory>(ConnectionOptions.DefaultKey);
        
        Assert.NotNull(connectionFactory);
    }

    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_ConnectionFactory_Default_FromOptions()
    {
        var host = _hostBuilder
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json");
            })
            .AddRabbitMqConnectionFactory(ConnectionOptions.DefaultKey, options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost" });
                options.UserName = "guest";
                options.Password = "guest";
                options.VirtualHost = "/";
            })
            .Build();
        
        Assert.NotNull(host);

        var connectionFactory = host.Services.GetRequiredServiceByName<IConnectionFactory>(ConnectionOptions.DefaultKey);
        
        Assert.NotNull(connectionFactory);
    }

    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_ConnectionFactory_Default_FromOptionsBuilder()
    {
        var host = _hostBuilder
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json");
            })
            .AddRabbitMqConnectionFactory(ConnectionOptions.DefaultKey, options => options.BindConfiguration("Path"))
            .Build();
        
        Assert.NotNull(host);

        var connectionFactory = host.Services.GetRequiredServiceByName<IConnectionFactory>(ConnectionOptions.DefaultKey);
        
        Assert.NotNull(connectionFactory);
    }

    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_ConnectionFactory_Default_FromOption_Test()
    {
        var host = _hostBuilder
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json");
            })
            .AddRabbitMqConnectionOptionsAsDefault("Path")
            .AddRabbitMqConnectionFactoryFromOption("test", ConnectionOptions.DefaultKey)
            .Build();
        
        Assert.NotNull(host);

        var connectionFactory = host.Services.GetRequiredServiceByName<IConnectionFactory>("test");
        
        Assert.NotNull(connectionFactory);
    }
    
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_Connection_Default_FromConfigSectionPath()
    {
        var host = _hostBuilder
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json");
            })
            .AddRabbitMqConnection(ConnectionOptions.DefaultKey, "Path")
            .Build();
        
        Assert.NotNull(host);

        var connection = host.Services.GetRequiredServiceByName<IConnection>(ConnectionOptions.DefaultKey);
        
        Assert.NotNull(connection);
    }
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_Connection_Default_FromOptions()
    {
        var host = _hostBuilder
            .AddRabbitMqConnection(ConnectionOptions.DefaultKey, options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost" });
                options.UserName = "guest";
                options.Password = "guest";
                options.VirtualHost = "/";
            })
            .Build();
        
        Assert.NotNull(host);

        var connection = host.Services.GetRequiredServiceByName<IConnection>(ConnectionOptions.DefaultKey);
        
        Assert.NotNull(connection);
    }
 
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_Connection_Default_FromOptionsBuilder()
    {
        var host = _hostBuilder
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json");
            })
            .AddRabbitMqConnection(ConnectionOptions.DefaultKey, options => options.BindConfiguration("Path"))
            .Build();
        
        Assert.NotNull(host);

        var connection = host.Services.GetRequiredServiceByName<IConnection>(ConnectionOptions.DefaultKey);
        
        Assert.NotNull(connection);
    }
    
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_Connection_Test_FromFactory_Test()
    {
        var host = _hostBuilder
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json");
            })
            .AddRabbitMqConnectionOptionsAsDefault("Path")
            .AddRabbitMqConnectionFactoryFromOption("test", ConnectionOptions.DefaultKey)
            .AddRabbitMqConnectionFromFactory("test", "test")
            .Build();
        
        Assert.NotNull(host);

        var connection = host.Services.GetRequiredServiceByName<IConnection>("test");
        
        Assert.NotNull(connection);
    }
}
