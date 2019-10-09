# Scrutor.Extensions
![alt text](https://ci.appveyor.com/api/projects/status/kp23f8fg0eqbxiyf?svg=true "Build status")

This repo exposes Scrutors scanning logic with actual Attributes that can be actually used to anotate classes which need to get registered to actual DI container (ServiceCollection).

## Usage
There is one attribute exposed called `[Export]`. Which has one parameter that denotes lifetime of annotated dependency (parameter is of type `ServiceLifetime`).

Below are example of annotating services/classes that need to be registered:
```c#
//example of scoped dependency
public interface IScopedService { }

[Export(ServiceLifetime.Scoped)]
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