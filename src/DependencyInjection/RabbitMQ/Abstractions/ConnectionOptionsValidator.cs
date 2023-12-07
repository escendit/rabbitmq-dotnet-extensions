// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

#pragma warning disable CA1812

namespace Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions;

using Microsoft.Extensions.Options;

/// <summary>
/// Connection Options Validator.
/// </summary>
internal class ConnectionOptionsValidator : IValidateOptions<ConnectionOptions>
{
    /// <inheritdoc />
    public ValidateOptionsResult Validate(string? name, ConnectionOptions options)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(options);

        var errors = new List<string>();

        if (options.Endpoints.Count == 0)
        {
            errors.Add($"Expecting at least one endpoint for '{name}'");
        }

        return errors.Any()
            ? ValidateOptionsResult.Fail(errors)
            : ValidateOptionsResult.Success;
    }
}
