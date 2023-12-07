// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Microsoft.Extensions.DependencyInjection;

using Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions;
using Options;
#if !NET8_0_OR_GREATER
using Orleans.Runtime;
#endif

/// <summary>
/// Service Collection Extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
/// <summary>
    /// Add Rabbit Mq Client Options As Default.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqConnectionOptionsAsDefault(
        this IServiceCollection services,
        Action<ConnectionOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureOptions);
        services
            .ConfigureOptions<ConnectionOptionsValidator>()
            .AddOptions<ConnectionOptions>(ConnectionOptions.DefaultKey)
            .Configure(configureOptions);

        // also register without a name.
        services
            .AddOptions<ConnectionOptions>()
            .Configure(configureOptions);
        return services;
    }

    /// <summary>
    /// Add Rabbit Mq Client Options As Default.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqConnectionOptionsAsDefault(
        this IServiceCollection services,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureOptions);
        configureOptions.Invoke(services
            .ConfigureOptions<ConnectionOptionsValidator>()
            .AddOptions<ConnectionOptions>(ConnectionOptions.DefaultKey));

        // also register without a name.
        configureOptions.Invoke(services
            .AddOptions<ConnectionOptions>());
        return services;
    }

    /// <summary>
    /// Add Rabbit Mq Client Options As Default.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqConnectionOptionsAsDefault(
        this IServiceCollection services,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        services
            .ConfigureOptions<ConnectionOptionsValidator>()
            .AddOptions<ConnectionOptions>(ConnectionOptions.DefaultKey)
            .BindConfiguration(configSectionPath);

        // also register without a name.
        services
            .AddOptions<ConnectionOptions>()
            .BindConfiguration(configSectionPath);
        return services;
    }

    /// <summary>
    /// Add Rabbit Mq Client Options.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqConnectionOptions(
        this IServiceCollection services,
        string name,
        Action<ConnectionOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        services
            .ConfigureOptions<ConnectionOptionsValidator>()
            .AddOptions<ConnectionOptions>(name)
            .Configure(configureOptions);
        return services;
    }

    /// <summary>
    /// Add Rabbit Mq Client Options.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqConnectionOptions(
        this IServiceCollection services,
        string name,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        configureOptions.Invoke(services
            .ConfigureOptions<ConnectionOptionsValidator>()
            .AddOptions<ConnectionOptions>(name));
        return services;
    }

    /// <summary>
    /// Add Rabbit Mq Client Options.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqConnectionOptions(
        this IServiceCollection services,
        string name,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        services
            .ConfigureOptions<ConnectionOptionsValidator>()
            .AddOptions<ConnectionOptions>(name)
            .BindConfiguration(configSectionPath);
        return services;
    }

    /// <summary>
    /// Register a Service with Dependency Injection Container.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="name">The name.</param>
    /// <param name="factory">The factory.</param>
    /// <typeparam name="TService">The service type.</typeparam>
    /// <returns>The updated service collection.</returns>
    internal static IServiceCollection AddService<TService>(this IServiceCollection services, string name, Func<IServiceProvider, object?, TService> factory)
        where TService : class
    {
#if NET8_0_OR_GREATER
        return services.AddKeyedSingleton(name, factory);
#else
        return services.AddSingletonKeyedService(name, factory);
#endif
    }
}
