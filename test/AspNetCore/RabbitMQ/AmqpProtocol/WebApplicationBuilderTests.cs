// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.AspNetCore.Builder.RabbitMQ.AmqpProtocol.Tests;

using Extensions.DependencyInjection.RabbitMQ.Abstractions;
using global::RabbitMQ.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
#if !NET8_0_OR_GREATER
using Microsoft.Extensions.DependencyInjection.Extensions;
using Orleans.Runtime;
#endif
using Xunit.Categories;

/// <summary>
/// Web Application Builder Tests.
/// </summary>
public class WebApplicationBuilderTests
{
    private readonly WebApplicationBuilder _webApplicationBuilder;

    /// <summary>
    /// Initializes a new instance of the <see cref="WebApplicationBuilderTests"/> class.
    /// </summary>
    public WebApplicationBuilderTests()
    {
        _webApplicationBuilder = WebApplication
            .CreateBuilder();

        _webApplicationBuilder
            .Configuration
            .AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json");

#if !NET8_0_OR_GREATER
        _webApplicationBuilder
            .Services
            .TryAddSingleton(typeof(IKeyedServiceCollection<,>), typeof(KeyedServiceCollection<,>));
#endif

    }

    /// <summary>
    /// Test GetRequiredService for ConnectionFactory Default FromConfigSectionPath.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_ConnectionFactory_Default_FromConfigSectionPath()
    {
        var webApplication = _webApplicationBuilder
            .AddRabbitMqConnectionFactory(ConnectionOptions.DefaultKey, "Path")
            .Build();

        Assert.NotNull(webApplication);

        var connectionFactory = GetAmqpService<IConnectionFactory>(webApplication, ConnectionOptions.DefaultKey);

        Assert.NotNull(connectionFactory);
    }

    /// <summary>
    /// Test GetRequiredService ConnectionFactory Default FromOptions.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_ConnectionFactory_Default_FromOptions()
    {
        var webApplication = _webApplicationBuilder
            .AddRabbitMqConnectionFactory(ConnectionOptions.DefaultKey, options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost" });
                options.UserName = "guest";
                options.Password = "guest";
                options.VirtualHost = "/";
            })
            .Build();

        Assert.NotNull(webApplication);

        var connectionFactory = GetAmqpService<IConnectionFactory>(webApplication, ConnectionOptions.DefaultKey);

        Assert.NotNull(connectionFactory);
    }

    /// <summary>
    /// Test GetRequiredService ConnectionFactory Default FromOptionsBuilder.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_ConnectionFactory_Default_FromOptionsBuilder()
    {
        var webApplication = _webApplicationBuilder
            .AddRabbitMqConnectionFactory(ConnectionOptions.DefaultKey, options => options.BindConfiguration("Path"))
            .Build();

        Assert.NotNull(webApplication);

        var connectionFactory = GetAmqpService<IConnectionFactory>(webApplication, ConnectionOptions.DefaultKey);

        Assert.NotNull(connectionFactory);
    }

    /// <summary>
    /// Test GetRequiredService ConnectionFactory Default FromOption Test.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_ConnectionFactory_Default_FromOption_Test()
    {
        _webApplicationBuilder
            .Services
            .AddRabbitMqConnectionOptionsAsDefault("Path");

        var webApplication = _webApplicationBuilder
            .AddRabbitMqConnectionFactoryFromOption("test", ConnectionOptions.DefaultKey)
            .Build();

        Assert.NotNull(webApplication);

        var connectionFactory = GetAmqpService<IConnectionFactory>(webApplication, "test");

        Assert.NotNull(connectionFactory);
    }

    /// <summary>
    /// Test GetRequiredService Connection Default FromConfigSectionPath.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_Connection_Default_FromConfigSectionPath()
    {
        var webApplication = _webApplicationBuilder
            .AddRabbitMqConnection(ConnectionOptions.DefaultKey, "Path")
            .Build();

        Assert.NotNull(webApplication);

        var connection = GetAmqpService<IConnection>(webApplication, ConnectionOptions.DefaultKey);

        Assert.NotNull(connection);
    }

    /// <summary>
    /// Test GetRequiredService Connection Default FromOptions.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_Connection_Default_FromOptions()
    {
        var webApplication = _webApplicationBuilder
            .AddRabbitMqConnection(ConnectionOptions.DefaultKey, options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost" });
                options.UserName = "guest";
                options.Password = "guest";
                options.VirtualHost = "/";
            })
            .Build();

        Assert.NotNull(webApplication);

        var connection = GetAmqpService<IConnection>(webApplication, ConnectionOptions.DefaultKey);

        Assert.NotNull(connection);
    }

    /// <summary>
    /// Test GetRequiredService Connection Default FromOptionsBuilder.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_Connection_Default_FromOptionsBuilder()
    {
        var webApplication = _webApplicationBuilder
            .AddRabbitMqConnection(ConnectionOptions.DefaultKey, options => options.BindConfiguration("Path"))
            .Build();

        Assert.NotNull(webApplication);

        var connection = GetAmqpService<IConnection>(webApplication, ConnectionOptions.DefaultKey);

        Assert.NotNull(connection);
    }

    /// <summary>
    /// Test GetRequiredService Connection Test FromFactory Test.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_Connection_Test_FromFactory_Test()
    {
        _webApplicationBuilder
            .Services
            .AddRabbitMqConnectionOptionsAsDefault("Path");

        var webApplication = _webApplicationBuilder
            .AddRabbitMqConnectionFactoryFromOption("test", ConnectionOptions.DefaultKey)
            .AddRabbitMqConnectionFromFactory("test", "test")
            .Build();

        Assert.NotNull(webApplication);

        var connection = GetAmqpService<IConnection>(webApplication, "test");

        Assert.NotNull(connection);
    }

    private static TService GetAmqpService<TService>(WebApplication webApplication, string name)
        where TService : class
    {
#if NET8_0_OR_GREATER
        return webApplication.Services.GetRequiredKeyedService<TService>(name);
#else
        return webApplication.Services.GetRequiredServiceByKey<object?, TService>(name);
#endif
    }
}
