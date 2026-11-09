namespace Smart.Avalonia;

using global::Avalonia.Controls;

public sealed class VisualExtensionsTests
{
    //------------------------------------------------------------
    // Descendant
    //------------------------------------------------------------

    [Fact]
    public void FindDescendantsWithStateReturnsMatchingInDepthFirstOrder()
    {
        // Arrange
        var (root, inner, _, shallow) = Build();

        // Act
        var descendants = root.FindDescendants<Control, int>(5, static (x, s) => x.Name!.Length >= s).ToList();

        // Assert
        Assert.Equal([inner, shallow], descendants);
    }

    [Fact]
    public void FindDescendantWithStateReturnsFirstMatch()
    {
        // Arrange
        var (root, _, deep, _) = Build();

        // Act & Assert
        Assert.Same(deep, root.FindDescendant<TextBlock, int>(4, static (x, s) => x.Name!.Length == s));
        Assert.Null(root.FindDescendant<TextBlock, string>("none", static (x, s) => x.Name == s));
    }

    [Fact]
    public void FindDescendantOrSelfWithStateChecksSelf()
    {
        // Arrange
        var (root, inner, _, _) = Build();

        // Act & Assert
        Assert.Same(root, root.FindDescendantOrSelf<StackPanel, string>("root", static (x, s) => x.Name == s));
        Assert.Same(inner, root.FindDescendantOrSelf<StackPanel, string>("inner", static (x, s) => x.Name == s));
    }

    //------------------------------------------------------------
    // Ascendant
    //------------------------------------------------------------

    [Fact]
    public void FindAscendantWithStateSkipsNotMatching()
    {
        // Arrange
        var (root, _, deep, _) = Build();

        // Act & Assert
        Assert.Same(root, deep.FindAscendant<StackPanel, string>("root", static (x, s) => x.Name == s));
        Assert.Null(deep.FindAscendant<StackPanel, string>("none", static (x, s) => x.Name == s));
    }

    [Fact]
    public void FindAscendantOrSelfWithStateChecksSelf()
    {
        // Arrange
        var (root, inner, _, _) = Build();

        // Act & Assert
        Assert.Same(inner, inner.FindAscendantOrSelf<StackPanel, string>("inner", static (x, s) => x.Name == s));
        Assert.Same(root, inner.FindAscendantOrSelf<StackPanel, string>("root", static (x, s) => x.Name == s));
    }

    private static (StackPanel Root, StackPanel Inner, TextBlock Deep, TextBlock Shallow) Build()
    {
        var deep = new TextBlock { Name = "deep" };
        var shallow = new TextBlock { Name = "shallow" };
        var inner = new StackPanel { Name = "inner", Children = { deep } };
        var root = new StackPanel { Name = "root", Children = { inner, shallow } };
        return (root, inner, deep, shallow);
    }
}
