namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;

public sealed class ToLowerUpperConverterTest
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void ToLowerConvertsText()
    {
        // Arrange
        var converter = new ToLowerConverter();

        // Act
        var result = converter.Convert("HELLO", typeof(string), null, Culture);

        // Assert
        Assert.Equal("hello", result);
    }

    [Fact]
    public void ToLowerNonStringReturnsUnsetValue()
    {
        // Arrange
        var converter = new ToLowerConverter();

        // Act
        var result = converter.Convert(42, typeof(string), null, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }

    [Fact]
    public void ToLowerConvertBackReturnsUnsetValue()
    {
        // Arrange
        var converter = new ToLowerConverter();

        // Act
        var result = converter.ConvertBack("hello", typeof(string), null, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }

    [Fact]
    public void ToUpperConvertsText()
    {
        // Arrange
        var converter = new ToUpperConverter();

        // Act
        var result = converter.Convert("hello", typeof(string), null, Culture);

        // Assert
        Assert.Equal("HELLO", result);
    }

    [Fact]
    public void ToUpperNonStringReturnsUnsetValue()
    {
        // Arrange
        var converter = new ToUpperConverter();

        // Act
        var result = converter.Convert(42, typeof(string), null, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }

    [Fact]
    public void ToUpperConvertBackReturnsUnsetValue()
    {
        // Arrange
        var converter = new ToUpperConverter();

        // Act
        var result = converter.ConvertBack("HELLO", typeof(string), null, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }
}
