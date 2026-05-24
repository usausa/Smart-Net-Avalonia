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

    [UnconditionalSuppressMessage("Trimming", "IL2111", Justification = "Enum type is specified by user; they must ensure it is preserved")]
    [UnconditionalSuppressMessage("AOT", "IL3050", Justification = "Enum.GetValues(Type) uses dynamic code; use Enum.GetValues<TEnum>() for AOT-safe usage")]
    public override object ProvideValue(IServiceProvider serviceProvider) => Enum.GetValues(Type);
}
