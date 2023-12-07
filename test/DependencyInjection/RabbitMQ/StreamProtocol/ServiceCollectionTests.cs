// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.Extensions.DependencyInjection.RabbitMQ.StreamProtocol.Tests;

using Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions;
using global::RabbitMQ.Stream.Client;
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
                .AddLogging()
                .Configure<ConnectionOptions>(configuration);
        }
    }

    /// <summary>
    /// Test GetRequiredService StreamSystem Default FromConfigSectionPath.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_StreamSystem_Default_FromConfigSectionPath()
    {
        var serviceProvider = Services
            .AddRabbitMqStreamSystemAsDefault("Path")
            .BuildServiceProvider();

        Assert.NotNull(serviceProvider);

        var streamSystem = GetStreamService<StreamSystem>(serviceProvider, ConnectionOptions.DefaultKey);

        Assert.NotNull(streamSystem);
    }

    /// <summary>
    /// Test GetRequiredService StreamSystem Default FromOptions.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_StreamSystem_Default_FromOptions()
    {
        var serviceProvider = Services
            .AddRabbitMqStreamSystemAsDefault(options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost" });
                options.UserName = "guest";
                options.Password = "guest";
                options.VirtualHost = "/";
            })
            .BuildServiceProvider();

        Assert.NotNull(serviceProvider);

        var streamSystem = GetStreamService<StreamSystem>(serviceProvider, ConnectionOptions.DefaultKey);

        Assert.NotNull(streamSystem);
    }

    /// <summary>
    /// Test GetRequiredService StreamSystem Default FromOptionsBuilder.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_StreamSystem_Default_FromOptionsBuilder()
    {
        var serviceProvider = Services
            .AddRabbitMqStreamSystemAsDefault(options => options.BindConfiguration("Path"))
            .BuildServiceProvider();

        Assert.NotNull(serviceProvider);

        var streamSystem = GetStreamService<StreamSystem>(serviceProvider, ConnectionOptions.DefaultKey);

        Assert.NotNull(streamSystem);
    }

    /// <summary>
    /// Test GetRequiredService StreamSystem Test FromConfigSectionPath.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_StreamSystem_Test_FromConfigSectionPath()
    {
        var serviceProvider = Services
            .AddRabbitMqStreamSystem("test", "Path")
            .BuildServiceProvider();

        Assert.NotNull(serviceProvider);

        var streamSystem = GetStreamService<StreamSystem>(serviceProvider, "test");

        Assert.NotNull(streamSystem);
    }

    /// <summary>
    /// Test GetRequiredService StreamSystem Test FromOptions.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_StreamSystem_Test_FromOptions()
    {
        var serviceProvider = Services
            .AddRabbitMqStreamSystem("test", options =>
            {
                options.Endpoints.Add(new Endpoint { HostName = "localhost" });
                options.UserName = "guest";
                options.Password = "guest";
                options.VirtualHost = "/";
            })
            .BuildServiceProvider();

        Assert.NotNull(serviceProvider);

        var streamSystem = GetStreamService<StreamSystem>(serviceProvider, "test");

        Assert.NotNull(streamSystem);
    }

    /// <summary>
    /// Test GetRequiredService StreamSystem Test FromOptionsBuilder.
    /// </summary>
    [Fact]
    [IntegrationTest]
    public void Test_GetRequiredService_StreamSystem_Test_FromOptionsBuilder()
    {
        var serviceProvider = Services
            .AddRabbitMqStreamSystem("test", options => options.BindConfiguration("Path"))
            .BuildServiceProvider();

        Assert.NotNull(serviceProvider);

        var streamSystem = GetStreamService<StreamSystem>(serviceProvider, "test");

        Assert.NotNull(streamSystem);
    }

    private static TService GetStreamService<TService>(IServiceProvider serviceProvider, string name)
        where TService : class
    {
#if NET8_0_OR_GREATER
        return serviceProvider.GetRequiredKeyedService<TService>(name);
#else
        return serviceProvider.GetRequiredServiceByKey<object?, TService>(name);
#endif
    }
}
