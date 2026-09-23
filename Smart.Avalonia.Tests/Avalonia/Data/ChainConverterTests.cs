namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;
using global::Avalonia.Data;

public sealed class ChainConverterTests
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void ConvertAppliesConvertersInOrder()
    {
        // Arrange
        var converter = new ChainConverter();
        converter.Converters.Add(new ToUpperConverter());
        converter.Converters.Add(new ToLowerConverter());

        // Act
        // ToUpper then ToLower => lower
        var result = converter.Convert("Hello", typeof(string), null, Culture);

        // Assert
        Assert.Equal("hello", result);
    }

    [Fact]
    public void ConvertEmptyChainPassesThrough()
    {
        // Arrange
        var converter = new ChainConverter();

        // Act
        var result = converter.Convert("value", typeof(string), null, Culture);

        // Assert
        Assert.Equal("value", result);
    }

    [Fact]
    public void ConvertBackAppliesConvertersInReverseOrder()
    {
        // Arrange
        var converter = new ChainConverter();
        converter.Converters.Add(new ToUpperConverter());
        converter.Converters.Add(new ToLowerConverter());

        // Act
        // ConvertBack: reverse order = ToLower ConvertBack first
        // ToLower.ConvertBack returns DoNothing, so the chain stops before ToUpper
        var result = converter.ConvertBack("HELLO", typeof(string), null, Culture);

        // Assert
        Assert.Equal(BindingOperations.DoNothing, result);
    }

    [Fact]
    public void ConvertStopsAtUnsetValue()
    {
        // Arrange
        var converter = new ChainConverter();
        converter.Converters.Add(new ToUpperConverter());
        converter.Converters.Add(new NullToTextConverter { NullValue = "null", NonNullValue = "set" });

        // Act
        // ToUpperConverter returns UnsetValue for a non-string, which must not reach NullToTextConverter
        var result = converter.Convert(1, typeof(string), null, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }

    [Fact]
    public void ConvertBackStopsAtDoNothing()
    {
        // Arrange
        var converter = new ChainConverter();
        converter.Converters.Add(new ObjectConvertConverter());
        converter.Converters.Add(new BoolToTextConverter { TrueValue = "ON", FalseValue = "OFF" });

        // Act
        // BoolToTextConverter.ConvertBack returns DoNothing, which must not reach ObjectConvertConverter
        var result = converter.ConvertBack("unknown", typeof(int), null, Culture);

        // Assert
        Assert.Equal(BindingOperations.DoNothing, result);
    }
}
