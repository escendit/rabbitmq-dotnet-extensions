// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Microsoft.Extensions.Hosting;

using DependencyInjection;
using Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions;
using Hosting;
using Options;
using RabbitMQ.Client;

/// <summary>
/// Host Builder Extensions.
/// </summary>
public static partial class HostBuilderExtensions
{
    /// <summary>
    /// Add Rabbit Mq Connection Factory.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqConnectionFactory(
        this IHostBuilder hostBuilder,
        string name,
        Action<ConnectionOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .ConfigureServices(services => services
                .AddRabbitMqConnectionFactory(name, configureOptions));
    }

    /// <summary>
    /// Add Rabbit Mq Stream Client.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqConnectionFactory(
        this IHostBuilder hostBuilder,
        string name,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .ConfigureServices(services => services
                .AddRabbitMqConnectionFactory(name, configureOptions));
    }

    /// <summary>
    /// Add Rabbit Mq Stream Client.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqConnectionFactory(
        this IHostBuilder hostBuilder,
        string name,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        return hostBuilder
            .ConfigureServices(services => services
                .AddRabbitMqConnectionFactory(name, configSectionPath));
    }

    /// <summary>
    /// Add Rabbit Mq Stream Client.
    /// </summary>
    /// <remarks>
    /// It needs provided connection options.
    /// </remarks>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="optionsName">The named option.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqConnectionFactoryFromOptions(
        this IHostBuilder hostBuilder,
        string name,
        string optionsName)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(optionsName);
        return hostBuilder
            .ConfigureServices(services => services
                .AddRabbitMqConnectionFactoryFromOptions(name, optionsName));
    }
}
