// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.Orleans.Clients.RabbitMQ.Abstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Endpoint.
/// </summary>
[DynamicallyAccessedMembers(
    DynamicallyAccessedMemberTypes.All)]
public class Endpoint
{
    /// <summary>
    /// Gets or sets the host name.
    /// </summary>
    /// <value>The host name.</value>
    public string HostName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the port.
    /// </summary>
    /// <value>The port.</value>
    public int? Port { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{HostName}:{Port ?? -1}";
    }
}
