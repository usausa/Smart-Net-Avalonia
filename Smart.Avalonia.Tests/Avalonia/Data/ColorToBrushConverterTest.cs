namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia.Media;

public sealed class ColorToBrushConverterTest
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void ConvertColorReturnsSolidColorBrush()
    {
        // Arrange
        var converter = new ColorToBrushConverter();
        var color = Color.FromRgb(255, 0, 0);

        // Act
        var result = converter.Convert(color, typeof(IBrush), null, Culture);

        // Assert
        var brush = Assert.IsType<SolidColorBrush>(result);
        Assert.Equal(color, brush.Color);
    }

    [Fact]
    public void ConvertNonColorReturnsNull()
    {
        // Arrange
        var converter = new ColorToBrushConverter();

        // Act
        var result = converter.Convert("not a color", typeof(IBrush), null, Culture);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ConvertNullReturnsNull()
    {
        // Arrange
        var converter = new ColorToBrushConverter();

        // Act
        var result = converter.Convert(null, typeof(IBrush), null, Culture);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ConvertBackSolidColorBrushReturnsColor()
    {
        // Arrange
        var converter = new ColorToBrushConverter();
        var color = Color.FromRgb(0, 128, 255);
        var brush = new SolidColorBrush(color);

        // Act
        var result = converter.ConvertBack(brush, typeof(Color), null, Culture);

        // Assert
        Assert.Equal(color, result);
    }

    [Fact]
    public void ConvertBackNullReturnsNull()
    {
        // Arrange
        var converter = new ColorToBrushConverter();

        // Act
        var result = converter.ConvertBack(null, typeof(Color), null, Culture);

        // Assert
        Assert.Null(result);
    }
}
