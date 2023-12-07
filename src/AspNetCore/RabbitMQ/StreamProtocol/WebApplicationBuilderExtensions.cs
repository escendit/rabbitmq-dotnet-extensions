// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Microsoft.AspNetCore.Builder;

using Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

/// <summary>
/// Web Application Builder Extensions.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Add Rabbit Mq Stream System As Default.
    /// </summary>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqStreamSystemAsDefault(
        this WebApplicationBuilder webApplicationBuilder,
        Action<ConnectionOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(ConnectionOptions.DefaultKey);
        ArgumentNullException.ThrowIfNull(configureOptions);
        webApplicationBuilder
            .Host
            .AddRabbitMqStreamSystemAsDefault(configureOptions);
        return webApplicationBuilder;
    }

    /// <summary>
    /// Add Rabbit Mq Stream System As Default.
    /// </summary>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqStreamSystemAsDefault(
        this WebApplicationBuilder webApplicationBuilder,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(ConnectionOptions.DefaultKey);
        ArgumentNullException.ThrowIfNull(configureOptions);
        webApplicationBuilder
            .Host
            .AddRabbitMqStreamSystemAsDefault(configureOptions);
        return webApplicationBuilder;
    }

    /// <summary>
    /// Add Rabbit Mq Stream System As Default.
    /// </summary>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqStreamSystemAsDefault(
        this WebApplicationBuilder webApplicationBuilder,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        webApplicationBuilder
            .Host
            .AddRabbitMqStreamSystemAsDefault(configSectionPath);
        return webApplicationBuilder;
    }

    /// <summary>
    /// Add Rabbit Mq Stream System From Options As Default.
    /// </summary>
    /// <remarks>
    /// It needs provided connection options.
    /// </remarks>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="optionName">The named option.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqStreamSystemFromOptionAsDefault(
        this WebApplicationBuilder webApplicationBuilder,
        string optionName)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(optionName);
        webApplicationBuilder
            .Host
            .AddRabbitMqStreamSystemFromOptionsAsDefault(optionName);
        return webApplicationBuilder;
    }

    /// <summary>
    /// Add Rabbit Mq Stream System.
    /// </summary>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqStreamSystem(
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
    /// Add Rabbit Mq Stream System.
    /// </summary>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqStreamSystem(
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
    /// Add Rabbit Mq Stream System.
    /// </summary>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqStreamSystem(
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
    /// Add Rabbit Mq Stream System From Option.
    /// </summary>
    /// <remarks>
    /// It needs provided connection options.
    /// </remarks>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="optionName">The named option.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqStreamSystemFromOption(
        this WebApplicationBuilder webApplicationBuilder,
        string name,
        string optionName)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(optionName);
        webApplicationBuilder
            .Host
            .AddRabbitMqStreamSystemFromOptions(name, optionName);
        return webApplicationBuilder;
    }
}
