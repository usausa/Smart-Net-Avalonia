namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;

using Smart.Avalonia.Expressions;

public sealed class BinaryConverterTests
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void ConvertAddExpression()
    {
        // Arrange
        var converter = new BinaryConverter { Expression = BinaryExpressions.Add };

        // Act
        var result = converter.Convert(3, typeof(int), 4, Culture);

        // Assert
        Assert.Equal(7, result);
    }

    [Fact]
    public void ConvertSubExpression()
    {
        // Arrange
        var converter = new BinaryConverter { Expression = BinaryExpressions.Sub };

        // Act
        var result = converter.Convert(10, typeof(int), 3, Culture);

        // Assert
        Assert.Equal(7, result);
    }

    [Fact]
    public void ConvertMaxExpression()
    {
        // Arrange
        var converter = new BinaryConverter { Expression = BinaryExpressions.Max };

        // Act
        var result = converter.Convert(5, typeof(int), 10, Culture);

        // Assert
        Assert.Equal(10, result);
    }

    [Fact]
    public void ConvertMinExpression()
    {
        // Arrange
        var converter = new BinaryConverter { Expression = BinaryExpressions.Min };

        // Act
        var result = converter.Convert(5, typeof(int), 10, Culture);

        // Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void ConvertBackReturnsUnsetValue()
    {
        // Arrange
        var converter = new BinaryConverter { Expression = BinaryExpressions.Add };

        // Act
        var result = converter.ConvertBack(7, typeof(int), 4, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }
}
