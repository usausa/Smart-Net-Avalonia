namespace Smart.Avalonia.Resolver;

using System.Diagnostics.CodeAnalysis;

using global::Avalonia.Markup.Xaml;
using global::Avalonia.Metadata;

using Smart.Mvvm.Resolver;

public sealed class ResolveExtension : MarkupExtension
{
    [ConstructorArgument("type")]
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public Type Type { get; set; } = default!;

    public ResolveExtension()
    {
    }

    public ResolveExtension([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type type)
    {
        Type = type;
    }

    public override object ProvideValue(IServiceProvider serviceProvider) => ResolveProvider.Default.GetService(Type)!;
}
