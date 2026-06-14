namespace Smart.Avalonia.Data;

using System.Globalization;

public sealed class ObjectToBoolConverterTest
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void ConvertMatchingValueReturnsTrue()
    {
        // Arrange
        var converter = new TextToBoolConverter { TrueValue = "active", FalseValue = "inactive" };

        // Act
        var result = converter.Convert("active", typeof(bool), null, Culture);

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public void ConvertNonMatchingValueReturnsFalse()
    {
        // Arrange
        var converter = new TextToBoolConverter { TrueValue = "active", FalseValue = "inactive" };

        // Act
        var result = converter.Convert("inactive", typeof(bool), null, Culture);

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void ConvertNullReturnsFalse()
    {
        // Arrange
        var converter = new TextToBoolConverter { TrueValue = "active", FalseValue = "inactive" };

        // Act
        var result = converter.Convert(null, typeof(bool), null, Culture);

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void ConvertBackTrueReturnsTrueValue()
    {
        // Arrange
        var converter = new TextToBoolConverter { TrueValue = "active", FalseValue = "inactive" };

        // Act
        var result = converter.ConvertBack(true, typeof(string), null, Culture);

        // Assert
        Assert.Equal("active", result);
    }

    [Fact]
    public void ConvertBackFalseReturnsFalseValue()
    {
        // Arrange
        var converter = new TextToBoolConverter { TrueValue = "active", FalseValue = "inactive" };

        // Act
        var result = converter.ConvertBack(false, typeof(string), null, Culture);

        // Assert
        Assert.Equal("inactive", result);
    }

    [Fact]
    public void IntToBoolConverterMatchReturnsTrue()
    {
        // Arrange
        var converter = new IntToBoolConverter { TrueValue = 1, FalseValue = 0 };

        // Act
        var result = converter.Convert(1, typeof(bool), null, Culture);

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public void IntToBoolConverterNoMatchReturnsFalse()
    {
        // Arrange
        var converter = new IntToBoolConverter { TrueValue = 1, FalseValue = 0 };

        // Act
        var result = converter.Convert(2, typeof(bool), null, Culture);

        // Assert
        Assert.Equal(false, result);
    }
}
