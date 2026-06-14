namespace Smart.Avalonia.Data;

using System.Globalization;

public sealed class ReverseConverterTest
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void ConvertTrueReturnsFalse()
    {
        // Arrange
        var converter = new ReverseConverter();

        // Act
        var result = converter.Convert(true, typeof(bool), null, Culture);

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void ConvertFalseReturnsTrue()
    {
        // Arrange
        var converter = new ReverseConverter();

        // Act
        var result = converter.Convert(false, typeof(bool), null, Culture);

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public void ConvertNonBoolPassesThrough()
    {
        // Arrange
        var converter = new ReverseConverter();

        // Act
        var result = converter.Convert("text", typeof(object), null, Culture);

        // Assert
        Assert.Equal("text", result);
    }

    [Fact]
    public void ConvertBackTrueReturnsFalse()
    {
        // Arrange
        var converter = new ReverseConverter();

        // Act
        var result = converter.ConvertBack(true, typeof(bool), null, Culture);

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void ConvertBackFalseReturnsTrue()
    {
        // Arrange
        var converter = new ReverseConverter();

        // Act
        var result = converter.ConvertBack(false, typeof(bool), null, Culture);

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public void ConvertNullPassesThrough()
    {
        // Arrange
        var converter = new ReverseConverter();

        // Act
        var result = converter.Convert(null, typeof(bool), null, Culture);

        // Assert
        Assert.Null(result);
    }
}
