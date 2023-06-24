// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.Orleans.Clients.RabbitMQ.AmqpProtocol;

using System.Diagnostics.CodeAnalysis;
using Abstractions;
using global::Orleans;
using global::Orleans.Runtime;
using global::RabbitMQ.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

/// <summary>
/// Host Builder Extensions.
/// </summary>
[DynamicallyAccessedMembers(
    DynamicallyAccessedMemberTypes.All)]
public static partial class HostBuilderExtensions
{
    private const string ClientPropertyOptionsNameKey = "x-dotnet-options-name";

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
            .AddRabbitMqConnectionOptions(name, configureOptions)
            .ConfigureServices(services =>
                services
                    .AddSingletonNamedService(name, CreateConnectionFactory));
    }

    /// <summary>
    /// Add Rabbit Mq Stream Client.
    /// </summary>
    /// <param name="hostBuilder">The host builder.</param>
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
            .AddRabbitMqConnectionOptions(name, configureOptions)
            .ConfigureServices(services => services
                .AddSingletonNamedService(name, CreateConnectionFactory));
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
            .AddRabbitMqConnectionOptions(name, configSectionPath)
            .ConfigureServices(services => services
                .AddSingletonNamedService(name, CreateConnectionFactory));
    }

    /// <summary>
    /// Add Rabbit Mq Stream Client.
    /// </summary>
    /// <remarks>
    /// It needs provided connection options.
    /// </remarks>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="optionName">The named option.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqConnectionFactoryFromOption(
        this IHostBuilder hostBuilder,
        string name,
        string optionName)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(optionName);
        return hostBuilder
            .ConfigureServices(services => services
                .AddSingletonNamedService(name, (serviceProvider, _) =>
                    CreateConnectionFactory(serviceProvider, optionName)));
    }

    private static IConnectionFactory CreateConnectionFactory(
        IServiceProvider serviceProvider,
        string name)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(name);
        var options = serviceProvider.GetOptionsByName<ConnectionOptions>(name);
        return CreateConnectionFactoryInternal(serviceProvider, name, options);
    }

    private static IConnectionFactory CreateConnectionFactoryInternal(
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
            UseBackgroundThreadsForIO = true,
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
                { ClientPropertyOptionsNameKey, name },
            },
        };
    }
}
