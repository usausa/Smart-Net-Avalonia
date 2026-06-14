namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;
using global::Avalonia.Media;

public sealed class ColorBlendConverterTest
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void ConvertBlendColorAtZeroRaito()
    {
        // Arrange
        // Raito=0: result should be same as input color
        var converter = new ColorBlendConverter
        {
            Color = Color.FromRgb(255, 255, 255),
            Raito = 0d
        };
        var input = Color.FromRgb(100, 150, 200);

        // Act
        var result = converter.Convert(input, typeof(Color), null, Culture);

        // Assert
        var blended = Assert.IsType<Color>(result);
        Assert.Equal(input, blended);
    }

    [Fact]
    public void ConvertBlendColorAtOneRaito()
    {
        // Arrange
        // Raito=1: result should be same as Color property
        var target = Color.FromRgb(255, 0, 128);
        var converter = new ColorBlendConverter
        {
            Color = target,
            Raito = 1d
        };
        var input = Color.FromRgb(10, 20, 30);

        // Act
        var result = converter.Convert(input, typeof(Color), null, Culture);

        // Assert
        var blended = Assert.IsType<Color>(result);
        Assert.Equal(target.R, blended.R);
        Assert.Equal(target.G, blended.G);
        Assert.Equal(target.B, blended.B);
    }

    [Fact]
    public void ConvertNonColorReturnsUnsetValue()
    {
        // Arrange
        var converter = new ColorBlendConverter
        {
            Color = Color.FromRgb(255, 255, 255),
            Raito = 0.5d
        };

        // Act
        var result = converter.Convert("not a color", typeof(Color), null, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }

    [Fact]
    public void ConvertBackReturnsUnsetValue()
    {
        // Arrange
        var converter = new ColorBlendConverter
        {
            Color = Color.FromRgb(255, 255, 255),
            Raito = 0.5d
        };

        // Act
        var result = converter.ConvertBack(Color.FromRgb(128, 128, 128), typeof(Color), null, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }

    [Fact]
    public void RaitoOutOfRangeThrows()
    {
        // Arrange
        var converter = new ColorBlendConverter();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => converter.Raito = 1.5d);
        Assert.Throws<ArgumentOutOfRangeException>(() => converter.Raito = -0.1d);
    }
}
