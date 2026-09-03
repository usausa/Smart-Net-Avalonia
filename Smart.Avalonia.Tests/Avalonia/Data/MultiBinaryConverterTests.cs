namespace Smart.Avalonia.Data;

using System.Globalization;

using Smart.Avalonia.Expressions;

public sealed class MultiBinaryConverterTests
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void ConvertAddMultipleValues()
    {
        // Arrange
        var converter = new MultiBinaryConverter { Expression = BinaryExpressions.Add };

        // Act
        var result = converter.Convert([1, 2, 3], typeof(int), null, Culture);

        // Assert
        Assert.Equal(6, result);
    }

    [Fact]
    public void ConvertMaxMultipleValues()
    {
        // Arrange
        var converter = new MultiBinaryConverter { Expression = BinaryExpressions.Max };

        // Act
        var result = converter.Convert([3, 10, 5], typeof(int), null, Culture);

        // Assert
        Assert.Equal(10, result);
    }

    [Fact]
    public void ConvertSingleValueReturnsItself()
    {
        // Arrange
        var converter = new MultiBinaryConverter { Expression = BinaryExpressions.Add };

        // Act
        var result = converter.Convert([42], typeof(int), null, Culture);

        // Assert
        Assert.Equal(42, result);
    }
}
