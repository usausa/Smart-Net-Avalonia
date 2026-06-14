namespace Smart.Avalonia.Data;

using System.Globalization;
using System.Text.RegularExpressions;

using global::Avalonia;

public sealed class TextReplaceConverterTest
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void ConvertReplacesMatchedPattern()
    {
        // Arrange
        var converter = new TextReplaceConverter
        {
            Pattern = @"\d+",
            Replacement = "#"
        };

        // Act
        var result = converter.Convert("abc123def456", typeof(string), null, Culture);

        // Assert
        Assert.Equal("abc#def#", result);
    }

    [Fact]
    public void ConvertReplaceFirst()
    {
        // Arrange
        var converter = new TextReplaceConverter
        {
            Pattern = @"\d+",
            Replacement = "#",
            ReplaceAll = false
        };

        // Act
        var result = converter.Convert("abc123def456", typeof(string), null, Culture);

        // Assert
        Assert.Equal("abc#def456", result);
    }

    [Fact]
    public void ConvertNullReturnsNull()
    {
        // Arrange
        var converter = new TextReplaceConverter { Pattern = @"\d+", Replacement = "#" };

        // Act
        var result = converter.Convert(null, typeof(string), null, Culture);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ConvertEmptyStringReturnsEmpty()
    {
        // Arrange
        var converter = new TextReplaceConverter { Pattern = @"\d+", Replacement = "#" };

        // Act
        var result = converter.Convert(string.Empty, typeof(string), null, Culture);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ConvertIgnoreCaseOption()
    {
        // Arrange
        var converter = new TextReplaceConverter
        {
            Pattern = "hello",
            Replacement = "hi",
            Options = RegexOptions.IgnoreCase
        };

        // Act
        var result = converter.Convert("Hello World", typeof(string), null, Culture);

        // Assert
        Assert.Equal("hi World", result);
    }

    [Fact]
    public void ConvertBackReturnsUnsetValue()
    {
        // Arrange
        var converter = new TextReplaceConverter { Pattern = @"\d+", Replacement = "#" };

        // Act
        var result = converter.ConvertBack("result", typeof(string), null, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }
}
