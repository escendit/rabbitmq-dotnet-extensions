// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.Orleans.Clients.RabbitMQ.Abstractions.Tests;

using global::Orleans;
using global::Orleans.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
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
    /// Test - AddRabbitMqConnectionOptionsAsDefault - setting options.
    /// </summary>
    [Fact]
    [UnitTest]
    public void Test_AddRabbitMqConnectionOptionsAsDefault_Options()
    {
        var host = _webApplicationBuilder
            .AddRabbitMqConnectionOptionsAsDefault(options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost", Port = 5552 });
            })
            .Build();

        var options = host
            .Services
            .GetOptionsByName<ConnectionOptions>(ConnectionOptions.DefaultKey);

        Assert.NotNull(options);
    }

    /// <summary>
    /// Test - AddRabbitMqConnectionOptionsAsDefault - setting options builder.
    /// </summary>
    [Fact]
    [UnitTest]
    public void Test_AddRabbitMqConnectionOptionsAsDefault_OptionsBuilder()
    {
        var host = _webApplicationBuilder
            .AddRabbitMqConnectionOptionsAsDefault(options => options.BindConfiguration("Path"))
            .Build();

        var options = host
            .Services
            .GetOptionsByName<ConnectionOptions>(ConnectionOptions.DefaultKey);

        Assert.NotNull(options);
    }

    /// <summary>
    /// Test - AddRabbitMqConnectionOptionsAsDefault - setting config section path.
    /// </summary>
    [Fact]
    [UnitTest]
    public void Test_AddRabbitMqConnectionOptionsAsDefault_ConfigSectionPath()
    {
        var host = _webApplicationBuilder
            .AddRabbitMqConnectionOptionsAsDefault("Path")
            .Build();

        var options = host
            .Services
            .GetOptionsByName<ConnectionOptions>(ConnectionOptions.DefaultKey);

        Assert.NotNull(options);
    }

    /// <summary>
    /// Test - AddRabbitMqConnectionOptions - setting options.
    /// </summary>
    [Fact]
    [UnitTest]
    public void Test_AddRabbitMqConnectionOptions_Options()
    {
        var host = _webApplicationBuilder
            .AddRabbitMqConnectionOptions("test", options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost", Port = 5552 });
            })
            .Build();

        var options = host
            .Services
            .GetOptionsByName<ConnectionOptions>("test");

        Assert.NotNull(options);
    }

    /// <summary>
    /// Test - AddRabbitMqConnectionOptions - setting options builder.
    /// </summary>
    [Fact]
    [UnitTest]
    public void Test_AddRabbitMqConnectionOptions_OptionsBuilder()
    {
        var host = _webApplicationBuilder
            .AddRabbitMqConnectionOptions("test", options => options.BindConfiguration("Path"))
            .Build();

        var options = host
            .Services
            .GetOptionsByName<ConnectionOptions>("test");

        Assert.NotNull(options);
    }

    /// <summary>
    /// Test - AddRabbitMqConnectionOptionsAsDefault - setting config section path.
    /// </summary>
    [Fact]
    [UnitTest]
    public void Test_AddRabbitMqConnectionOptions_ConfigSectionPath()
    {
        var host = _webApplicationBuilder
            .AddRabbitMqConnectionOptions("test", "Path")
            .Build();

        var options = host
            .Services
            .GetOptionsByName<ConnectionOptions>("test");

        Assert.NotNull(options);
    }

    /// <summary>
    /// Test - AddRabbitMqConnectionOptions - no endpoints exception.
    /// </summary>
    [Fact]
    [UnitTest]
    public void Test_AddRabbitMqConnectionOptions_NoEndpoints()
    {
        var host = _webApplicationBuilder
            .AddRabbitMqConnectionOptions("test", options =>
            {
                options.UserName = "guest";
            })
            .Build();
        Assert.Throws<OptionsValidationException>(
            () => host
                .Services
                .GetOptionsByName<ConnectionOptions>("test"));
    }
}
