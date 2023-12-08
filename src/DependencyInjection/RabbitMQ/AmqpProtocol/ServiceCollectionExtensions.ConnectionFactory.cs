// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Microsoft.Extensions.DependencyInjection;

using Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions;
using Options;
using RabbitMQ.Client;
using Constants = Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions.Constants;

/// <summary>
/// Service Collection Extensions.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Rabbit Mq Connection Factory.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqConnectionFactory(
        this IServiceCollection services,
        string name,
        Action<ConnectionOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        services
            .AddRabbitMqConnectionOptions(name, configureOptions)
            .AddService(name, CreateConnectionFactory);
        return services;
    }

    /// <summary>
    /// Add Rabbit Mq Stream Client.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqConnectionFactory(
        this IServiceCollection services,
        string name,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return services
            .AddRabbitMqConnectionOptions(name, configureOptions)
            .AddService(name, CreateConnectionFactory);
    }

    /// <summary>
    /// Add Rabbit Mq Stream Client.
    /// </summary>
    /// <param name="services">The initial service collection.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqConnectionFactory(
        this IServiceCollection services,
        string name,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        return services
            .AddRabbitMqConnectionOptions(name, configSectionPath)
            .AddService(name, CreateConnectionFactory);
    }

    /// <summary>
    /// Add Rabbit Mq Stream Client.
    /// </summary>
    /// <remarks>
    /// It needs provided connection options.
    /// </remarks>
    /// <param name="services">The initial service collection.</param>
    /// <param name="name">The name.</param>
    /// <param name="optionsName">The named option.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRabbitMqConnectionFactoryFromOptions(
        this IServiceCollection services,
        string name,
        string optionsName)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(optionsName);
        return services
            .AddService(name, (serviceProvider, _) =>
                CreateConnectionFactory(serviceProvider, optionsName));
    }

    private static IConnectionFactory CreateConnectionFactory(
        IServiceProvider serviceProvider,
        object? name)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(name);
        if (name is not string stringedName)
        {
            throw new ArgumentException("Invalid name");
        }

        var monitor = serviceProvider.GetRequiredService<IOptionsMonitor<ConnectionOptions>>();
        var options = monitor.Get(stringedName);
        return CreateConnectionFactoryInternal(serviceProvider, stringedName, options);
    }

    private static ConnectionFactory CreateConnectionFactoryInternal(
        IServiceProvider serviceProvider,
        string name,
        ConnectionOptions options)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(options);
        return new ConnectionFactory
        {
            Password = options.Password,
            UserName = options.UserName,
            VirtualHost = options.VirtualHost,
            DispatchConsumersAsync = true,
            Ssl = options.SslOptions is null
                ? null
                : new SslOption
                {
                    AcceptablePolicyErrors = options.SslOptions.AcceptablePolicyErrors,
                    Certs = options.SslOptions.Certificates,
                    Enabled = options.SslOptions.Enabled,
                    Version = options.SslOptions.Version,
                    CertPassphrase = options.SslOptions.CertPassphrase,
                    CertPath = options.SslOptions.CertPath,
                    ServerName = options.SslOptions.ServerName,
                    CertificateSelectionCallback = options.SslOptions.CertificateSelectionCallback,
                    CertificateValidationCallback = options.SslOptions.CertificateValidationCallback,
                },
            ClientProperties = new Dictionary<string, object>
            {
                { Constants.ClientPropertyOptionsNameKey, name },
            },
        };
    }
}
