namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;

public sealed class NullToParameterConverterTests
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void ConvertNullValueReturnsParameter()
    {
        // Arrange
        var converter = new NullToParameterConverter();

        // Act
        var result = converter.Convert(null, typeof(object), "param", Culture);

        // Assert
        Assert.Equal("param", result);
    }

    [Fact]
    public void ConvertNonNullValueReturnsValue()
    {
        // Arrange
        var converter = new NullToParameterConverter();

        // Act
        var result = converter.Convert("value", typeof(object), "param", Culture);

        // Assert
        Assert.Equal("value", result);
    }

    [Fact]
    public void ConvertInvertNullReturnsValue()
    {
        // Arrange
        var converter = new NullToParameterConverter { Invert = true };

        // Act
        // value is null, Invert=true: return value (null)
        var result = converter.Convert(null, typeof(object), "param", Culture);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ConvertInvertNonNullReturnsParameter()
    {
        // Arrange
        var converter = new NullToParameterConverter { Invert = true };

        // Act
        var result = converter.Convert("value", typeof(object), "param", Culture);

        // Assert
        Assert.Equal("param", result);
    }

    [Fact]
    public void ConvertEmptyStringWithHandleEmptyString()
    {
        // Arrange
        var converter = new NullToParameterConverter { HandleEmptyString = true };

        // Act
        var result = converter.Convert(string.Empty, typeof(object), "param", Culture);

        // Assert
        Assert.Equal("param", result);
    }

    [Fact]
    public void ConvertBackReturnsUnsetValue()
    {
        // Arrange
        var converter = new NullToParameterConverter();

        // Act
        var result = converter.ConvertBack("anything", typeof(object), null, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }
}
