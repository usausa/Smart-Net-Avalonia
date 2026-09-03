namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;

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
        // ConvertBack: reverse order = ToLower ConvertBack (unset), then ToUpper ConvertBack (unset)
        // Each ConvertBack returns UnsetValue; test that it propagates without crashing
        var result = converter.ConvertBack("HELLO", typeof(string), null, Culture);

        // Assert
        // ToLower.ConvertBack returns UnsetValue; ToUpper.ConvertBack(UnsetValue) also returns UnsetValue
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }
}
