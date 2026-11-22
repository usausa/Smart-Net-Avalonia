namespace Smart.Avalonia.Interactivity;

using System.Reflection;

using Smart.Mvvm.Messaging;

public sealed class ResolvePropertyActionTests
{
    private sealed class PropertyTarget
    {
        public string Name { get; } = "target";

        public string Broken => throw new InvalidOperationException(Name);
    }

    [Fact]
    public void ResolvesPropertyOfTargetObject()
    {
        // Arrange
        var action = new ResolvePropertyAction { TargetObject = new PropertyTarget(), PropertyName = nameof(PropertyTarget.Name) };
        var args = new ResolveEventArgs();

        // Act
        var result = action.Execute(null, args);

        // Assert
        Assert.True((bool)result);
        Assert.Equal("target", args.Result);
    }

    [Fact]
    public void ResolvesPropertyOfSenderWhenTargetObjectIsNotSet()
    {
        // Arrange
        var action = new ResolvePropertyAction { PropertyName = nameof(PropertyTarget.Name) };
        var args = new ResolveEventArgs();

        // Act
        action.Execute(new PropertyTarget(), args);

        // Assert
        Assert.Equal("target", args.Result);
    }

    [Fact]
    public void ReturnsFalseWhenParameterIsNotResolveEventArgs()
    {
        // Arrange
        var action = new ResolvePropertyAction { TargetObject = new PropertyTarget(), PropertyName = nameof(PropertyTarget.Name) };

        // Act
        var result = action.Execute(null, new object());

        // Assert
        Assert.False((bool)result);
    }

    [Fact]
    public void ReturnsFalseWhenPropertyNameIsNull()
    {
        // Arrange
        var action = new ResolvePropertyAction { TargetObject = new PropertyTarget(), PropertyName = null! };
        var args = new ResolveEventArgs();

        // Act
        var result = action.Execute(null, args);

        // Assert
        Assert.False((bool)result);
        Assert.Null(args.Result);
    }

    [Fact]
    public void ThrowsWrappedExceptionWhenGetterThrows()
    {
        // Arrange
        var action = new ResolvePropertyAction { TargetObject = new PropertyTarget(), PropertyName = nameof(PropertyTarget.Broken) };
        var args = new ResolveEventArgs();

        // Act
        var exception = Assert.Throws<TargetInvocationException>(() => action.Execute(null, args));

        // Assert
        var inner = Assert.IsType<InvalidOperationException>(exception.InnerException);
        Assert.Equal("target", inner.Message);
    }
}
