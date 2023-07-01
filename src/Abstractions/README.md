# NuGet Package: Escendit.Orleans.Clients.RabbitMQ.Abstractions

Escendit.Orleans.Clients.RabbitMQ.Abstractions is a NuGet package that provides the ability to register
connection options for RabbitMQ protocols. This package is suitable for both Worker (Console)
and Web Applications, allowing you to easily configure and manage RabbitMQ connections within
your Orleans-based projects.

## Installation

To install Escendit.Orleans.Clients.RabbitMQ.Abstractions, run the following command in the Package Manager Console:

```powershell
Install-Package Escendit.Orleans.Clients.RabbitMQ.Abstractions
```

## Usage

Escendit.Orleans.Clients.RabbitMQ.Abstractions package is not intended to be used as a standalone package.
It is meant to be used in conjunction with either:
- Escendit.Orleans.Clients.RabbitMQ.StreamProtocol
- Escendit.Orleans.Clients.RabbitMQ.AmqpProtocol.

There are several ways how to register connection options.

### Host

#### Register & Use with Default Options

```csharp
Host
    .CreateDefaultBuilder()
    .AddRabbitMqConnectionOptionsAsDefault(...)
```

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<ConnectionOptions>(ConnectionOptions.DefaultKey);
```

#### Register & Use with Named Options

```csharp
Host
    .CreateDefaultBuilder()
    .AddRabbitMqConnectionOptions("name", ...)
```

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<ConnectionOptions>("name");
```

### Web Application

#### Register & Use with Default Options

```csharp
WebApplication
    .CreateBuilder()
    .AddRabbitMqConnectionOptionsAsDefault(...)
```

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<ConnectionOptions>(ConnectionOptions.DefaultKey);
```

#### Register & Use with Named Options

```csharp
WebApplication
    .CreateBuilder()
    .AddRabbitMqConnectionOptions("name", ...)
```

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<ConnectionOptions>("name");
```

## Contributing

If you'd like to contribute to Escendit.Orleans.Clients.RabbitMQ,
please fork the repository and make changes as you'd like.
Pull requests are warmly welcome.
