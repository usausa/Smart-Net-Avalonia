namespace Smart.Avalonia.Data;

using System.Globalization;

public sealed class ArrayIndexConverterTest
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void ConvertReturnsElementAtIndex()
    {
        // Arrange
        var converter = new ArrayIndexConverter();
        string[] array = ["a", "b", "c"];

        // Act
        var result = converter.Convert(1, typeof(string), array, Culture);

        // Assert
        Assert.Equal("b", result);
    }

    [Fact]
    public void ConvertNonIntValueReturnsNull()
    {
        // Arrange
        var converter = new ArrayIndexConverter();
        string[] array = ["a", "b", "c"];

        // Act
        var result = converter.Convert("x", typeof(string), array, Culture);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ConvertNullParameterReturnsNull()
    {
        // Arrange
        var converter = new ArrayIndexConverter();

        // Act
        var result = converter.Convert(0, typeof(string), null, Culture);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ConvertBackReturnsIndexOfElement()
    {
        // Arrange
        var converter = new ArrayIndexConverter();
        string[] array = ["a", "b", "c"];

        // Act
        var result = converter.ConvertBack("b", typeof(int), array, Culture);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void ConvertBackNotFoundReturnsMinus1()
    {
        // Arrange
        var converter = new ArrayIndexConverter();
        string[] array = ["a", "b", "c"];

        // Act
        var result = converter.ConvertBack("z", typeof(int), array, Culture);

        // Assert
        Assert.Equal(-1, result);
    }

    [Fact]
    public void ConvertBackNullParameterReturnsMinus1()
    {
        // Arrange
        var converter = new ArrayIndexConverter();

        // Act
        var result = converter.ConvertBack("a", typeof(int), null, Culture);

        // Assert
        Assert.Equal(-1, result);
    }
}
