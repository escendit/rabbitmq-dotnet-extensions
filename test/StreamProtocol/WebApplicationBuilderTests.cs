using Escendit.Orleans.Clients.RabbitMQ.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Orleans.Runtime;
using RabbitMQ.Stream.Client;
using Xunit.Categories;

namespace Escendit.Orleans.Clients.RabbitMQ.StreamProtocol.Tests;

/// <summary>
/// Web Application Builder Tests.
/// </summary>
public class WebApplicationBuilderTests
{
    private readonly WebApplicationBuilder _webApplicationBuilder;
    
    public WebApplicationBuilderTests()
    {
        _webApplicationBuilder = WebApplication
            .CreateBuilder();
            
        _webApplicationBuilder
            .Services
            .TryAddSingleton(typeof(IKeyedServiceCollection<,>), typeof(KeyedServiceCollection<,>));
        
        _webApplicationBuilder
            .Configuration
            .AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json");
    }
    
    [Fact]
    [IntegrationTest]
    public void Test_WAB_GetRequiredService_StreamSystem_Default_FromConfigSectionPath()
    {
        var webApplication = _webApplicationBuilder
            .AddRabbitMqStreamSystem(ConnectionOptions.DefaultKey, "Path")
            .Build();
        
        Assert.NotNull(webApplication);

        var streamSystem = webApplication.Services.GetRequiredServiceByName<StreamSystem>(ConnectionOptions.DefaultKey);
        
        Assert.NotNull(streamSystem);
    }

    [Fact]
    [IntegrationTest]
    public void Test_WAB_GetRequiredService_StreamSystem_Default_FromOptions()
    {
        var webApplication = _webApplicationBuilder
            .AddRabbitMqStreamSystemAsDefault(options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost" });
                options.UserName = "guest";
                options.Password = "guest";
                options.VirtualHost = "/";
            })
            .Build();
        
        Assert.NotNull(webApplication);

        var streamSystem = webApplication.Services.GetRequiredServiceByName<StreamSystem>(ConnectionOptions.DefaultKey);
        
        Assert.NotNull(streamSystem);
    }
    
    [Fact]
    [IntegrationTest]
    public void Test_WAB_GetRequiredService_StreamSystem_Default_FromOptionsBuilder()
    {
        var webApplication = _webApplicationBuilder
            .AddRabbitMqStreamSystemAsDefault(options => options.BindConfiguration("Path"))
            .Build();
        
        Assert.NotNull(webApplication);

        var streamSystem = webApplication.Services.GetRequiredServiceByName<StreamSystem>(ConnectionOptions.DefaultKey);
        
        Assert.NotNull(streamSystem);
    }

    [Fact]
    [IntegrationTest]
    public void Test_WAB_GetRequiredService_StreamSystem_Test_FromConfigSectionPath()
    {
        var webApplication = _webApplicationBuilder
            .AddRabbitMqStreamSystem("test", "Path")
            .Build();
        
        Assert.NotNull(webApplication);

        var streamSystem = webApplication.Services.GetRequiredServiceByName<StreamSystem>("test");
        
        Assert.NotNull(streamSystem);
    }

    [Fact]
    [IntegrationTest]
    public void Test_WAB_GetRequiredService_StreamSystem_Test_FromOptions()
    {
        var webApplication = _webApplicationBuilder
            .AddRabbitMqStreamSystem("test", options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost" });
                options.UserName = "guest";
                options.Password = "guest";
                options.VirtualHost = "/";
            })
            .Build();
        
        Assert.NotNull(webApplication);

        var streamSystem = webApplication.Services.GetRequiredServiceByName<StreamSystem>("test");
        
        Assert.NotNull(streamSystem);
    }
    
    [Fact]
    [IntegrationTest]
    public void Test_WAB_GetRequiredService_StreamSystem_Test_FromOptionsBuilder()
    {
        var webApplication = _webApplicationBuilder
            .AddRabbitMqStreamSystem("test", options => options.BindConfiguration("Path"))
            .Build();
        
        Assert.NotNull(webApplication);

        var streamSystem = webApplication.Services.GetRequiredServiceByName<StreamSystem>("test");
        
        Assert.NotNull(streamSystem);
    }
}
