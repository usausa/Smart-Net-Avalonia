namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;

public sealed class MapToObjectConverterTests
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void ConvertMatchedEntryReturnsMappedValue()
    {
        // Arrange
        var converter = new MapToTextConverter { DefaultValue = "default" };
        converter.Entries.Add(new MapToTextEntry { Key = 1, Value = "one" });
        converter.Entries.Add(new MapToTextEntry { Key = 2, Value = "two" });

        // Act
        var result = converter.Convert(2, typeof(string), null, Culture);

        // Assert
        Assert.Equal("two", result);
    }

    [Fact]
    public void ConvertUnmatchedEntryReturnsDefaultValue()
    {
        // Arrange
        var converter = new MapToTextConverter { DefaultValue = "default" };
        converter.Entries.Add(new MapToTextEntry { Key = 1, Value = "one" });

        // Act
        var result = converter.Convert(9, typeof(string), null, Culture);

        // Assert
        Assert.Equal("default", result);
    }

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
    public void BoolConverterReturnsMappedValueOrDefault()
    {
        // Arrange
        var converter = new MapToBoolConverter();
        converter.Entries.Add(new MapToBoolEntry { Key = 1, Value = true });

        // Act & Assert
        Assert.Equal(true, converter.Convert(1, typeof(bool), null, Culture));
        Assert.Equal(false, converter.Convert(2, typeof(bool), null, Culture));
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
