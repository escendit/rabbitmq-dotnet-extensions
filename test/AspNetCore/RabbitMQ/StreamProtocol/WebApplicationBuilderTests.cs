// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.AspNetCore.Builder.RabbitMQ.StreamProtocol.Tests;

using Extensions.DependencyInjection.RabbitMQ.Abstractions;
using global::RabbitMQ.Stream.Client;
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
    /// Test GetRequiredService StreamSystem Default FromConfigSectionPath with WAB.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_WAB_GetRequiredService_StreamSystem_Default_FromConfigSectionPath()
    {
        var webApplication = _webApplicationBuilder
            .AddRabbitMqStreamSystem(ConnectionOptions.DefaultKey, "Path")
            .Build();

        Assert.NotNull(webApplication);

        var streamSystem = GetStreamService<StreamSystem>(webApplication, ConnectionOptions.DefaultKey);

        Assert.NotNull(streamSystem);
    }

    /// <summary>
    /// Test GetRequiredService StreamSystem Default FromOptions with WAB.
    /// </summary>
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

        var streamSystem = GetStreamService<StreamSystem>(webApplication, ConnectionOptions.DefaultKey);

        Assert.NotNull(streamSystem);
    }

    /// <summary>
    /// Test GetRequiredService StreamSystem Default FromOptionsBuilder with WAB.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_WAB_GetRequiredService_StreamSystem_Default_FromOptionsBuilder()
    {
        var webApplication = _webApplicationBuilder
            .AddRabbitMqStreamSystemAsDefault(options => options.BindConfiguration("Path"))
            .Build();

        Assert.NotNull(webApplication);

        var streamSystem = GetStreamService<StreamSystem>(webApplication, ConnectionOptions.DefaultKey);

        Assert.NotNull(streamSystem);
    }

    /// <summary>
    /// Test GetRequiredService StreamSystem Test FromConfigSectionPath with WAB.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_WAB_GetRequiredService_StreamSystem_Test_FromConfigSectionPath()
    {
        var webApplication = _webApplicationBuilder
            .AddRabbitMqStreamSystem("test", "Path")
            .Build();

        Assert.NotNull(webApplication);

        var streamSystem = GetStreamService<StreamSystem>(webApplication, "test");

        Assert.NotNull(streamSystem);
    }

    /// <summary>
    /// Test GetRequiredService StreamSystem Test FromOptions with WAB.
    /// </summary>
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

        var streamSystem = GetStreamService<StreamSystem>(webApplication, "test");

        Assert.NotNull(streamSystem);
    }

    /// <summary>
    /// Test GetRequiredService StreamSystem Test FromOptionsBuilder.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_WAB_GetRequiredService_StreamSystem_Test_FromOptionsBuilder()
    {
        var webApplication = _webApplicationBuilder
            .AddRabbitMqStreamSystem("test", options => options.BindConfiguration("Path"))
            .Build();

        Assert.NotNull(webApplication);

        var streamSystem = GetStreamService<StreamSystem>(webApplication, "test");

        Assert.NotNull(streamSystem);
    }

    private static TService GetStreamService<TService>(WebApplication webApplication, string name)
        where TService : class
    {
#if NET8_0_OR_GREATER
        return webApplication.Services.GetRequiredKeyedService<TService>(name);
#else
        return webApplication.Services.GetRequiredServiceByKey<object?, TService>(name);
#endif
    }
}
