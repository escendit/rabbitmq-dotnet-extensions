// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Microsoft.Extensions.Hosting;

using DependencyInjection;
using Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions;
using Options;

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
            .ConfigureServices(services => services
                .AddRabbitMqConnection(name, configureOptions));
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
            .ConfigureServices(services => services
                .AddRabbitMqConnection(name, configureOptions));
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
            .ConfigureServices(services => services
                .AddRabbitMqConnection(name, configSectionPath));
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
                .AddRabbitMqConnectionFromFactory(name, factoryName));
    }
}
