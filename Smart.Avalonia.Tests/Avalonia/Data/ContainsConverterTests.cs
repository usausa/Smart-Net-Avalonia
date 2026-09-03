namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;

public sealed class ContainsConverterTests
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void ConvertValueInListReturnsTrueValue()
    {
        // Arrange
        var converter = new ContainsToBoolConverter();
        var list = new List<string> { "a", "b", "c" };

        // Act
        var result = converter.Convert("b", typeof(bool), list, Culture);

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public void ConvertValueNotInListReturnsFalseValue()
    {
        // Arrange
        var converter = new ContainsToBoolConverter();
        var list = new List<string> { "a", "b", "c" };

        // Act
        var result = converter.Convert("z", typeof(bool), list, Culture);

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void ConvertNullParameterReturnsFalseValue()
    {
        // Arrange
        var converter = new ContainsToBoolConverter();

        // Act
        var result = converter.Convert("a", typeof(bool), null, Culture);

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void ConvertBackReturnsUnsetValue()
    {
        // Arrange
        var converter = new ContainsToBoolConverter();

        // Act
        var result = converter.ConvertBack(true, typeof(bool), null, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }

    [Fact]
    public void ContainsToTextConverterInList()
    {
        // Arrange
        var converter = new ContainsToTextConverter { TrueValue = "yes", FalseValue = "no" };
        var list = new List<int> { 1, 2, 3 };

        // Act
        var result = converter.Convert(2, typeof(string), list, Culture);

        // Assert
        Assert.Equal("yes", result);
    }

    [Fact]
    public void ContainsToTextConverterNotInList()
    {
        // Arrange
        var converter = new ContainsToTextConverter { TrueValue = "yes", FalseValue = "no" };
        var list = new List<int> { 1, 2, 3 };

        // Act
        var result = converter.Convert(9, typeof(string), list, Culture);

        // Assert
        Assert.Equal("no", result);
    }
}
