﻿// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Microsoft.AspNetCore.Builder;

using System.Diagnostics.CodeAnalysis;
using Escendit.Orleans.Clients.RabbitMQ.Abstractions;
using Escendit.Orleans.Clients.RabbitMQ.StreamProtocol;
using Microsoft.Extensions.Options;

/// <summary>
/// Web Application Builder Extensions.
/// </summary>
[DynamicallyAccessedMembers(
    DynamicallyAccessedMemberTypes.All)]
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Add Rabbit Mq Stream Client.
    /// </summary>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqStreamClient(
        this WebApplicationBuilder webApplicationBuilder,
        string name,
        Action<ConnectionOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        webApplicationBuilder
            .Host
            .AddRabbitMqStreamSystem(name, configureOptions);
        return webApplicationBuilder;
    }

    /// <summary>
    /// Add Rabbit Mq Stream Client.
    /// </summary>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqStreamClient(
        this WebApplicationBuilder webApplicationBuilder,
        string name,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        webApplicationBuilder
            .Host
            .AddRabbitMqStreamSystem(name, configureOptions);
        return webApplicationBuilder;
    }

    /// <summary>
    /// Add Rabbit Mq Stream Client.
    /// </summary>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated host builder.</returns>
    public static WebApplicationBuilder AddRabbitMqStreamClient(
        this WebApplicationBuilder webApplicationBuilder,
        string name,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        webApplicationBuilder
            .Host
            .AddRabbitMqStreamSystem(name, configSectionPath);
        return webApplicationBuilder;
    }

    /// <summary>
    /// Add Rabbit Mq Stream Client.
    /// </summary>
    /// <remarks>
    /// It needs provided connection options.
    /// </remarks>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="optionName">The named option.</param>
    /// <returns>The updated host builder.</returns>
    public static WebApplicationBuilder AddRabbitMqStreamClientFromOption(
        this WebApplicationBuilder webApplicationBuilder,
        string name,
        string optionName)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(optionName);
        webApplicationBuilder
            .Host
            .AddRabbitMqStreamSystemFromOption(name, optionName);
        return webApplicationBuilder;
    }
}