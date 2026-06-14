namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;

public sealed class MapToObjectConverterTest
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    // NOTE: MapToObjectConverter<T>.Entries is a get-only empty array intended for XAML population.
    // Without a running XAML loader, entries cannot be added, so only the default-value path is tested.

    [Fact]
    public void ConvertNoMatchReturnsDefaultValue()
    {
        // Arrange
        var converter = new MapToTextConverter { DefaultValue = "default" };

        // Act
        var result = converter.Convert("missing", typeof(string), null, Culture);

        // Assert
        Assert.Equal("default", result);
    }

    [Fact]
    public void ConvertNullReturnsDefaultValue()
    {
        // Arrange
        var converter = new MapToTextConverter { DefaultValue = "default" };

        // Act
        var result = converter.Convert(null, typeof(string), null, Culture);

        // Assert
        Assert.Equal("default", result);
    }

    [Fact]
    public void ConvertBackReturnsUnsetValue()
    {
        // Arrange
        var converter = new MapToTextConverter { DefaultValue = "default" };

        // Act
        var result = converter.ConvertBack("something", typeof(string), null, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }
}
