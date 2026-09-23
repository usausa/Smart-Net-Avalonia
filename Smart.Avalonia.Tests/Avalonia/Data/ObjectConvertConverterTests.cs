namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia.Data;

public sealed class ObjectConvertConverterTests
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

    [Fact]
    public void ConvertBackInvalidReturnsDoNothing()
    {
        // Arrange
        var converter = new ObjectConvertConverter();

        // Act
        var result = converter.ConvertBack("abc", typeof(int), null, Culture);

        // Assert
        Assert.Equal(BindingOperations.DoNothing, result);
    }

    [Fact]
    public void ConvertBackEmptyToValueTypeReturnsDoNothing()
    {
        // Arrange
        var converter = new ObjectConvertConverter();

        // Act
        var result = converter.ConvertBack(string.Empty, typeof(int), null, Culture);

        // Assert
        Assert.Equal(BindingOperations.DoNothing, result);
    }

    [Fact]
    public void ConvertBackEmptyToNullableReturnsNull()
    {
        // Arrange
        var converter = new ObjectConvertConverter();

        // Act
        var result = converter.ConvertBack(string.Empty, typeof(int?), null, Culture);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ConvertBackWithoutConverterReturnsDoNothing()
    {
        // Arrange
        var converter = new ObjectConvertConverter();

        // Act
        var result = converter.ConvertBack(new object(), typeof(int), null, Culture);

        // Assert
        Assert.Equal(BindingOperations.DoNothing, result);
    }
}
