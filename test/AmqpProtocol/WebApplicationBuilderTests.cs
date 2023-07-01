// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.Orleans.Clients.RabbitMQ.AmqpProtocol.Tests;

using Abstractions;
using global::Orleans.Runtime;
using global::RabbitMQ.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
            .Services
            .TryAddSingleton(typeof(IKeyedServiceCollection<,>), typeof(KeyedServiceCollection<,>));

        _webApplicationBuilder
            .Configuration
            .AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json");
    }

    /// <summary>
    /// Test GetRequiredService for ConnectionFactory Default FromConfigSectionPath.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_ConnectionFactory_Default_FromConfigSectionPath()
    {
        var host = _webApplicationBuilder
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
        var host = _webApplicationBuilder
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
        var host = _webApplicationBuilder
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
        var host = _webApplicationBuilder
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
        var host = _webApplicationBuilder
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
        var host = _webApplicationBuilder
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
        var host = _webApplicationBuilder
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
        var host = _webApplicationBuilder
            .AddRabbitMqConnectionOptionsAsDefault("Path")
            .AddRabbitMqConnectionFactoryFromOption("test", ConnectionOptions.DefaultKey)
            .AddRabbitMqConnectionFromFactory("test", "test")
            .Build();

        Assert.NotNull(host);

        var connection = host.Services.GetRequiredServiceByName<IConnection>("test");

        Assert.NotNull(connection);
    }
}
