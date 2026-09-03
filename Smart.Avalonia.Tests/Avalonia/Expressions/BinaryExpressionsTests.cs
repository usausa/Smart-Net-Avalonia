namespace Smart.Avalonia.Expressions;

public sealed class BinaryExpressionsTests
{
    [Fact]
    public void MaxReturnsLarger()
    {
        // Act & Assert
        Assert.Equal(10, BinaryExpressions.Max.Eval(10, 5));
        Assert.Equal(10, BinaryExpressions.Max.Eval(5, 10));
        Assert.Equal(7, BinaryExpressions.Max.Eval(7, 7));
    }

    [Fact]
    public void MinReturnsSmaller()
    {
        // Act & Assert
        Assert.Equal(5, BinaryExpressions.Min.Eval(5, 10));
        Assert.Equal(5, BinaryExpressions.Min.Eval(10, 5));
    }

    [Fact]
    public void AddIntegers()
    {
        // Act & Assert
        Assert.Equal(7, BinaryExpressions.Add.Eval(3, 4));
    }

    [Fact]
    public void AddDoubles()
    {
        // Act & Assert
        Assert.Equal(3.5d, BinaryExpressions.Add.Eval(1.5d, 2.0d));
    }

    [Fact]
    public void SubIntegers()
    {
        // Act & Assert
        Assert.Equal(3, BinaryExpressions.Sub.Eval(10, 7));
    }

    [Fact]
    public void SubDoubles()
    {
        // Act & Assert
        Assert.Equal(1.5d, BinaryExpressions.Sub.Eval(3.5d, 2.0d));
    }

    [Fact]
    public void AddNullLeftReturnsNull()
    {
        // Act & Assert
        Assert.Null(BinaryExpressions.Add.Eval(null, 5));
    }

    [Fact]
    public void AddNullRightReturnsNull()
    {
        // Act & Assert
        Assert.Null(BinaryExpressions.Add.Eval(5, null));
    }

    [Fact]
    public void MaxNullRightReturnsLeft()
    {
        // Act & Assert
        // BinaryExpressions.Max: right is null => left is IComparable but right is null => returns left
        Assert.Equal(5, BinaryExpressions.Max.Eval(5, null));
    }
}
