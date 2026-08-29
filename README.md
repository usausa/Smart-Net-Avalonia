# Smart.Avalonia .NET - MVVM helper library for Avalonia

[![NuGet](https://img.shields.io/nuget/v/Usa.Smart.Avalonia.svg)](https://www.nuget.org/packages/Usa.Smart.Avalonia/)

## Features

* Basic converters.
* Observable commands.
* Actions, Behaviors and Triggers.
* Markup extensions.
* Messenger.
* Resolver(DI Container) integration.
* Base class for ViewModel.
* StyledProperty source generator.

## StyledProperty generator

Add `[StyledProperty]` to a partial property, and the `StyledProperty` field and the property implementation are generated.

```csharp
public partial class GaugeControl : Control
{
    [StyledProperty(DefaultValue = 0d, Coerce = nameof(CoerceLevel))]
    public partial double Level { get; set; }

    [StyledProperty(Inherits = true)]
    public partial string? Label { get; set; }

    private double CoerceLevel(double value) => Math.Clamp(value, 0d, 100d);
}
```

| Option | Note |
|-|-|
| `DefaultValue` | Default value of the property |
| `DefaultValueExpression` | Default value as an expression, for values that can not be written as a constant |
| `DefaultBindingMode` | `BindingMode` |
| `Inherits` | Whether the value is inherited |
| `EnableDataValidation` | Whether data validation is enabled |
| `Coerce` | Name of a `T` method with `(T value)` |
| `Validate` | Name of a `static bool` method with `(T value)` |

## NuGet

| Package | Note  |
|-|-|
| [![NuGet](https://img.shields.io/nuget/v/Usa.Smart.Avalonia.svg)](https://www.nuget.org/packages/Usa.Smart.Avalonia/) | Core libyrary |
| [![NuGet](https://img.shields.io/nuget/v/Usa.Smart.Avalonia.Extensions.svg)](https://www.nuget.org/packages/Usa.Smart.Avalonia.Extensions/) | Extension helpers |

## Link

* [Smart.Mvvm](https://github.com/usausa/Smart-Net-Mvvm)
* [Smart.Resolver](https://github.com/usausa/Smart-Net-Resolver)
* [Smart.Navigation](https://github.com/usausa/Smart-Net-Navigation)
