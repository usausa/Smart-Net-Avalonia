namespace Smart.Avalonia.Expressions;

public sealed class CompareExpressionsTest
{
    [Fact]
    public void EqualReturnsTrueForSameValue()
    {
        // Act & Assert
        Assert.True(CompareExpressions.Equal.Eval(5, 5));
    }

    [Fact]
    public void EqualReturnsFalseForDifferentValues()
    {
        // Act & Assert
        Assert.False(CompareExpressions.Equal.Eval(5, 6));
    }

    [Fact]
    public void EqualBothNullReturnsTrue()
    {
        // Act & Assert
        Assert.True(CompareExpressions.Equal.Eval(null, null));
    }

    [Fact]
    public void NotEqualReturnsTrueForDifferentValues()
    {
        // Act & Assert
        Assert.True(CompareExpressions.NotEqual.Eval(5, 6));
    }

    [Fact]
    public void NotEqualReturnsFalseForSameValue()
    {
        // Act & Assert
        Assert.False(CompareExpressions.NotEqual.Eval(5, 5));
    }

    [Fact]
    public void LessThanReturnsTrueWhenLeftLess()
    {
        // Act & Assert
        Assert.True(CompareExpressions.LessThan.Eval(3, 5));
    }

    [Fact]
    public void LessThanReturnsFalseWhenEqual()
    {
        // Act & Assert
        Assert.False(CompareExpressions.LessThan.Eval(5, 5));
    }

    [Fact]
    public void LessThanOrEqualReturnsTrueWhenEqual()
    {
        // Act & Assert
        Assert.True(CompareExpressions.LessThanOrEqual.Eval(5, 5));
    }

    [Fact]
    public void LessThanOrEqualReturnsTrueWhenLess()
    {
        // Act & Assert
        Assert.True(CompareExpressions.LessThanOrEqual.Eval(3, 5));
    }

    [Fact]
    public void GreaterThanReturnsTrueWhenLeftGreater()
    {
        // Act & Assert
        Assert.True(CompareExpressions.GreaterThan.Eval(7, 5));
    }

    [Fact]
    public void GreaterThanReturnsFalseWhenEqual()
    {
        // Act & Assert
        Assert.False(CompareExpressions.GreaterThan.Eval(5, 5));
    }

    [Fact]
    public void GreaterThanOrEqualReturnsTrueWhenEqual()
    {
        // Act & Assert
        Assert.True(CompareExpressions.GreaterThanOrEqual.Eval(5, 5));
    }

    [Fact]
    public void EqualWithStringConversion()
    {
        // Act & Assert
        // int 5, string "5" => ConvertHelper converts "5" to int 5 => equal
        Assert.True(CompareExpressions.Equal.Eval(5, "5"));
    }

    [Fact]
    public void LessThanNullRightReturnsFalse()
    {
        // Act & Assert
        Assert.False(CompareExpressions.LessThan.Eval(5, null));
    }
}
