namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia.Data;

public sealed class BoolToObjectConverterTests
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void ConvertTrueReturnsTrueValue()
    {
        // Arrange
        var converter = new BoolToTextConverter { TrueValue = "yes", FalseValue = "no" };

        // Act
        var result = converter.Convert(true, typeof(string), null, Culture);

        // Assert
        Assert.Equal("yes", result);
    }

    [Fact]
    public void ConvertFalseReturnsFalseValue()
    {
        // Arrange
        var converter = new BoolToTextConverter { TrueValue = "yes", FalseValue = "no" };

        // Act
        var result = converter.Convert(false, typeof(string), null, Culture);

        // Assert
        Assert.Equal("no", result);
    }

    [Fact]
    public void ConvertNonBoolReturnsFalseValue()
    {
        // Arrange
        var converter = new BoolToTextConverter { TrueValue = "yes", FalseValue = "no" };

        // Act
        var result = converter.Convert(null, typeof(string), null, Culture);

        // Assert
        Assert.Equal("no", result);
    }

    [Fact]
    public void ConvertBackMatchesTrueValueReturnsTrue()
    {
        // Arrange
        var converter = new BoolToTextConverter { TrueValue = "yes", FalseValue = "no" };

        // Act
        var result = converter.ConvertBack("yes", typeof(bool), null, Culture);

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public void ConvertBackMatchesFalseValueReturnsFalse()
    {
        // Arrange
        var converter = new BoolToTextConverter { TrueValue = "yes", FalseValue = "no" };

        // Act
        var result = converter.ConvertBack("no", typeof(bool), null, Culture);

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void ConvertBackNoMatchReturnsDoNothing()
    {
        // Arrange
        var converter = new BoolToTextConverter { TrueValue = "yes", FalseValue = "no" };

        // Act
        var result = converter.ConvertBack("other", typeof(bool), null, Culture);

        // Assert
        Assert.Equal(BindingOperations.DoNothing, result);
    }

    [Fact]
    public void ConvertBackNullReturnsDoNothing()
    {
        // Arrange
        var converter = new BoolToTextConverter { TrueValue = "yes", FalseValue = "no" };

        // Act
        var result = converter.ConvertBack(null, typeof(bool), null, Culture);

        // Assert
        Assert.Equal(BindingOperations.DoNothing, result);
    }
}
