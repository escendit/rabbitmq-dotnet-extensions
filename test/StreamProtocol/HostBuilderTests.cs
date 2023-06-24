using Escendit.Orleans.Clients.RabbitMQ.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Orleans.Runtime;
using RabbitMQ.Stream.Client;
using Xunit.Categories;

namespace Escendit.Orleans.Clients.RabbitMQ.StreamProtocol.Tests;

public class HostBuilderTests
{
    private readonly IHostBuilder _hostBuilder;

    public HostBuilderTests()
    {
        _hostBuilder = Host
            .CreateDefaultBuilder();
    }
    
    [Fact]
    [UnitTest]
    public void Test1()
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
}
