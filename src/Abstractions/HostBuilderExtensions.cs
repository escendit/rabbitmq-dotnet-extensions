// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Microsoft.Extensions.Hosting;

using System.Diagnostics.CodeAnalysis;
using DependencyInjection;
using Escendit.Orleans.Clients.RabbitMQ.Abstractions;
using Options;

/// <summary>
/// Host Builder Extensions.
/// </summary>
[DynamicallyAccessedMembers(
    DynamicallyAccessedMemberTypes.All)]
public static class HostBuilderExtensions
{
    /// <summary>
    /// Add Rabbit Mq Client Options As Default.
    /// </summary>
    /// <param name="hostBuilder">The host builder.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqConnectionOptionsAsDefault(
        this IHostBuilder hostBuilder,
        Action<ConnectionOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .ConfigureServices(services => services
                .ConfigureOptions<ConnectionOptionsValidator>()
                .AddOptions<ConnectionOptions>(ConnectionOptions.DefaultKey)
                .Configure(configureOptions));
    }

    /// <summary>
    /// Add Rabbit Mq Client Options As Default.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqConnectionOptionsAsDefault(
        this IHostBuilder hostBuilder,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .ConfigureServices(services =>
            {
                configureOptions.Invoke(services
                    .ConfigureOptions<ConnectionOptionsValidator>()
                    .AddOptions<ConnectionOptions>(ConnectionOptions.DefaultKey));
            });
    }

    /// <summary>
    /// Add Rabbit Mq Client Options As Default.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqConnectionOptionsAsDefault(
        this IHostBuilder hostBuilder,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        return hostBuilder
            .ConfigureServices(services => services
                .ConfigureOptions<ConnectionOptionsValidator>()
                .AddOptions<ConnectionOptions>(ConnectionOptions.DefaultKey)
                .BindConfiguration(configSectionPath));
    }

    /// <summary>
    /// Add Rabbit Mq Client Options.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqConnectionOptions(
        this IHostBuilder hostBuilder,
        string name,
        Action<ConnectionOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .ConfigureServices(services => services
                .ConfigureOptions<ConnectionOptionsValidator>()
                .AddOptions<ConnectionOptions>(name)
                .Configure(configureOptions));
    }

    /// <summary>
    /// Add Rabbit Mq Client Options.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqConnectionOptions(
        this IHostBuilder hostBuilder,
        string name,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .ConfigureServices(services =>
            {
                configureOptions.Invoke(services
                    .ConfigureOptions<ConnectionOptionsValidator>()
                    .AddOptions<ConnectionOptions>(name));
            });
    }

    /// <summary>
    /// Add Rabbit Mq Client Options.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqConnectionOptions(
        this IHostBuilder hostBuilder,
        string name,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        return hostBuilder
            .ConfigureServices(services => services
                .ConfigureOptions<ConnectionOptionsValidator>()
                .AddOptions<ConnectionOptions>(name)
                .BindConfiguration(configSectionPath));
    }
}
