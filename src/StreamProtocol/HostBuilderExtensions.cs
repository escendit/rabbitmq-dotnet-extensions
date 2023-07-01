// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.Orleans.Clients.RabbitMQ.StreamProtocol;

using System.Diagnostics.CodeAnalysis;
using System.Net;
using Abstractions;
using global::Orleans;
using global::Orleans.Runtime;
using global::RabbitMQ.Stream.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

/// <summary>
/// Host Builder Extensions.
/// </summary>
[DynamicallyAccessedMembers(
    DynamicallyAccessedMemberTypes.All)]
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
        ArgumentNullException.ThrowIfNull(ConnectionOptions.DefaultKey);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .AddRabbitMqConnectionOptions(ConnectionOptions.DefaultKey, configureOptions)
            .ConfigureServices(services =>
                services
                    .AddSingletonNamedService(ConnectionOptions.DefaultKey, CreateStreamSystem));
    }

    /// <summary>
    /// Add Rabbit Mq Stream System As Default.
    /// </summary>
    /// <param name="hostBuilder">The host builder.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqStreamSystemAsDefault(
        this IHostBuilder hostBuilder,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(ConnectionOptions.DefaultKey);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .AddRabbitMqConnectionOptions(ConnectionOptions.DefaultKey, configureOptions)
            .ConfigureServices(services => services
                .AddSingletonNamedService(ConnectionOptions.DefaultKey, CreateStreamSystem));
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
            .AddRabbitMqConnectionOptions(ConnectionOptions.DefaultKey, configSectionPath)
            .ConfigureServices(services => services
                .AddSingletonNamedService(ConnectionOptions.DefaultKey, CreateStreamSystem));
    }

    /// <summary>
    /// Add Rabbit Mq Stream System From Options As Default.
    /// </summary>
    /// <remarks>
    /// It needs provided connection options.
    /// </remarks>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="optionName">The named option.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqStreamSystemFromOptionAsDefault(
        this IHostBuilder hostBuilder,
        string optionName)
    {
        return hostBuilder
            .ConfigureServices(services => services
                .AddSingletonNamedService(ConnectionOptions.DefaultKey, (serviceProvider, _) =>
                    CreateStreamSystem(serviceProvider, optionName)));
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
            .AddRabbitMqConnectionOptions(name, configureOptions)
            .ConfigureServices(services =>
                services
                    .AddSingletonNamedService(name, CreateStreamSystem));
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
            .AddRabbitMqConnectionOptions(name, configureOptions)
            .ConfigureServices(services => services
                .AddSingletonNamedService(name, CreateStreamSystem));
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
            .AddRabbitMqConnectionOptions(name, configSectionPath)
            .ConfigureServices(services => services
                .AddSingletonNamedService(name, CreateStreamSystem));
    }

    /// <summary>
    /// Add Rabbit Mq Stream System From Option.
    /// </summary>
    /// <remarks>
    /// It needs provided connection options.
    /// </remarks>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="optionName">The named option.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddRabbitMqStreamSystemFromOption(
        this IHostBuilder hostBuilder,
        string name,
        string optionName)
    {
        return hostBuilder
            .ConfigureServices(services => services
                .AddSingletonNamedService(name, (serviceProvider, _) =>
                    CreateStreamSystem(serviceProvider, optionName)));
    }

    private static StreamSystem CreateStreamSystem(IServiceProvider serviceProvider, string name)
    {
        var options = serviceProvider.GetOptionsByName<ConnectionOptions>(name);
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
