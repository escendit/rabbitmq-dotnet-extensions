~~# NuGet Package: Escendit.Orleans.Clients.RabbitMQ.AmqpProtocol

Escendit.Orleans.Clients.RabbitMQ.AmqpProtocol is a NuGet package that provides the ability to register
`IConnectionFactory` or `IConnection`. This package is suitable for both Worker (Console)
and Web Applications, allowing you to easily configure and manage RabbitMQ connections within
your Orleans-based projects.

## Installation

To install Escendit.Orleans.Clients.RabbitMQ.AmqpProtocol, run the following command in the Package Manager Console:

```powershell
Install-Package Escendit.Orleans.Clients.RabbitMQ.AmqpProtocol
```

## Usage

There are several ways to register contracts that can be used in an application:

### Host

#### Register and use `IConnectionFactory` with the newly named `ConnectionOptions` registration.

```csharp
Host
    .CreateDefaultBuilder()
    .AddRabbitMqConnectionFactory("name", ...)
```

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<IConnectionFactory>("name");
```

#### Register and use `IConnectionFactory` with the existing `ConnectionOptions` registration.


```csharp
Host
    .CreateDefaultBuilder()
    .AddRabbitMqConnectionFactoryFromOption("name", "existing_name")
```

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<IConnectionFactory>("name");
```

#### Register and use `IConnection` with the newly named `ConnectionOptions` and `IConnectionFactory` registration.

```csharp
Host
    .CreateDefaultBuilder()
    .AddRabbitMqConnection("name", ...)
```

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<IConnection>("name");
```

#### Register and use `IConnection` with the existing `IConnectionFactory` registration.

```csharp
Host
    .CreateDefaultBuilder()
    .AddRabbitMqConnectionFromFactory("name", "existing_name")
```

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<IConnection>("name");
```

### Web Application

#### Register and use `IConnectionFactory` with the newly named `ConnectionOptions` registration.

```csharp
WebApplication
    .CreateBuilder()
    .AddRabbitMqConnectionFactory("name", ...)
```

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<IConnectionFactory>("name");
```

#### Register and use `IConnectionFactory` with the existing `ConnectionOptions` registration.


```csharp
WebApplication
    .CreateBuilder()
    .AddRabbitMqConnectionFactoryFromOption("name", "existing_name")
```

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<IConnectionFactory>("name");
```

#### Register and use `IConnection` with the newly named `ConnectionOptions` and `IConnectionFactory` registration.

```csharp
WebApplication
    .CreateBuilder()
    .AddRabbitMqConnection("name", ...)
```

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<IConnection>("name");
```

#### Register and use `IConnection` with the existing `IConnectionFactory` registration.

```csharp
WebApplication
    .CreateBuilder()
    .AddRabbitMqConnectionFromFactory("name", "existing_name")
```

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<IConnection>("name");
```

## Contributing

If you'd like to contribute to Escendit.Orleans.Clients.RabbitMQ,
please fork the repository and make changes as you'd like.
Pull requests are warmly welcome.~~
