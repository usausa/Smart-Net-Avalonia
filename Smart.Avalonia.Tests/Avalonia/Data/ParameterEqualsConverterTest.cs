namespace Smart.Avalonia.Data;

using System.Globalization;

public sealed class ParameterEqualsConverterTest
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void ConvertEqualReturnsTrue()
    {
        // Arrange
        var converter = new ParameterEqualsConverter();

        // Act
        var result = converter.Convert("A", typeof(bool), "A", Culture);

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public void ConvertNotEqualReturnsFalse()
    {
        // Arrange
        var converter = new ParameterEqualsConverter();

        // Act
        var result = converter.Convert("A", typeof(bool), "B", Culture);

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void ConvertNullNullReturnsTrue()
    {
        // Arrange
        var converter = new ParameterEqualsConverter();

        // Act
        var result = converter.Convert(null, typeof(bool), null, Culture);

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public void ConvertBackTrueReturnsParameter()
    {
        // Arrange
        var converter = new ParameterEqualsConverter();

        // Act
        var result = converter.ConvertBack(true, typeof(string), "A", Culture);

        // Assert
        Assert.Equal("A", result);
    }

    [Fact]
    public void ConvertBackFalseReturnsNull()
    {
        // Arrange
        var converter = new ParameterEqualsConverter();

        // Act
        var result = converter.ConvertBack(false, typeof(string), "A", Culture);

        // Assert
        Assert.Null(result);
    }
}
