// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.Orleans.Clients.RabbitMQ.Abstractions.Tests;

using global::Orleans;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Xunit.Categories;

/// <summary>
/// Service Collection Tests.
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
    }

    /// <summary>
    /// Test - AddRabbitMqConnectionOptionsAsDefault - setting options.
    /// </summary>
    [Fact]
    [UnitTest]
    public void Test_AddRabbitMqConnectionOptionsAsDefault_Options()
    {
        var host = _hostBuilder
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
        var host = _hostBuilder
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
        var host = _hostBuilder
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
        var host = _hostBuilder
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
        var host = _hostBuilder
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
        var host = _hostBuilder
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
        var host = _hostBuilder
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
