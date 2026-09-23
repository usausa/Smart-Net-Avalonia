namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;
using global::Avalonia.Data;

public sealed class ToLowerUpperConverterTests
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
    public void ToLowerConvertBackReturnsDoNothing()
    {
        // Arrange
        var converter = new ToLowerConverter();

        // Act
        var result = converter.ConvertBack("hello", typeof(string), null, Culture);

        // Assert
        Assert.Equal(BindingOperations.DoNothing, result);
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
    public void ToUpperConvertBackReturnsDoNothing()
    {
        // Arrange
        var converter = new ToUpperConverter();

        // Act
        var result = converter.ConvertBack("HELLO", typeof(string), null, Culture);

        // Assert
        Assert.Equal(BindingOperations.DoNothing, result);
    }
}
