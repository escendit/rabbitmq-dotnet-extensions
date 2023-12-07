// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.Extensions.DependencyInjection.RabbitMQ.AmqpProtocol.Tests;

using Abstractions;
using global::RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
#if !NET8_0_OR_GREATER
using Microsoft.Extensions.DependencyInjection.Extensions;
using Orleans.Runtime;
#endif

/// <summary>
/// Service Collection Tests.
/// </summary>
public class ServiceCollectionTests
{
    private static IServiceCollection Services
    {
        get
        {
            var services = new ServiceCollection();

#if !NET8_0_OR_GREATER
            services.TryAddSingleton(typeof(IKeyedServiceCollection<,>), typeof(KeyedServiceCollection<,>));
#endif
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json", false, true);
            var configuration = configurationBuilder.Build();
            return services
                .AddSingleton<IConfiguration>(configuration)
                .AddOptions()
                .Configure<ConnectionOptions>(configuration.GetSection("Path"));
        }
    }

    /// <summary>
    /// Test GetRequiredService for ConnectionFactory Default FromConfigSectionPath.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_ConnectionFactory_Default_FromConfigSectionPath()
    {
        var serviceProvider = Services
            .AddRabbitMqConnectionFactory(ConnectionOptions.DefaultKey, "Path")
            .BuildServiceProvider();

        Assert.NotNull(serviceProvider);

        var connectionFactory = GetAmqpService<IConnectionFactory>(serviceProvider, ConnectionOptions.DefaultKey);

        Assert.NotNull(connectionFactory);
    }

    /// <summary>
    /// Test GetRequiredService ConnectionFactory Default FromOptions.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_ConnectionFactory_Default_FromOptions()
    {
        var serviceProvider = Services
            .AddRabbitMqConnectionFactory(ConnectionOptions.DefaultKey, options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost" });
                options.UserName = "guest";
                options.Password = "guest";
                options.VirtualHost = "/";
            })
            .BuildServiceProvider();

        Assert.NotNull(serviceProvider);

        var connectionFactory = GetAmqpService<IConnectionFactory>(serviceProvider, ConnectionOptions.DefaultKey);

        Assert.NotNull(connectionFactory);
    }

    /// <summary>
    /// Test GetRequiredService ConnectionFactory Default FromOptionsBuilder.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_ConnectionFactory_Default_FromOptionsBuilder()
    {
        var serviceProvider = Services
            .AddRabbitMqConnectionFactory(ConnectionOptions.DefaultKey, options => options.BindConfiguration("Path"))
            .BuildServiceProvider();

        Assert.NotNull(serviceProvider);

        var connectionFactory = GetAmqpService<IConnectionFactory>(serviceProvider, ConnectionOptions.DefaultKey);

        Assert.NotNull(connectionFactory);
    }

    /// <summary>
    /// Test GetRequiredService ConnectionFactory Default FromOption Test.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_ConnectionFactory_Default_FromOption_Test()
    {
        var serviceProvider = Services
            .AddRabbitMqConnectionOptionsAsDefault("Path")
            .AddRabbitMqConnectionFactoryFromOptions("test", ConnectionOptions.DefaultKey)
            .BuildServiceProvider();

        Assert.NotNull(serviceProvider);

        var connectionFactory = GetAmqpService<IConnectionFactory>(serviceProvider, "test");

        Assert.NotNull(connectionFactory);
    }

    /// <summary>
    /// Test GetRequiredService Connection Default FromConfigSectionPath.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_Connection_Default_FromConfigSectionPath()
    {
        var serviceProvider = Services
            .AddRabbitMqConnection(ConnectionOptions.DefaultKey, "Path")
            .BuildServiceProvider();

        Assert.NotNull(serviceProvider);

        var connection = GetAmqpService<IConnection>(serviceProvider, ConnectionOptions.DefaultKey);

        Assert.NotNull(connection);
    }

    /// <summary>
    /// Test GetRequiredService Connection Default FromOptions.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_Connection_Default_FromOptions()
    {
        var serviceProvider = Services
            .AddRabbitMqConnection(ConnectionOptions.DefaultKey, options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost" });
                options.UserName = "guest";
                options.Password = "guest";
                options.VirtualHost = "/";
            })
            .BuildServiceProvider();

        Assert.NotNull(serviceProvider);

        var connection = GetAmqpService<IConnection>(serviceProvider, ConnectionOptions.DefaultKey);

        Assert.NotNull(connection);
    }

    /// <summary>
    /// Test GetRequiredService Connection Default FromOptionsBuilder.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_Connection_Default_FromOptionsBuilder()
    {
        var serviceProvider = Services
            .AddRabbitMqConnection(ConnectionOptions.DefaultKey, options => options
                .BindConfiguration("Path"))
            .BuildServiceProvider();

        Assert.NotNull(serviceProvider);

        var connection = GetAmqpService<IConnection>(serviceProvider, ConnectionOptions.DefaultKey);

        Assert.NotNull(connection);
    }

    /// <summary>
    /// Test GetRequiredService Connection Test FromFactory Test.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_Connection_Test_FromFactory_Test()
    {
        var serviceProvider = Services
            .AddRabbitMqConnectionOptionsAsDefault("Path")
            .AddRabbitMqConnectionFactoryFromOptions("test", ConnectionOptions.DefaultKey)
            .AddRabbitMqConnectionFromFactory("test", "test")
            .BuildServiceProvider();

        Assert.NotNull(serviceProvider);

        var connection = GetAmqpService<IConnection>(serviceProvider, "test");

        Assert.NotNull(connection);
    }

    private static TService GetAmqpService<TService>(IServiceProvider serviceProvider, string name)
        where TService : class
    {
#if NET8_0_OR_GREATER
        return serviceProvider.GetRequiredKeyedService<TService>(name);
#else
        return serviceProvider.GetRequiredServiceByKey<object?, TService>(name);
#endif
    }
}
