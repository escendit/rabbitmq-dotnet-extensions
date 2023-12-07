// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Microsoft.Extensions.Hosting;

using System.Net;
using DependencyInjection;
using Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions;
using Hosting;
using Logging;
using Options;

/// <summary>
/// Host Builder Extensions.
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Add Rabbit Mq Stream System As Default.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqStreamSystemAsDefault(
        this IHostBuilder hostBuilder,
        Action<ConnectionOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .ConfigureServices(services => services
                .AddRabbitMqStreamSystemAsDefault(configureOptions));
    }

    /// <summary>
    /// Add Rabbit Mq Stream System As Default.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqStreamSystemAsDefault(
        this IHostBuilder hostBuilder,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .ConfigureServices(services => services
                .AddRabbitMqStreamSystemAsDefault(configureOptions));
    }

    /// <summary>
    /// Add Rabbit Mq Stream System As Default.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqStreamSystemAsDefault(
        this IHostBuilder hostBuilder,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        return hostBuilder
            .ConfigureServices(services => services
                .AddRabbitMqStreamSystemAsDefault(configSectionPath));
    }

    /// <summary>
    /// Add Rabbit Mq Stream System From Options As Default.
    /// </summary>
    /// <remarks>
    /// It needs provided connection options.
    /// </remarks>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="optionsName">The named option.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqStreamSystemFromOptionsAsDefault(
        this IHostBuilder hostBuilder,
        string optionsName)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(optionsName);
        return hostBuilder
            .ConfigureServices(services => services
                .AddRabbitMqStreamSystemFromOptionAsDefault(optionsName));
    }

    /// <summary>
    /// Add Rabbit Mq Stream System.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqStreamSystem(
        this IHostBuilder hostBuilder,
        string name,
        Action<ConnectionOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .ConfigureServices(services => services
                .AddRabbitMqStreamSystem(name, configureOptions));
    }

    /// <summary>
    /// Add Rabbit Mq Stream System.
    /// </summary>
    /// <param name="hostBuilder">The host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqStreamSystem(
        this IHostBuilder hostBuilder,
        string name,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .ConfigureServices(services => services
                .AddRabbitMqStreamSystem(name, configureOptions));
    }

    /// <summary>
    /// Add Rabbit Mq Stream System.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqStreamSystem(
        this IHostBuilder hostBuilder,
        string name,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        return hostBuilder
            .ConfigureServices(services => services
                .AddRabbitMqStreamSystem(name, configSectionPath));
    }

    /// <summary>
    /// Add Rabbit Mq Stream System From Option.
    /// </summary>
    /// <remarks>
    /// It needs provided connection options.
    /// </remarks>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="optionsName">The named option.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqStreamSystemFromOptions(
        this IHostBuilder hostBuilder,
        string name,
        string optionsName)
    {
        return hostBuilder
            .ConfigureServices(services => services
                .AddRabbitMqStreamSystemFromOptions(name, optionsName));
    }
}
