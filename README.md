# Scrutor.Extensions
![alt text](https://ci.appveyor.com/api/projects/status/kp23f8fg0eqbxiyf?svg=true "Build status")
![alt text](https://img.shields.io/myget/scrutor-extensions/v/Scrutor.Extensions?color=%23 "MyGet status")

https://img.shields.io/myget/scrutor-extensions/v/Scrutor.Extensions?color=%23

This repo exposes Scrutors scanning logic with "simpler" Attributes that can be used to anotate classes which need to get registered to DI container (ServiceCollection).

## Usage
There is one attribute exposed called `[Export]`. Which has one parameter that denotes lifetime of annotated dependency (parameter is of type `ServiceLifetime`).

Below are examples of annotating services/classes that need to be registered:
```csharp
//example of scoped dependency
public interface IScopedService { }

[Export(ServiceLifetime.Scoped)]
public class ScopedService: IScopedService { }

//same thing can be done without lifetime specified since scoped is default lifetime that is applied
[Export]
public class ScopedService: IScopedService { }

//example of transient dependency
public interface ITransientService { }

[Export(ServiceLifetime.Transient)]
public class TransientService: ITransientService { }

//example of singleton dependency
public interface ISingletonService { }

[Export(ServiceLifetime.Singleton)]
public class SingletonService: ISingletonService { }
```

After anotating you need to add a call to `.RegisterDependencies()` extension method located on `IServiceCollection`. Example of that can be seen below:
```csharp
//Startup.cs
public void ConfigureService(IServiceCollection services)
{
    //...
    services.RegisterDependencies()
    
    //or you can also filter app dependencies by assembly

    services.RegisterDependencies(assembly => !assembly.FullName.Contains("Namespace"));
    //...
}
```