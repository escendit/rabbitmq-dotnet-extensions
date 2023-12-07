// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.Extensions.Hosting.RabbitMQ.StreamProtocol.Tests;

using DependencyInjection.RabbitMQ.Abstractions;
using global::RabbitMQ.Stream.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
#if !NET8_0_OR_GREATER
using Microsoft.Extensions.DependencyInjection.Extensions;
using Orleans.Runtime;
#endif
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
    /// Test GetRequiredService StreamSystem Default FromConfigSectionPath.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_StreamSystem_Default_FromConfigSectionPath()
    {
        var host = _hostBuilder
            .AddRabbitMqStreamSystemAsDefault("Path")
            .Build();

        Assert.NotNull(host);

        var streamSystem = GetStreamService<StreamSystem>(host, ConnectionOptions.DefaultKey);

        Assert.NotNull(streamSystem);
    }

    /// <summary>
    /// Test GetRequiredService StreamSystem Default FromOptions.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_StreamSystem_Default_FromOptions()
    {
        var host = _hostBuilder
            .AddRabbitMqStreamSystemAsDefault(options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost" });
                options.UserName = "guest";
                options.Password = "guest";
                options.VirtualHost = "/";
            })
            .Build();

        Assert.NotNull(host);

        var streamSystem = GetStreamService<StreamSystem>(host, ConnectionOptions.DefaultKey);

        Assert.NotNull(streamSystem);
    }

    /// <summary>
    /// Test GetRequiredService StreamSystem Default FromOptionsBuilder.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_StreamSystem_Default_FromOptionsBuilder()
    {
        var host = _hostBuilder
            .AddRabbitMqStreamSystemAsDefault(options => options.BindConfiguration("Path"))
            .Build();

        Assert.NotNull(host);

        var streamSystem = GetStreamService<StreamSystem>(host, ConnectionOptions.DefaultKey);

        Assert.NotNull(streamSystem);
    }

    /// <summary>
    /// Test GetRequiredService StreamSystem Test FromConfigSectionPath.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_StreamSystem_Test_FromConfigSectionPath()
    {
        var host = _hostBuilder
            .AddRabbitMqStreamSystem("test", "Path")
            .Build();

        Assert.NotNull(host);

        var streamSystem = GetStreamService<StreamSystem>(host, "test");

        Assert.NotNull(streamSystem);
    }

    /// <summary>
    /// Test GetRequiredService StreamSystem Test FromOptions.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_StreamSystem_Test_FromOptions()
    {
        var host = _hostBuilder
            .AddRabbitMqStreamSystem("test", options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost" });
                options.UserName = "guest";
                options.Password = "guest";
                options.VirtualHost = "/";
            })
            .Build();

        Assert.NotNull(host);

        var streamSystem = GetStreamService<StreamSystem>(host, "test");

        Assert.NotNull(streamSystem);
    }

    /// <summary>
    /// Test GetRequiredService StreamSystem Test FromOptionsBuilder.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_StreamSystem_Test_FromOptionsBuilder()
    {
        var host = _hostBuilder
            .AddRabbitMqStreamSystem("test", options => options.BindConfiguration("Path"))
            .Build();

        Assert.NotNull(host);

        var streamSystem = GetStreamService<StreamSystem>(host, "test");

        Assert.NotNull(streamSystem);
    }

    private static TService GetStreamService<TService>(IHost host, string name)
        where TService : class
    {
#if NET8_0_OR_GREATER
        return host.Services.GetRequiredKeyedService<TService>(name);
#else
        return host.Services.GetRequiredServiceByKey<object?, TService>(name);
#endif
    }
}
