# RabbitMQ Client Extensions for .NET and Orleans

Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions is a NuGet package that provides 
the ability to register connection options for RabbitMQ protocols.

## Installation

To install Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions, run the following command in the Package Manager Console:

```powershell
Install-Package Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions
```

## Usage

Escendit.Extensions.DependencyInjection.RabbitMQ.Abstractions package is not intended to be used as a standalone package.
It is meant to be used in conjunction with either:

### Service Collection
- Escendit.Extensions.DependencyInjection.RabbitMQ.AmqpProtocol
- Escendit.Extensions.DependencyInjection.RabbitMQ.StreamProtocol

### Hosting
- Escendit.Extensions.Hosting.RabbitMQ.AmqpProtocol
- Escendit.Extensions.Hosting.RabbitMQ.StreamProtocol


### ASP.NET Core
- Escendit.AspNetCore.Builder.RabbitMQ.AmqpProtocol
- Escendit.AspNetCore.Builder.RabbitMQ.StreamProtocol

There are several ways how to register connection options.

## Dependency Injection

### Register & Use with Default Options

```csharp
services
    .AddRabbitMqConnectionOptionsAsDefault(...)
```

#### .NET 8
```csharp
var connectionOptions = serviceProvider.GetRequiredKeyedService<ConnectionOptions>(ConnectionOptions.DefaultKey);
```

#### .NET 7
```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByKey<object?, ConnectionOptions>(ConnectionOptions.DefaultKey);
```

### Register & Use with Named Options

```csharp
services
    .AddRabbitMqConnectionOptions("name", ...)
```

#### .NET 8
```csharp
var connectionOptions = serviceProvider.GetRequiredKeyedService<ConnectionOptions>("name");
```

#### .NET 7
```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByKey<object?, ConnectionOptions>("name");
```

## Contributing

If you'd like to contribute to rabbitmq-dotnet-extensions,
please fork the repository and make changes as you'd like.
Pull requests are warmly welcome.
