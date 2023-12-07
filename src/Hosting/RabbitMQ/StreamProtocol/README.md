# RabbitMQ Client Extensions for .NET and Orleans

Escendit.Extensions.Hosting.RabbitMQ.StreamProtocol is a NuGet package that provides the ability to register
`StreamSystem`. Utilize Web Applications with this package.

## Installation

To install Escendit.Extensions.Hosting.RabbitMQ.StreamProtocol, run the following command in the Package Manager Console:

```powershell
Install-Package Escendit.Extensions.Hosting.RabbitMQ.StreamProtocol
```

## Usage

There are several ways to register contracts that can be used in an application:

### Register and use default `StreamSystem` with the newly named `ConnectionOptions` registration.

```csharp
Host
    .CreateDefaultBuilder()
    .AddRabbitMqStreamSystemAsDefault(...)
```

#### .NET 8
```csharp
var streamSystem = serviceProvider.GetRequiredKeyedService<StreamSystem>("Default");
```

#### .NET 7
```csharp
var streamSystem = serviceProvider.GetRequiredServiceByKey<object?, StreamSystem>("Default");
```

### Register and use `StreamSystem` with the newly named `ConnectionOptions` registration.

```csharp
Host
    .CreateDefaultBuilder()
    .AddRabbitMqStreamSystem("name", ...)
```

#### .NET 8
```csharp
var streamSystem = serviceProvider.GetRequiredKeyedService<StreamSystem>("name");
```

#### .NET 7
```csharp
var streamSystem = serviceProvider.GetRequiredServiceByKey<object?, StreamSystem>("name");
```

## Contributing

If you'd like to contribute to rabbitmq-dotnet-extensions,
please fork the repository and make changes as you'd like.
Pull requests are warmly welcome.
