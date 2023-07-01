// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.Orleans.Clients.RabbitMQ.Abstractions;

using System.Diagnostics.CodeAnalysis;
using global::Orleans;

/// <summary>
/// Connection Options.
/// </summary>
[DynamicallyAccessedMembers(
    DynamicallyAccessedMemberTypes.All)]
public class ConnectionOptions
{
    /// <summary>
    /// Default Key for Named Default.
    /// </summary>
    public const string DefaultKey = "Default";

    /// <summary>
    /// Gets the endpoints.
    /// </summary>
    /// <value>The endpoints.</value>
    public IList<Endpoint> Endpoints { get; init; } = new List<Endpoint>();

    /// <summary>
    /// Gets or sets the virtual host.
    /// </summary>
    /// <value>The virtual host.</value>
    public string? VirtualHost { get; set; }

    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    /// <value>The username.</value>
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    /// <value>The password.</value>
    [Redact]
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the heartbeat.
    /// </summary>
    /// <value>The heartbeat.</value>
    public TimeSpan Heartbeat { get; set; } = TimeSpan.FromMinutes(1);

    /// <summary>
    /// Gets or sets the ssl options.
    /// </summary>
    /// <value>The ssl options.</value>
    public SslOptions? SslOptions { get; set; } = new();

    /// <summary>
    /// Gets or sets the client provided name.
    /// </summary>
    /// <value>The client provided name.</value>
    public string? ClientProvidedName { get; set; } = typeof(ConnectionOptions).Namespace;
}
