# RabbitMQ Client Extensions for .NET and Orleans

Escendit.AspNetCore.Builder.RabbitMQ.AmqpProtocol is a NuGet package that provides the ability to register
`StreamSystem`.

## Installation

To install Escendit.AspNetCore.Builder.RabbitMQ.AmqpProtocol, run the following command in the Package Manager Console:

```powershell
Install-Package Escendit.AspNetCore.Builder.RabbitMQ.AmqpProtocol
```

## Usage

There are several ways to register contracts that can be used in an application:

### Register and use `IConnectionFactory` with the newly named `ConnectionOptions` registration.

```csharp
WebApplication
    .CreateBuilder()
    .AddRabbitMqConnectionFactory("name", ...)
```

#### NET 8
```csharp
var connectionFactory = serviceProvider.GetRequiredKeyedService<IConnectionFactory>("name");
```

#### NET 7
```csharp
var connectionFactory = serviceProvider.GetRequiredServiceByKey<object?, IConnectionFactory>("name");
```

### Register and use `IConnectionFactory` with the existing `ConnectionOptions` registration.


```csharp
WebApplication
    .CreateBuilder()
    .AddRabbitMqConnectionFactoryFromOption("name", "existing_name")
```

#### NET 8
```csharp
var connectionFactory = serviceProvider.GetRequiredKeyedService<IConnectionFactory>("name");
```

#### NET 7
```csharp
var connectionFactory = serviceProvider.GetRequiredServiceByKey<object?, IConnectionFactory>("name");
```

### Register and use `IConnection` with the newly named `ConnectionOptions` and `IConnectionFactory` registration.

```csharp
WebApplication
    .CreateBuilder()
    .AddRabbitMqConnection("name", ...)
```

#### NET 8
```csharp
var connection = serviceProvider.GetRequiredKeyedService<IConnection>("name");
```

#### NET 7
```csharp
var connection = serviceProvider.GetRequiredServiceByKey<object?, IConnection>("name");
```

### Register and use `IConnection` with the existing `IConnectionFactory` registration.

```csharp
WebApplication
    .CreateBuilder()
    .AddRabbitMqConnectionFromFactory("name", "existing_name")
```

#### NET 8
```csharp
var connection = serviceProvider.GetRequiredKeyedService<IConnection>("name");
```

#### NET 7
```csharp
var connection = serviceProvider.GetRequiredServiceByKey<object?, IConnection>("name");
```

## Contributing

If you'd like to contribute to rabbitmq-dotnet-extensions,
please fork the repository and make changes as you'd like.
Pull requests are warmly welcome.
