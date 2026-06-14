namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;

public sealed class NullToObjectConverterTest
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void ConvertNullReturnsNullValue()
    {
        // Arrange
        var converter = new NullToTextConverter { NullValue = "null", NonNullValue = "set" };

        // Act
        var result = converter.Convert(null, typeof(string), null, Culture);

        // Assert
        Assert.Equal("null", result);
    }

    [Fact]
    public void ConvertNonNullReturnsNonNullValue()
    {
        // Arrange
        var converter = new NullToTextConverter { NullValue = "null", NonNullValue = "set" };

        // Act
        var result = converter.Convert("something", typeof(string), null, Culture);

        // Assert
        Assert.Equal("set", result);
    }

    [Fact]
    public void ConvertEmptyStringWithHandleEmptyString()
    {
        // Arrange
        var converter = new NullToTextConverter
        {
            NullValue = "null",
            NonNullValue = "set",
            HandleEmptyString = true
        };

        // Act
        var result = converter.Convert(string.Empty, typeof(string), null, Culture);

        // Assert
        Assert.Equal("null", result);
    }

    [Fact]
    public void ConvertEmptyStringWithoutHandleEmptyString()
    {
        // Arrange
        var converter = new NullToTextConverter
        {
            NullValue = "null",
            NonNullValue = "set",
            HandleEmptyString = false
        };

        // Act
        var result = converter.Convert(string.Empty, typeof(string), null, Culture);

        // Assert
        Assert.Equal("set", result);
    }

    [Fact]
    public void ConvertBackReturnsUnsetValue()
    {
        // Arrange
        var converter = new NullToTextConverter { NullValue = "null", NonNullValue = "set" };

        // Act
        var result = converter.ConvertBack("anything", typeof(string), null, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }

    [Fact]
    public void NullToBoolDefaultNullIsFalse()
    {
        // Arrange
        var converter = new NullToBoolConverter();

        // Act & Assert
        Assert.Equal(false, converter.Convert(null, typeof(bool), null, Culture));
        Assert.Equal(true, converter.Convert("value", typeof(bool), null, Culture));
    }
}
