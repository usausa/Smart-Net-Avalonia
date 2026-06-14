namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;

using Smart.Avalonia.Expressions;

public sealed class CompareConverterTest
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void CompareToBoolEqualMatch()
    {
        // Arrange
        var converter = new CompareToBoolConverter { Expression = CompareExpressions.Equal };

        // Act
        var result = converter.Convert(5, typeof(bool), 5, Culture);

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public void CompareToBoolEqualNoMatch()
    {
        // Arrange
        var converter = new CompareToBoolConverter { Expression = CompareExpressions.Equal };

        // Act
        var result = converter.Convert(5, typeof(bool), 6, Culture);

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void CompareToBoolLessThan()
    {
        // Arrange
        var converter = new CompareToBoolConverter { Expression = CompareExpressions.LessThan };

        // Act & Assert
        Assert.Equal(true, converter.Convert(3, typeof(bool), 5, Culture));
        Assert.Equal(false, converter.Convert(5, typeof(bool), 5, Culture));
        Assert.Equal(false, converter.Convert(7, typeof(bool), 5, Culture));
    }

    [Fact]
    public void CompareToBoolGreaterThan()
    {
        // Arrange
        var converter = new CompareToBoolConverter { Expression = CompareExpressions.GreaterThan };

        // Act & Assert
        Assert.Equal(true, converter.Convert(7, typeof(bool), 5, Culture));
        Assert.Equal(false, converter.Convert(5, typeof(bool), 5, Culture));
    }

    [Fact]
    public void CompareToTextConverterReturnsCorrectValue()
    {
        // Arrange
        var converter = new CompareToTextConverter
        {
            Expression = CompareExpressions.Equal,
            TrueValue = "match",
            FalseValue = "nomatch"
        };

        // Act & Assert
        Assert.Equal("match", converter.Convert(1, typeof(string), 1, Culture));
        Assert.Equal("nomatch", converter.Convert(1, typeof(string), 2, Culture));
    }

    [Fact]
    public void CompareConverterConvertBackReturnsUnsetValue()
    {
        // Arrange
        var converter = new CompareToBoolConverter();

        // Act
        var result = converter.ConvertBack(true, typeof(int), 5, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }
}
