namespace Smart.Avalonia.Interactivity.Resolver;

using global::Avalonia.Markup.Xaml;

using Smart.Mvvm.Resolver;

public sealed class ResolveExtension : MarkupExtension
{
    [ConstructorArgument("type")]
    public Type Type { get; set; } = default!;

    public ResolveExtension()
    {
    }

    public ResolveExtension(Type type)
    {
        Type = type;
    }

    public override object ProvideValue(IServiceProvider serviceProvider) => ResolveProvider.Default.GetService(Type)!;
}
