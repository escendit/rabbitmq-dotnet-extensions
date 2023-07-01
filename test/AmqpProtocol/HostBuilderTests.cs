// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.Orleans.Clients.RabbitMQ.AmqpProtocol.Tests;

using Abstractions;
using AmqpProtocol;
using global::Orleans.Runtime;
using global::RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Xunit.Categories;

/// <summary>
/// Host Builder Tests.
/// </summary>
public class HostBuilderTests
{
    private readonly IHostBuilder _hostBuilder;

    /// <summary>
    /// Initializes a new instance of the <see cref="HostBuilderTests"/> class.
    /// </summary>
    public HostBuilderTests()
    {
        _hostBuilder = Host
            .CreateDefaultBuilder()
            .ConfigureServices(services => services
                .TryAddSingleton(typeof(IKeyedServiceCollection<,>), typeof(KeyedServiceCollection<,>)));
    }

    /// <summary>
    /// Test GetRequiredService for ConnectionFactory Default FromConfigSectionPath.
    /// </summary>
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

    /// <summary>
    /// Test GetRequiredService ConnectionFactory Default FromOptions.
    /// </summary>
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

    /// <summary>
    /// Test GetRequiredService ConnectionFactory Default FromOptionsBuilder.
    /// </summary>
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

    /// <summary>
    /// Test GetRequiredService ConnectionFactory Default FromOption Test.
    /// </summary>
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

    /// <summary>
    /// Test GetRequiredService Connection Default FromConfigSectionPath.
    /// </summary>
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

    /// <summary>
    /// Test GetRequiredService Connection Default FromOptions.
    /// </summary>
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

    /// <summary>
    /// Test GetRequiredService Connection Default FromOptionsBuilder.
    /// </summary>
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

    /// <summary>
    /// Test GetRequiredService Connection Test FromFactory Test.
    /// </summary>
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
