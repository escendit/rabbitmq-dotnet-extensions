# RabbitMQ Client Extensions for .NET and Orleans

Escendit.Extensions.Hosting.RabbitMQ.AmqpProtocol is a NuGet package that provides the ability to register
`IConnectionFactory` or `IConnection`. Utilize Host-based applications with this package.

## Installation

To install Escendit.Extensions.Hosting.RabbitMQ.AmqpProtocol, run the following command in the Package Manager Console:

```powershell
Install-Package Escendit.Extensions.Hosting.RabbitMQ.AmqpProtocol
```

## Usage

There are several ways to register contracts that can be used in an application:

### Register and use `IConnectionFactory` with the newly named `ConnectionOptions` registration.

```csharp
Host
    .CreateDefaultBuilder()
    .AddRabbitMqConnectionFactory("name", ...)
```

#### .NET 8
```csharp
var connectionFactory = serviceProvider.GetRequiredKeyedService<IConnectionFactory>("name");
```

#### .NET 7
```csharp
var connectionFactory = serviceProvider.GetRequiredServiceByKey<object?, IConnectionFactory>("name");
```

### Register and use `IConnectionFactory` with the existing `ConnectionOptions` registration.


```csharp
Host
    .CreateDefaultBuilder()
    .AddRabbitMqConnectionFactoryFromOption("name", "existing_name")
```

#### .NET 8
```csharp
var connectionFactory = serviceProvider.GetRequiredKeyedService<IConnectionFactory>("name");
```

#### .NET 7
```csharp
var connectionFactory = serviceProvider.GetRequiredServiceByKey<object?, IConnectionFactory>("name");
```

### Register and use `IConnection` with the newly named `ConnectionOptions` and `IConnectionFactory` registration.

```csharp
Host
    .CreateDefaultBuilder()
    .AddRabbitMqConnection("name", ...)
```

#### .NET 8
```csharp
var connection = serviceProvider.GetRequiredKeyedService<IConnection>("name");
```

#### .NET 7
```csharp
var connection = serviceProvider.GetRequiredServiceByName<object?, IConnection>("name");
```

### Register and use `IConnection` with the existing `IConnectionFactory` registration.

```csharp
Host
    .CreateDefaultBuilder()
    .AddRabbitMqConnectionFromFactory("name", "existing_name")
```

#### .NET 8
```csharp
var connection = serviceProvider.GetRequiredKeyedService<IConnection>("name");
```

#### .NET 7
```csharp
var connection = serviceProvider.GetRequiredServiceByName<object?, IConnection>("name");
```

## Contributing

If you'd like to contribute to rabbitmq-dotnet-extensions,
please fork the repository and make changes as you'd like.
Pull requests are warmly welcome.
