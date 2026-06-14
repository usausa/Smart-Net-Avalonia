namespace Smart.Avalonia.Markup;

public sealed class PrimitiveExtensionTest
{
    // IServiceProvider stub - these extensions don't use IServiceProvider
    private static readonly IServiceProvider NullProvider = new NullServiceProvider();

    private sealed class NullServiceProvider : IServiceProvider
    {
        public object? GetService(Type serviceType) => null;
    }

    [Fact]
    public void BoolExtensionProvidesTrue()
    {
        // Arrange
        var ext = new BoolExtension(true);

        // Act & Assert
        Assert.Equal(true, ext.ProvideValue(NullProvider));
    }

    [Fact]
    public void BoolExtensionProvidesFalse()
    {
        // Arrange
        var ext = new BoolExtension(false);

        // Act & Assert
        Assert.Equal(false, ext.ProvideValue(NullProvider));
    }

    [Fact]
    public void DoubleExtensionProvidesValue()
    {
        // Arrange
        var ext = new DoubleExtension(3.14d);

        // Act & Assert
        Assert.Equal(3.14d, ext.ProvideValue(NullProvider));
    }

    [Fact]
    public void FloatExtensionProvidesValue()
    {
        // Arrange
        var ext = new FloatExtension(1.5f);

        // Act & Assert
        Assert.Equal(1.5f, ext.ProvideValue(NullProvider));
    }

    [Fact]
    public void Int16ExtensionProvidesValue()
    {
        // Arrange
        short value = 42;
        var ext = new Int16Extension(value);

        // Act & Assert
        Assert.Equal(value, ext.ProvideValue(NullProvider));
    }

    [Fact]
    public void Int32ExtensionProvidesValue()
    {
        // Arrange
        var ext = new Int32Extension(100);

        // Act & Assert
        Assert.Equal(100, ext.ProvideValue(NullProvider));
    }

    [Fact]
    public void Int64ExtensionProvidesValue()
    {
        // Arrange
        var ext = new Int64Extension(9_000_000_000L);

        // Act & Assert
        Assert.Equal(9_000_000_000L, ext.ProvideValue(NullProvider));
    }
}
