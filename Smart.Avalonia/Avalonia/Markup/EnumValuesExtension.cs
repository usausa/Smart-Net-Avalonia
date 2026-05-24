namespace Smart.Avalonia.Markup;

using System.Diagnostics.CodeAnalysis;

using global::Avalonia.Markup.Xaml;

public sealed class EnumValuesExtension : MarkupExtension
{
    public Type Type { get; set; }

    public EnumValuesExtension(Type type)
    {
        Type = type;
    }

    [UnconditionalSuppressMessage("AOT", "IL3050", Justification = "Enum.GetValues(Type) is used at runtime. Use Enum.GetValues<TEnum>() for AOT-safe usage where possible.")]
    public override object ProvideValue(IServiceProvider serviceProvider) => Enum.GetValues(Type);
}
