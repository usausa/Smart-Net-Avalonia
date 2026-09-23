namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;

public sealed class AllAnyConverterTests
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    // ---------------------------------------------------------------------------
    // AllConverter
    // ---------------------------------------------------------------------------

    [Fact]
    public void AllConverterAllTrueReturnsTrue()
    {
        // Arrange
        var converter = new AllConverter();

        // Act
        var result = converter.Convert([true, true, true], typeof(bool), null, Culture);

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public void AllConverterAnyFalseReturnsFalse()
    {
        // Arrange
        var converter = new AllConverter();

        // Act
        var result = converter.Convert([true, false, true], typeof(bool), null, Culture);

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void AllConverterInvertAllTrueReturnsFalse()
    {
        // Arrange
        var converter = new AllConverter { Invert = true };

        // Act
        var result = converter.Convert([true, true], typeof(bool), null, Culture);

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void AllConverterEmptyReturnsTrue()
    {
        // Arrange
        var converter = new AllConverter();

        // Act
        var result = converter.Convert([], typeof(bool), null, Culture);

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public void AllConverterUnsetValueReturnsUnsetValue()
    {
        // Arrange
        var converter = new AllConverter();

        // Act
        var result = converter.Convert([true, AvaloniaProperty.UnsetValue], typeof(bool), null, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }

    // ---------------------------------------------------------------------------
    // AnyConverter
    // ---------------------------------------------------------------------------

    [Fact]
    public void AnyConverterAnyTrueReturnsTrue()
    {
        // Arrange
        var converter = new AnyConverter();

        // Act
        var result = converter.Convert([false, true, false], typeof(bool), null, Culture);

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public void AnyConverterAllFalseReturnsFalse()
    {
        // Arrange
        var converter = new AnyConverter();

        // Act
        var result = converter.Convert([false, false, false], typeof(bool), null, Culture);

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void AnyConverterInvertAnyTrueReturnsFalse()
    {
        // Arrange
        var converter = new AnyConverter { Invert = true };

        // Act
        var result = converter.Convert([false, true], typeof(bool), null, Culture);

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void AnyConverterEmptyReturnsFalse()
    {
        // Arrange
        var converter = new AnyConverter();

        // Act
        var result = converter.Convert([], typeof(bool), null, Culture);

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void AnyConverterUnsetValueReturnsUnsetValue()
    {
        // Arrange
        var converter = new AnyConverter();

        // Act
        var result = converter.Convert([false, AvaloniaProperty.UnsetValue], typeof(bool), null, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }
}
