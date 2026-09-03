namespace Smart.Avalonia.Markup;

using System.Globalization;

using Smart.Avalonia.Data;
using Smart.Avalonia.Expressions;

public sealed class ConverterExtensionTests
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
    private static readonly IServiceProvider NullProvider = new NullServiceProvider();

    private sealed class NullServiceProvider : IServiceProvider
    {
        public object? GetService(Type serviceType) => null;
    }

    [Fact]
    public void BoolToTextExtensionProvidesConverter()
    {
        // Arrange
        var ext = new BoolToTextExtension { True = "yes", False = "no" };

        // Act
        var converter = Assert.IsType<BoolToTextConverter>(ext.ProvideValue(NullProvider));

        // Assert
        Assert.Equal("yes", converter.TrueValue);
        Assert.Equal("no", converter.FalseValue);
    }

    [Fact]
    public void TextToBoolExtensionProvidesConverter()
    {
        // Arrange
        var ext = new TextToBoolExtension { True = "active", False = "inactive" };

        // Act
        var converter = Assert.IsType<TextToBoolConverter>(ext.ProvideValue(NullProvider));

        // Assert
        Assert.Equal("active", converter.TrueValue);
        Assert.Equal("inactive", converter.FalseValue);
    }

    [Fact]
    public void IntToBoolExtensionProvidesConverter()
    {
        // Arrange
        var ext = new IntToBoolExtension { True = 1, False = 0 };

        // Act
        var converter = Assert.IsType<IntToBoolConverter>(ext.ProvideValue(NullProvider));

        // Assert
        Assert.Equal(1, converter.TrueValue);
        Assert.Equal(0, converter.FalseValue);
    }

    [Fact]
    public void NullToBoolExtensionProvidesConverter()
    {
        // Arrange
        var ext = new NullToBoolExtension();

        // Act
        var converter = Assert.IsType<NullToBoolConverter>(ext.ProvideValue(NullProvider));

        // Assert
        // Invert=false => NullValue=!false=true, NonNullValue=false
        Assert.True(converter.NullValue);
        Assert.False(converter.NonNullValue);
    }

    [Fact]
    public void NullToBoolExtensionInverted()
    {
        // Arrange
        var ext = new NullToBoolExtension { Invert = true };

        // Act
        var converter = Assert.IsType<NullToBoolConverter>(ext.ProvideValue(NullProvider));

        // Assert
        // Invert=true => NullValue=!true=false, NonNullValue=true
        Assert.False(converter.NullValue);
        Assert.True(converter.NonNullValue);
    }

    [Fact]
    public void NullToTextExtensionProvidesConverter()
    {
        // Arrange
        var ext = new NullToTextExtension { Null = "empty", NonNull = "filled" };

        // Act
        var converter = Assert.IsType<NullToTextConverter>(ext.ProvideValue(NullProvider));

        // Assert
        Assert.Equal("empty", converter.NullValue);
        Assert.Equal("filled", converter.NonNullValue);
    }

    [Fact]
    public void ContainsToBoolExtensionProvidesConverter()
    {
        // Arrange
        var ext = new ContainsToBoolExtension();

        // Act
        var converter = Assert.IsType<ContainsToBoolConverter>(ext.ProvideValue(NullProvider));

        // Assert
        Assert.True(converter.TrueValue);
        Assert.False(converter.FalseValue);
    }

    [Fact]
    public void ContainsToBoolExtensionInverted()
    {
        // Arrange
        var ext = new ContainsToBoolExtension { Invert = true };

        // Act
        var converter = Assert.IsType<ContainsToBoolConverter>(ext.ProvideValue(NullProvider));

        // Assert
        Assert.False(converter.TrueValue);
        Assert.True(converter.FalseValue);
    }

    [Fact]
    public void ContainsToTextExtensionProvidesConverter()
    {
        // Arrange
        var ext = new ContainsToTextExtension { True = "yes", False = "no" };

        // Act
        var converter = Assert.IsType<ContainsToTextConverter>(ext.ProvideValue(NullProvider));

        // Assert
        Assert.Equal("yes", converter.TrueValue);
        Assert.Equal("no", converter.FalseValue);
    }

    [Fact]
    public void CompareToBoolExtensionProvidesConverterWithDefaultEqual()
    {
        // Arrange
        var ext = new CompareToBoolExtension();

        // Act
        var converter = Assert.IsType<CompareToBoolConverter>(ext.ProvideValue(NullProvider));

        // Assert
        Assert.Equal(CompareExpressions.Equal, converter.Expression);
        Assert.True(converter.TrueValue);
        Assert.False(converter.FalseValue);
    }

    [Fact]
    public void CompareToBoolExtensionProvidesConverterWithCustomExpression()
    {
        // Arrange
        var ext = new CompareToBoolExtension { Expression = CompareExpressions.GreaterThan };

        // Act
        var converter = Assert.IsType<CompareToBoolConverter>(ext.ProvideValue(NullProvider));

        // Assert
        Assert.Equal(CompareExpressions.GreaterThan, converter.Expression);
    }

    [Fact]
    public void CompareToTextExtensionProvidesConverter()
    {
        // Arrange
        var ext = new CompareToTextExtension { True = "match", False = "no" };

        // Act
        var converter = Assert.IsType<CompareToTextConverter>(ext.ProvideValue(NullProvider));

        // Assert
        Assert.Equal("match", converter.TrueValue);
        Assert.Equal("no", converter.FalseValue);
    }

    [Fact]
    public void TextReplaceExtensionProvidesConverter()
    {
        // Arrange
        var ext = new TextReplaceExtension { Pattern = @"\d+", Replacement = "#" };

        // Act
        var converter = Assert.IsType<TextReplaceConverter>(ext.ProvideValue(NullProvider));
        var result = converter.Convert("abc123", typeof(string), null, Culture);

        // Assert
        Assert.Equal("abc#", result);
    }

    [Fact]
    public void EnumValuesExtensionProvidesEnumArray()
    {
        // Arrange
        var ext = new EnumValuesExtension(typeof(DayOfWeek));

        // Act
        var result = ext.ProvideValue(NullProvider);

        // Assert
        var values = Assert.IsAssignableFrom<Array>(result);
        Assert.Equal(7, values.Length);
    }
}
