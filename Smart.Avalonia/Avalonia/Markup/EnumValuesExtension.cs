namespace Smart.Avalonia.Markup;

using global::Avalonia.Markup.Xaml;

public sealed class EnumValuesExtension : MarkupExtension
{
    public Type Type { get; set; }

    public EnumValuesExtension(Type type)
    {
        Type = type;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var values = Enum.GetValuesAsUnderlyingType(Type);
        var result = new object[values.Length];
        for (var i = 0; i < values.Length; i++)
        {
            result[i] = Enum.ToObject(Type, values.GetValue(i)!);
        }

        return result;
    }
}
