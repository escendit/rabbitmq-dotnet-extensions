// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Microsoft.Extensions.DependencyInjection;

using System.Net;
using Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions;
using global::RabbitMQ.Stream.Client;
using Logging;
using Options;

/// <summary>
/// Service Collection Extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
        /// <summary>
    /// Add Rabbit Mq Stream System As Default.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqStreamSystemAsDefault(
        this IServiceCollection services,
        Action<ConnectionOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(ConnectionOptions.DefaultKey);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return services
            .AddRabbitMqConnectionOptions(ConnectionOptions.DefaultKey, configureOptions)
            .AddService(ConnectionOptions.DefaultKey, CreateStreamSystem);
    }

    /// <summary>
    /// Add Rabbit Mq Stream System As Default.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqStreamSystemAsDefault(
        this IServiceCollection services,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(ConnectionOptions.DefaultKey);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return services
            .AddRabbitMqConnectionOptions(ConnectionOptions.DefaultKey, configureOptions)
            .AddService(ConnectionOptions.DefaultKey, CreateStreamSystem);
    }

    /// <summary>
    /// Add Rabbit Mq Stream System As Default.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqStreamSystemAsDefault(
        this IServiceCollection services,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        return services
            .AddRabbitMqConnectionOptions(ConnectionOptions.DefaultKey, configSectionPath)
            .AddService(ConnectionOptions.DefaultKey, CreateStreamSystem);
    }

    /// <summary>
    /// Add Rabbit Mq Stream System From Options As Default.
    /// </summary>
    /// <remarks>
    /// It needs provided connection options.
    /// </remarks>
    /// <param name="services">The initial service collection.</param>
    /// <param name="optionsName">The named option.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqStreamSystemFromOptionAsDefault(
        this IServiceCollection services,
        string optionsName)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(optionsName);
        return services
            .AddService(ConnectionOptions.DefaultKey, (serviceProvider, _) =>
                CreateStreamSystem(serviceProvider, optionsName));
    }

    /// <summary>
    /// Add Rabbit Mq Stream System.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqStreamSystem(
        this IServiceCollection services,
        string name,
        Action<ConnectionOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return services
            .AddRabbitMqConnectionOptions(name, configureOptions)
            .AddService(name, CreateStreamSystem);
    }

    /// <summary>
    /// Add Rabbit Mq Stream System.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqStreamSystem(
        this IServiceCollection services,
        string name,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return services
            .AddRabbitMqConnectionOptions(name, configureOptions)
            .AddService(name, CreateStreamSystem);
    }

    /// <summary>
    /// Add Rabbit Mq Stream System.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqStreamSystem(
        this IServiceCollection services,
        string name,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        return services
            .AddRabbitMqConnectionOptions(name, configSectionPath)
            .AddService(name, CreateStreamSystem);
    }

    /// <summary>
    /// Add Rabbit Mq Stream System From Option.
    /// </summary>
    /// <remarks>
    /// It needs provided connection options.
    /// </remarks>
    /// <param name="services">The initial service collection.</param>
    /// <param name="name">The name.</param>
    /// <param name="optionsName">The named option.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqStreamSystemFromOptions(
        this IServiceCollection services,
        string name,
        string optionsName)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(optionsName);
        return services
            .AddService(name, (serviceProvider, _) =>
                CreateStreamSystem(serviceProvider, optionsName));
    }

    private static StreamSystem CreateStreamSystem(IServiceProvider serviceProvider, object? name)
    {
        if (name is not string stringedName)
        {
            throw new ArgumentException("Invalid name");
        }

        var monitor = serviceProvider.GetRequiredService<IOptionsMonitor<ConnectionOptions>>();
        var options = monitor.Get(stringedName);
        var config = CreateStreamSystemConfig(options);
        return CreateStreamSystemInternal(serviceProvider, config);
    }

    private static StreamSystem CreateStreamSystemInternal(
        IServiceProvider serviceProvider,
        StreamSystemConfig streamSystemConfig)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<StreamSystem>>();
        return StreamSystem
            .Create(streamSystemConfig, logger)
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();
    }

    private static StreamSystemConfig CreateStreamSystemConfig(ConnectionOptions options)
    {
        return new StreamSystemConfig
        {
            Endpoints = options
                .Endpoints
                .Select(endpoint => new DnsEndPoint(endpoint.HostName, endpoint.Port ?? 5552))
                .Cast<EndPoint>()
                .ToList(),
            Heartbeat = options.Heartbeat,
            Password = options.Password,
            UserName = options.UserName,
            VirtualHost = options.VirtualHost,
            ClientProvidedName = options.ClientProvidedName,
            Ssl = options.SslOptions is null
                ? new()
                : new SslOption
                {
                    AcceptablePolicyErrors = options.SslOptions.AcceptablePolicyErrors,
                    ServerName = options.SslOptions.ServerName,
                    CertificateSelectionCallback = options.SslOptions.CertificateSelectionCallback,
                    CertificateValidationCallback = options.SslOptions.CertificateValidationCallback,
                    CertPassphrase = options.SslOptions.CertPassphrase,
                    CertPath = options.SslOptions.CertPath,
                    Certs = options.SslOptions.Certificates,
                    CheckCertificateRevocation = options.SslOptions.CheckCertificateRevocation,
                    Enabled = options.SslOptions.Enabled,
                    Version = options.SslOptions.Version,
                },
        };
    }
}
