// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.Orleans.Clients.RabbitMQ.AmqpProtocol;

using Abstractions;
using global::Orleans;
using global::Orleans.Runtime;
using global::RabbitMQ.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

/// <summary>
/// Host Builder Extensions.
/// </summary>
public static partial class HostBuilderExtensions
{
    /// <summary>
    /// Add Rabbit Mq Connection.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqConnection(
        this IHostBuilder hostBuilder,
        string name,
        Action<ConnectionOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .AddRabbitMqConnectionOptions(name, configureOptions)
            .AddRabbitMqConnectionFactoryFromOption(name, name)
            .ConfigureServices(services =>
                services
                    .AddSingletonNamedService(name, CreateConnection));
    }

    /// <summary>
    /// Add Rabbit Mq Connection.
    /// </summary>
    /// <param name="hostBuilder">The host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqConnection(
        this IHostBuilder hostBuilder,
        string name,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .AddRabbitMqConnectionOptions(name, configureOptions)
            .AddRabbitMqConnectionFactoryFromOption(name, name)
            .ConfigureServices(services => services
                .AddSingletonNamedService(name, CreateConnection));
    }

    /// <summary>
    /// Add Rabbit Mq Connection.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqConnection(
        this IHostBuilder hostBuilder,
        string name,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        return hostBuilder
            .AddRabbitMqConnectionOptions(name, configSectionPath)
            .AddRabbitMqConnectionFactoryFromOption(name, name)
            .ConfigureServices(services => services
                .AddSingletonNamedService(name, CreateConnection));
    }

    /// <summary>
    /// Add Rabbit Mq Connection From Factory.
    /// </summary>
    /// <remarks>
    /// It needs provided connection options.
    /// </remarks>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="factoryName">The factory name.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqConnectionFromFactory(
        this IHostBuilder hostBuilder,
        string name,
        string factoryName)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(factoryName);
        return hostBuilder
            .ConfigureServices(services => services
                .AddSingletonNamedService(name, (serviceProvider, _) =>
                    CreateConnectionFromFactory(serviceProvider, factoryName)));
    }

    private static IConnection CreateConnectionInternal(IServiceProvider serviceProvider, IConnectionFactory connectionFactory, IList<AmqpTcpEndpoint> endpoints, string clientProvidedName)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(connectionFactory);
        ArgumentNullException.ThrowIfNull(endpoints);
        ArgumentNullException.ThrowIfNull(clientProvidedName);
        return connectionFactory.CreateConnection(endpoints, clientProvidedName);
    }

    private static IConnection CreateConnection(IServiceProvider serviceProvider, string name)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(name);
        var connectionFactory = serviceProvider.GetRequiredServiceByName<IConnectionFactory>(name);
        var options = serviceProvider.GetOptionsByName<ConnectionOptions>(name);
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

    private static IConnection CreateConnectionFromFactory(IServiceProvider serviceProvider, string name)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(name);
        var connectionFactory = serviceProvider.GetRequiredServiceByName<IConnectionFactory>(name);
        var optionName = connectionFactory.ClientProperties[Abstractions.Constants.ClientPropertyOptionsNameKey] as string;
        var options = serviceProvider.GetOptionsByName<ConnectionOptions>(optionName);
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

    private static IList<AmqpTcpEndpoint> BuildEndpoints(ConnectionOptions options)
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
