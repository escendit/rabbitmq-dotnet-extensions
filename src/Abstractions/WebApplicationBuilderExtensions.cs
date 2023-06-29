// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Microsoft.AspNetCore.Builder;

using System.Diagnostics.CodeAnalysis;
using Escendit.Orleans.Clients.RabbitMQ.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

/// <summary>
/// Web Application Builder Extensions.
/// </summary>
[DynamicallyAccessedMembers(
    DynamicallyAccessedMemberTypes.All)]
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Add Rabbit Mq Connection Options As Default.
    /// </summary>
    /// <param name="webApplicationBuilder">The web initial application builder.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqConnectionOptionsAsDefault(
        this WebApplicationBuilder webApplicationBuilder,
        Action<ConnectionOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(configureOptions);
        webApplicationBuilder
            .Services
            .ConfigureOptions<ConnectionOptionsValidator>()
            .AddOptions<ConnectionOptions>(ConnectionOptions.DefaultKey)
            .Configure(configureOptions);
        return webApplicationBuilder;
    }

    /// <summary>
    /// Add Rabbit Mq Connection Options As Default.
    /// </summary>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqConnectionOptionsAsDefault(
        this WebApplicationBuilder webApplicationBuilder,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(configureOptions);
        webApplicationBuilder
            .Services
            .ConfigureOptions<ConnectionOptionsValidator>()
            .AddOptions<ConnectionOptions>(ConnectionOptions.DefaultKey);
        return webApplicationBuilder;
    }

    /// <summary>
    /// Add Rabbit Mq Connection Options As Default.
    /// </summary>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqConnectionOptionsAsDefault(
        this WebApplicationBuilder webApplicationBuilder,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        webApplicationBuilder
            .Services
            .ConfigureOptions<ConnectionOptionsValidator>()
            .AddOptions<ConnectionOptions>(ConnectionOptions.DefaultKey)
            .BindConfiguration(configSectionPath);
        return webApplicationBuilder;
    }

    /// <summary>
    /// Add Rabbit Mq Connection Options.
    /// </summary>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqConnectionOptions(
        this WebApplicationBuilder webApplicationBuilder,
        string name,
        Action<ConnectionOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        webApplicationBuilder
            .Services
            .ConfigureOptions<ConnectionOptionsValidator>()
            .AddOptions<ConnectionOptions>(name)
            .Configure(configureOptions);
        return webApplicationBuilder;
    }

    /// <summary>
    /// Add Rabbit Mq Connection Options.
    /// </summary>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqConnectionOptions(
        this WebApplicationBuilder webApplicationBuilder,
        string name,
        Action<OptionsBuilder<ConnectionOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        configureOptions
            .Invoke(webApplicationBuilder
                .Services
                .ConfigureOptions<ConnectionOptionsValidator>()
                .AddOptions<ConnectionOptions>(name));
        return webApplicationBuilder;
    }

    /// <summary>
    /// Add Rabbit Mq Connection Options.
    /// </summary>
    /// <param name="webApplicationBuilder">The initial web application builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRabbitMqConnectionOptions(
        this WebApplicationBuilder webApplicationBuilder,
        string name,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        webApplicationBuilder
            .Services
            .ConfigureOptions<ConnectionOptionsValidator>()
            .AddOptions<ConnectionOptions>(name)
            .BindConfiguration(configSectionPath);
        return webApplicationBuilder;
    }
}
