using Escendit.Orleans.Clients.RabbitMQ.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Orleans.Runtime;
using RabbitMQ.Stream.Client;
using Xunit.Categories;

namespace Escendit.Orleans.Clients.RabbitMQ.StreamProtocol.Tests;

/// <summary>
/// Host Builder Tests.
/// </summary>
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
    public void Test_GetRequiredService_StreamSystem_Default_FromConfigSectionPath()
    {
        var host = _hostBuilder
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json");
            })
            .AddRabbitMqStreamSystemAsDefault("Path")
            .Build();
        
        Assert.NotNull(host);

        var streamSystem = host.Services.GetRequiredServiceByName<StreamSystem>(ConnectionOptions.DefaultKey);
        
        Assert.NotNull(streamSystem);
    }

    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_StreamSystem_Default_FromOptions()
    {
        var host = _hostBuilder
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json");
            })
            .AddRabbitMqStreamSystemAsDefault(options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost" });
                options.UserName = "guest";
                options.Password = "guest";
                options.VirtualHost = "/";
            })
            .Build();
        
        Assert.NotNull(host);

        var streamSystem = host.Services.GetRequiredServiceByName<StreamSystem>(ConnectionOptions.DefaultKey);
        
        Assert.NotNull(streamSystem);
    }
    
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_StreamSystem_Default_FromOptionsBuilder()
    {
        var host = _hostBuilder
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json");
            })
            .AddRabbitMqStreamSystemAsDefault(options => options.BindConfiguration("Path"))
            .Build();
        
        Assert.NotNull(host);

        var streamSystem = host.Services.GetRequiredServiceByName<StreamSystem>(ConnectionOptions.DefaultKey);
        
        Assert.NotNull(streamSystem);
    }

    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_StreamSystem_Test_FromConfigSectionPath()
    {
        var host = _hostBuilder
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json");
            })
            .AddRabbitMqStreamSystem("test", "Path")
            .Build();
        
        Assert.NotNull(host);

        var streamSystem = host.Services.GetRequiredServiceByName<StreamSystem>("test");
        
        Assert.NotNull(streamSystem);
    }

    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_StreamSystem_Test_FromOptions()
    {
        var host = _hostBuilder
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json");
            })
            .AddRabbitMqStreamSystem("test", options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost" });
                options.UserName = "guest";
                options.Password = "guest";
                options.VirtualHost = "/";
            })
            .Build();
        
        Assert.NotNull(host);

        var streamSystem = host.Services.GetRequiredServiceByName<StreamSystem>("test");
        
        Assert.NotNull(streamSystem);
    }
    
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_StreamSystem_Test_FromOptionsBuilder()
    {
        var host = _hostBuilder
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json");
            })
            .AddRabbitMqStreamSystem("test", options => options.BindConfiguration("Path"))
            .Build();
        
        Assert.NotNull(host);

        var streamSystem = host.Services.GetRequiredServiceByName<StreamSystem>("test");
        
        Assert.NotNull(streamSystem);
    }
}
