// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Microsoft.AspNetCore.Builder;

using System.Diagnostics.CodeAnalysis;
using Escendit.Orleans.Clients.RabbitMQ.Abstractions;
using Escendit.Orleans.Clients.RabbitMQ.AmqpProtocol;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Orleans;
using Orleans.Runtime;
using RabbitMQ.Client;

/// <summary>
/// Web Application Builder Extensions.
/// </summary>
[DynamicallyAccessedMembers(
    DynamicallyAccessedMemberTypes.All)]
public static partial class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Add Rabbit Mq Connection.
    /// </summary>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqConnection(
        this WebApplicationBuilder webApplicationBuilder,
        string name,
        Action<ConnectionOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        webApplicationBuilder
            .Host
            .AddRabbitMqConnection(name, configureOptions);
        return webApplicationBuilder;
    }

    /// <summary>
    /// Add Rabbit Mq Connection.
    /// </summary>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqConnection(
        this WebApplicationBuilder webApplicationBuilder,
        string name,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        webApplicationBuilder
            .Host
            .AddRabbitMqConnection(name, configureOptions);
        return webApplicationBuilder;
    }

    /// <summary>
    /// Add Rabbit Mq Connection.
    /// </summary>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqConnection(
        this WebApplicationBuilder webApplicationBuilder,
        string name,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        webApplicationBuilder
            .Host
            .AddRabbitMqConnection(name, configSectionPath);
        return webApplicationBuilder;
    }

    /// <summary>
    /// Add Rabbit Mq Connection From Factory.
    /// </summary>
    /// <remarks>
    /// It needs provided connection options.
    /// </remarks>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="factoryName">The factory name.</param>
    /// <returns>The updated host builder.</returns>
    public static WebApplicationBuilder AddRabbitMqConnectionFromFactory(
        this WebApplicationBuilder webApplicationBuilder,
        string name,
        string factoryName)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(factoryName);
        webApplicationBuilder
            .Host
            .AddRabbitMqConnectionFromFactory(name, factoryName);
        return webApplicationBuilder;
    }
}
