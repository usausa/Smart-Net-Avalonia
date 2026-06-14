namespace Smart.Avalonia.Data;

using System.Globalization;

public sealed class ObjectConvertConverterTest
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void ConvertIntToString()
    {
        // Arrange
        var converter = new ObjectConvertConverter();

        // Act
        var result = converter.Convert(42, typeof(string), null, Culture);

        // Assert
        Assert.Equal("42", result);
    }

    [Fact]
    public void ConvertStringToInt()
    {
        // Arrange
        var converter = new ObjectConvertConverter();

        // Act
        var result = converter.Convert("123", typeof(int), null, Culture);

        // Assert
        Assert.Equal(123, result);
    }

    [Fact]
    public void ConvertBackStringToInt()
    {
        // Arrange
        var converter = new ObjectConvertConverter();

        // Act
        var result = converter.ConvertBack("99", typeof(int), null, Culture);

        // Assert
        Assert.Equal(99, result);
    }
}
