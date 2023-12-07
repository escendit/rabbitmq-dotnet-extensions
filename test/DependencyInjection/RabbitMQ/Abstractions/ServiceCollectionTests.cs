// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions.Tests;

using Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit.Categories;

/// <summary>
/// Service Collection Tests.
/// </summary>
public class ServiceCollectionTests
{
    private static IServiceCollection Services
    {
        get
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json", false, true);
            var configuration = configurationBuilder.Build();
            return new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddOptions()
                .Configure<ConnectionOptions>(configuration.GetSection("Path"));
        }
    }

    /// <summary>
    /// Test - AddRabbitMqConnectionOptionsAsDefault - setting options.
    /// </summary>
    [Fact]
    [UnitTest]
    public void Test_AddRabbitMqConnectionOptionsAsDefault_Options()
    {
        var serviceProvider = Services
            .AddRabbitMqConnectionOptionsAsDefault(options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost", Port = 5552 });
            })
            .BuildServiceProvider();

        var options = GetOptions<ConnectionOptions>(serviceProvider, ConnectionOptions.DefaultKey);

        Assert.NotNull(options);
    }

    /// <summary>
    /// Test - AddRabbitMqConnectionOptionsAsDefault - setting options builder.
    /// </summary>
    [Fact]
    [UnitTest]
    public void Test_AddRabbitMqConnectionOptionsAsDefault_OptionsBuilder()
    {
        var serviceProvider = Services
            .AddRabbitMqConnectionOptionsAsDefault(options => options.BindConfiguration("Path"))
            .BuildServiceProvider();

        var options = GetOptions<ConnectionOptions>(serviceProvider, ConnectionOptions.DefaultKey);

        Assert.NotNull(options);
    }

    /// <summary>
    /// Test - AddRabbitMqConnectionOptionsAsDefault - setting config section path.
    /// </summary>
    [Fact]
    [UnitTest]
    public void Test_AddRabbitMqConnectionOptionsAsDefault_ConfigSectionPath()
    {
        var serviceProvider = Services
            .AddRabbitMqConnectionOptionsAsDefault("Path")
            .BuildServiceProvider();

        var options = GetOptions<ConnectionOptions>(serviceProvider, ConnectionOptions.DefaultKey);

        Assert.NotNull(options);
    }

    /// <summary>
    /// Test - AddRabbitMqConnectionOptions - setting options.
    /// </summary>
    [Fact]
    [UnitTest]
    public void Test_AddRabbitMqConnectionOptions_Options()
    {
        var serviceProvider = Services
            .AddRabbitMqConnectionOptions("test", options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost", Port = 5552 });
            })
            .BuildServiceProvider();

        var options = GetOptions<ConnectionOptions>(serviceProvider, "test");

        Assert.NotNull(options);
    }

    /// <summary>
    /// Test - AddRabbitMqConnectionOptions - setting options builder.
    /// </summary>
    [Fact]
    [UnitTest]
    public void Test_AddRabbitMqConnectionOptions_OptionsBuilder()
    {
        var serviceProvider = Services
            .AddRabbitMqConnectionOptions("test", options => options.BindConfiguration("Path"))
            .BuildServiceProvider();

        var options = GetOptions<ConnectionOptions>(serviceProvider, "test");

        Assert.NotNull(options);
    }

    /// <summary>
    /// Test - AddRabbitMqConnectionOptionsAsDefault - setting config section path.
    /// </summary>
    [Fact]
    [UnitTest]
    public void Test_AddRabbitMqConnectionOptions_ConfigSectionPath()
    {
        var serviceProvider = Services
            .AddRabbitMqConnectionOptions("test", "Path")
            .BuildServiceProvider();

        var options = GetOptions<ConnectionOptions>(serviceProvider, "test");

        Assert.NotNull(options);
    }

    /// <summary>
    /// Test - AddRabbitMqConnectionOptions - no endpoints exception.
    /// </summary>
    [Fact]
    [UnitTest]
    public void Test_AddRabbitMqConnectionOptions_NoEndpoints()
    {
        var serviceProvider = Services
            .AddRabbitMqConnectionOptions("test", options =>
            {
                options.UserName = "guest";
            })
            .BuildServiceProvider();
        Assert.Throws<OptionsValidationException>(
            () => GetOptions<ConnectionOptions>(serviceProvider, "test"));
    }

    private static TOption GetOptions<TOption>(IServiceProvider serviceProvider, string name)
        where TOption : class, new()
    {
        return serviceProvider
            .GetRequiredService<IOptionsMonitor<TOption>>().Get(name);
    }
}
