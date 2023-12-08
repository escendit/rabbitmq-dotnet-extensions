// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Microsoft.Extensions.DependencyInjection;

using Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions;
using Options;
using RabbitMQ.Client;
using Constants = Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions.Constants;
#if !NET8_0_OR_GREATER
using Orleans;
using Orleans.Runtime;
#endif

/// <summary>
/// Service Collection Extensions.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Rabbit Mq Connection.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqConnection(
        this IServiceCollection services,
        string name,
        Action<ConnectionOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return services
            .AddRabbitMqConnectionOptions(name, configureOptions)
            .AddRabbitMqConnectionFactoryFromOptions(name, name)
            .AddService(name, CreateConnection);
    }

    /// <summary>
    /// Add Rabbit Mq Connection.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IServiceCollection AddRabbitMqConnection(
        this IServiceCollection services,
        string name,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return services
            .AddRabbitMqConnectionOptions(name, configureOptions)
            .AddRabbitMqConnectionFactoryFromOptions(name, name)
            .AddService(name, CreateConnection);
    }

    /// <summary>
    /// Add Rabbit Mq Connection.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqConnection(
        this IServiceCollection services,
        string name,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        return services
            .AddRabbitMqConnectionOptions(name, configSectionPath)
            .AddRabbitMqConnectionFactoryFromOptions(name, name)
            .AddService(name, CreateConnection);
    }

    /// <summary>
    /// Add Rabbit Mq Connection From Factory.
    /// </summary>
    /// <remarks>
    /// It needs provided connection options.
    /// </remarks>
    /// <param name="services">The initial service collection.</param>
    /// <param name="name">The name.</param>
    /// <param name="factoryName">The factory name.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqConnectionFromFactory(
        this IServiceCollection services,
        string name,
        string factoryName)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(factoryName);
        return services
            .AddService(name, (serviceProvider, _) =>
                CreateConnectionFromFactory(serviceProvider, factoryName));
    }

    private static IConnection CreateConnectionInternal(IServiceProvider serviceProvider, IConnectionFactory connectionFactory, IList<AmqpTcpEndpoint> endpoints, string clientProvidedName)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(connectionFactory);
        ArgumentNullException.ThrowIfNull(endpoints);
        ArgumentNullException.ThrowIfNull(clientProvidedName);
        return connectionFactory.CreateConnection(endpoints, clientProvidedName);
    }

    private static IConnection CreateConnection(IServiceProvider serviceProvider, object? name)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(name);

        if (name is not string stringedName)
        {
            throw new ArgumentException("Invalid name");
        }
#if NET8_0_OR_GREATER
        var connectionFactory = serviceProvider.GetRequiredKeyedService<IConnectionFactory>(name);
        var optionsMonitor = serviceProvider.GetRequiredService<IOptionsMonitor<ConnectionOptions>>();
        var options = optionsMonitor.Get(stringedName);
#else
        var connectionFactory = serviceProvider.GetRequiredServiceByKey<object?, IConnectionFactory>(name);
        var options = serviceProvider.GetOptionsByName<ConnectionOptions>(stringedName);
#endif
        return Task
            .Run(() =>
                CreateConnectionInternal(
                    serviceProvider,
                    connectionFactory,
                    BuildEndpoints(options),
                    options.ClientProvidedName!))
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();
    }

    private static IConnection CreateConnectionFromFactory(IServiceProvider serviceProvider, object? name)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(name);
#if NET8_0_OR_GREATER
        var connectionFactory = serviceProvider.GetRequiredKeyedService<IConnectionFactory>(name);
        var optionName = connectionFactory.ClientProperties[Constants.ClientPropertyOptionsNameKey] as string;
        var monitor = serviceProvider.GetRequiredService<IOptionsMonitor<ConnectionOptions>>();
        var options = monitor.Get(optionName);
#else
        var connectionFactory = serviceProvider.GetRequiredServiceByKey<object?, IConnectionFactory>(name);
        var optionName = connectionFactory.ClientProperties[Constants.ClientPropertyOptionsNameKey] as string;
        var options = serviceProvider.GetOptionsByName<ConnectionOptions>(optionName);
#endif
        return Task
            .Run(() =>
                CreateConnectionInternal(
                    serviceProvider,
                    connectionFactory,
                    BuildEndpoints(options),
                    options.ClientProvidedName!))
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();
    }

    private static List<AmqpTcpEndpoint> BuildEndpoints(ConnectionOptions options)
    {
        return options
            .Endpoints
            .Select(endpoint =>
                new AmqpTcpEndpoint(
                    endpoint.HostName,
                    endpoint.Port ?? 5672))
            .ToList();
    }
}
