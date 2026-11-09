namespace Smart.Avalonia.Markup;

using global::Avalonia.Media;

public sealed class FontImageExtensionTests
{
    [Fact]
    public void HasDefaults()
    {
        // Arrange
        var extension = new FontImageExtension();

        // Act & Assert
        Assert.Equal(string.Empty, extension.Glyph);
        Assert.Equal(FontFamily.Default, extension.FontFamily);
        Assert.Equal(16d, extension.Size);
        Assert.Equal(Colors.Black, Assert.IsAssignableFrom<ISolidColorBrush>(extension.Brush).Color);
        Assert.Equal(FontWeight.Normal, extension.FontWeight);
        Assert.Equal(FontStyle.Normal, extension.FontStyle);
    }
}
