# NuGet Package: Escendit.Orleans.Clients.RabbitMQ.StreamProtocol

Escendit.Orleans.Clients.RabbitMQ.StreamProtocol is a NuGet package that provides the ability to register
`StreamSystem`. This package is suitable for both Worker (Console)
and Web Applications, allowing you to easily configure and manage RabbitMQ connections within
your Orleans-based projects.

## Installation

To install Escendit.Orleans.Clients.RabbitMQ.StreamProtocol, run the following command in the Package Manager Console:

```powershell
Install-Package Escendit.Orleans.Clients.RabbitMQ.StreamProtocol
```

## Usage

There are several ways to register contracts that can be used in an application:

### Host

#### Register and use default `StreamSystem` with the newly named `ConnectionOptions` registration.

```csharp
Host
    .CreateDefaultBuilder()
    .AddRabbitMqStreamSystemAsDefault(...)
```

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<StreamSystem>("Default");
```

#### Register and use `StreamSystem` with the newly named `ConnectionOptions` registration.

```csharp
Host
    .CreateDefaultBuilder()
    .AddRabbitMqStreamSystem("name", ...)
```

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<StreamSystem>("name");
```

### Web Application

#### Register and use default `StreamSystem` with the newly named `ConnectionOptions` registration.

```csharp
WebApplication
    .CreateBuilder()
    .AddRabbitMqStreamSystemAsDefault(...)
```

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<StreamSystem>("Default");
```

#### Register and use `StreamSystem` with the newly named `ConnectionOptions` registration.

```csharp
WebApplication
    .CreateBuilder()
    .AddRabbitMqStreamSystem("name", ...)
```

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<StreamSystem>("name");
```

## Contributing

If you'd like to contribute to Escendit.Orleans.Clients.RabbitMQ,
please fork the repository and make changes as you'd like.
Pull requests are warmly welcome.
