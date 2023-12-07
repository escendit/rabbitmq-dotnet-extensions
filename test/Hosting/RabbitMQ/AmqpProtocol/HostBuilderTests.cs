// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace AmqpProtocol;

using Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
#if !NET8_0_OR_GREATER
using Orleans.Runtime;
#endif
using RabbitMQ.Client;
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
            .ConfigureAppConfiguration(builder =>
            {
                builder.AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json");
            });
#if !NET8_0_OR_GREATER
        _hostBuilder
            .ConfigureServices(services => services
                .TryAddSingleton(typeof(IKeyedServiceCollection<,>), typeof(KeyedServiceCollection<,>)));
#endif
    }

    /// <summary>
    /// Test GetRequiredService for ConnectionFactory Default FromConfigSectionPath.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_ConnectionFactory_Default_FromConfigSectionPath()
    {
        var host = _hostBuilder
            .AddRabbitMqConnectionFactory(ConnectionOptions.DefaultKey, "Path")
            .Build();

        Assert.NotNull(host);

        var connectionFactory = GetAmqpService<IConnectionFactory>(host, ConnectionOptions.DefaultKey);

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
            .AddRabbitMqConnectionFactory(ConnectionOptions.DefaultKey, options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost" });
                options.UserName = "guest";
                options.Password = "guest";
                options.VirtualHost = "/";
            })
            .Build();

        Assert.NotNull(host);

        var connectionFactory = GetAmqpService<IConnectionFactory>(host, ConnectionOptions.DefaultKey);

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
            .AddRabbitMqConnectionFactory(ConnectionOptions.DefaultKey, options => options.BindConfiguration("Path"))
            .Build();

        Assert.NotNull(host);

        var connectionFactory = GetAmqpService<IConnectionFactory>(host, ConnectionOptions.DefaultKey);

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
            .ConfigureServices(services => services
                .AddRabbitMqConnectionOptionsAsDefault("Path"))
            .AddRabbitMqConnectionFactoryFromOptions("test", ConnectionOptions.DefaultKey)
            .Build();

        Assert.NotNull(host);

        var connectionFactory = GetAmqpService<IConnectionFactory>(host, "test");

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
            .AddRabbitMqConnection(ConnectionOptions.DefaultKey, "Path")
            .Build();

        Assert.NotNull(host);

        var connection = GetAmqpService<IConnection>(host, ConnectionOptions.DefaultKey);

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

        var connection = GetAmqpService<IConnection>(host, ConnectionOptions.DefaultKey);

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
            .AddRabbitMqConnection(ConnectionOptions.DefaultKey, options => options.BindConfiguration("Path"))
            .Build();

        Assert.NotNull(host);

        var connection = GetAmqpService<IConnection>(host, ConnectionOptions.DefaultKey);

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
            .ConfigureServices(services => services
                .AddRabbitMqConnectionOptionsAsDefault("Path"))
            .AddRabbitMqConnectionFactoryFromOptions("test", ConnectionOptions.DefaultKey)
            .AddRabbitMqConnectionFromFactory("test", "test")
            .Build();

        Assert.NotNull(host);

        var connection = GetAmqpService<IConnection>(host, "test");

        Assert.NotNull(connection);
    }

    private static TService GetAmqpService<TService>(IHost host, string name)
        where TService : class
    {
#if NET8_0_OR_GREATER
        return host.Services.GetRequiredKeyedService<TService>(name);
#else
        return host.Services.GetRequiredServiceByKey<object?, TService>(name);
#endif
    }
}
