namespace Smart.Avalonia.Data;

using System.ComponentModel;
using System.Globalization;

using global::Avalonia;

public sealed class EnumDescriptionConverterTests
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    private enum SampleEnum
    {
        [Description("First Item")]
        First,
        Second
    }

    [Fact]
    public void ConvertEnumWithDescriptionReturnsDescription()
    {
        // Arrange
        var converter = new EnumDescriptionConverter();

        // Act
        var result = converter.Convert(SampleEnum.First, typeof(string), null, Culture);

        // Assert
        Assert.Equal("First Item", result);
    }

    [Fact]
    public void ConvertEnumWithoutDescriptionReturnsToString()
    {
        // Arrange
        var converter = new EnumDescriptionConverter();

        // Act
        var result = converter.Convert(SampleEnum.Second, typeof(string), null, Culture);

        // Assert
        Assert.Equal("Second", result);
    }

    [Fact]
    public void ConvertNullReturnsUnsetValue()
    {
        // Arrange
        var converter = new EnumDescriptionConverter();

        // Act
        var result = converter.Convert(null, typeof(string), null, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }

    [Fact]
    public void ConvertNonEnumReturnsToString()
    {
        // Arrange
        var converter = new EnumDescriptionConverter();

        // Act
        var result = converter.Convert(42, typeof(string), null, Culture);

        // Assert
        Assert.Equal("42", result);
    }

    [Fact]
    public void ConvertBackReturnsUnsetValue()
    {
        // Arrange
        var converter = new EnumDescriptionConverter();

        // Act
        var result = converter.ConvertBack("First Item", typeof(SampleEnum), null, Culture);

        // Assert
        Assert.Equal(AvaloniaProperty.UnsetValue, result);
    }
}
