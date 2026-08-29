namespace Smart.Avalonia;

public sealed class DiagnosticTest
{
    // ------------------------------------------------------------
    // Property definition
    // ------------------------------------------------------------

    [Fact]
    public void Sav0001NotPartialEmitsDiagnostic()
    {
        // Arrange
        const string source =
            """
            using Smart.Avalonia;
            using Avalonia;

            namespace Test;

            public partial class TestElement : AvaloniaObject
            {
                [StyledProperty]
                public string? Text { get; set; }
            }
            """;

        // Act
        var diagnostics = GeneratorTestHelper.GetDiagnostics(source);

        // Assert
        Assert.Contains(diagnostics, static x => x.Id == "SAV0001");
    }

    [Fact]
    public void Sav0002StaticPropertyEmitsDiagnostic()
    {
        // Arrange
        const string source =
            """
            using Smart.Avalonia;
            using Avalonia;

            namespace Test;

            public partial class TestElement : AvaloniaObject
            {
                [StyledProperty]
                public static partial string? Text { get; set; }
            }
            """;

        // Act
        var diagnostics = GeneratorTestHelper.GetDiagnostics(source);

        // Assert
        Assert.Contains(diagnostics, static x => x.Id == "SAV0002");
    }

    [Fact]
    public void Sav0003AccessorModifierEmitsDiagnostic()
    {
        // Arrange
        const string source =
            """
            using Smart.Avalonia;
            using Avalonia;

            namespace Test;

            public partial class TestElement : AvaloniaObject
            {
                [StyledProperty]
                public partial string? Text { get; private set; }
            }
            """;

        // Act
        var diagnostics = GeneratorTestHelper.GetDiagnostics(source);

        // Assert
        Assert.Contains(diagnostics, static x => x.Id == "SAV0003");
    }

    // ------------------------------------------------------------
    // Containing type
    // ------------------------------------------------------------

    [Fact]
    public void Sav0004ContainingTypeNotPartialEmitsDiagnostic()
    {
        // Arrange
        const string source =
            """
            using Smart.Avalonia;
            using Avalonia;

            namespace Test;

            public class TestElement : AvaloniaObject
            {
                [StyledProperty]
                public partial string? Text { get; set; }
            }
            """;

        // Act
        var diagnostics = GeneratorTestHelper.GetDiagnostics(source);

        // Assert
        Assert.Contains(diagnostics, static x => x.Id == "SAV0004");
    }

    [Fact]
    public void Sav0005NotAvaloniaObjectEmitsDiagnostic()
    {
        // Arrange
        const string source =
            """
            using Smart.Avalonia;

            namespace Test;

            public partial class TestElement
            {
                [StyledProperty]
                public partial string? Text { get; set; }
            }
            """;

        // Act
        var diagnostics = GeneratorTestHelper.GetDiagnostics(source);

        // Assert
        Assert.Contains(diagnostics, static x => x.Id == "SAV0005");
    }

    [Fact]
    public void Sav0006GenericTypeEmitsDiagnostic()
    {
        // Arrange
        const string source =
            """
            using Smart.Avalonia;
            using Avalonia;

            namespace Test;

            public partial class TestElement<T> : AvaloniaObject
            {
                [StyledProperty]
                public partial string? Text { get; set; }
            }
            """;

        // Act
        var diagnostics = GeneratorTestHelper.GetDiagnostics(source);

        // Assert
        Assert.Contains(diagnostics, static x => x.Id == "SAV0006");
    }

    // ------------------------------------------------------------
    // Attribute argument
    // ------------------------------------------------------------

    [Fact]
    public void Sav0007DefaultValueConflictEmitsDiagnostic()
    {
        // Arrange
        const string source =
            """
            using Smart.Avalonia;
            using Avalonia;

            namespace Test;

            public partial class TestElement : AvaloniaObject
            {
                [StyledProperty(DefaultValue = "abc", DefaultValueExpression = "\"abc\"")]
                public partial string? Text { get; set; }
            }
            """;

        // Act
        var diagnostics = GeneratorTestHelper.GetDiagnostics(source);

        // Assert
        Assert.Contains(diagnostics, static x => x.Id == "SAV0007");
    }

    [Fact]
    public void Sav0008InaccessibleBaseCallbackEmitsDiagnostic()
    {
        // Arrange
        const string source =
            """
            using Smart.Avalonia;
            using Avalonia;

            namespace Test;

            public class BaseElement : AvaloniaObject
            {
                private double CoerceScale(double value) => value;
            }

            public partial class TestElement : BaseElement
            {
                [StyledProperty(Coerce = "CoerceScale")]
                public partial double Scale { get; set; }
            }
            """;

        // Act
        var diagnostics = GeneratorTestHelper.GetDiagnostics(source);

        // Assert
        Assert.Contains(diagnostics, static x => x.Id == "SAV0008");
    }

    [Fact]
    public void Sav0008CallbackNotFoundEmitsDiagnostic()
    {
        // Arrange
        const string source =
            """
            using Smart.Avalonia;
            using Avalonia;

            namespace Test;

            public partial class TestElement : AvaloniaObject
            {
                [StyledProperty(Coerce = "CoerceText")]
                public partial string? Text { get; set; }
            }
            """;

        // Act
        var diagnostics = GeneratorTestHelper.GetDiagnostics(source);

        // Assert
        Assert.Contains(diagnostics, static x => x.Id == "SAV0008");
    }

    [Fact]
    public void Sav0009InvalidCoerceSignatureEmitsDiagnostic()
    {
        // Arrange
        const string source =
            """
            using Smart.Avalonia;
            using Avalonia;

            namespace Test;

            public partial class TestElement : AvaloniaObject
            {
                [StyledProperty(Coerce = nameof(CoerceText))]
                public partial string? Text { get; set; }

                private void CoerceText(int value)
                {
                }
            }
            """;

        // Act
        var diagnostics = GeneratorTestHelper.GetDiagnostics(source);

        // Assert
        Assert.Contains(diagnostics, static x => x.Id == "SAV0009");
    }

    [Fact]
    public void Sav0009NonStaticValidateEmitsDiagnostic()
    {
        // Arrange
        const string source =
            """
            using Smart.Avalonia;
            using Avalonia;

            namespace Test;

            public partial class TestElement : AvaloniaObject
            {
                [StyledProperty(Validate = nameof(ValidateText))]
                public partial string? Text { get; set; }

                private bool ValidateText(string? value) => true;
            }
            """;

        // Act
        var diagnostics = GeneratorTestHelper.GetDiagnostics(source);

        // Assert
        Assert.Contains(diagnostics, static x => x.Id == "SAV0009");
    }

    [Fact]
    public void Sav0010InvalidDefaultValueEmitsDiagnostic()
    {
        // Arrange
        const string source =
            """
            using Smart.Avalonia;
            using Avalonia;

            namespace Test;

            public partial class TestElement : AvaloniaObject
            {
                [StyledProperty(DefaultValue = new int[] { 1, 2 })]
                public partial int[]? Values { get; set; }
            }
            """;

        // Act
        var diagnostics = GeneratorTestHelper.GetDiagnostics(source);

        // Assert
        Assert.Contains(diagnostics, static x => x.Id == "SAV0010");
    }

    // ------------------------------------------------------------
    // Valid
    // ------------------------------------------------------------

    [Fact]
    public void ValidDefinitionEmitsNoDiagnostic()
    {
        // Arrange
        const string source =
            """
            using Smart.Avalonia;
            using Avalonia;

            namespace Test;

            public partial class TestElement : AvaloniaObject
            {
                [StyledProperty(DefaultValue = 0d, Inherits = true, Coerce = nameof(CoerceScale), Validate = nameof(ValidateScale))]
                public partial double Scale { get; set; }

                private double CoerceScale(double value) => value;

                private static bool ValidateScale(double value) => true;
            }
            """;

        // Act
        var diagnostics = GeneratorTestHelper.GetDiagnostics(source);

        // Assert
        Assert.Empty(diagnostics);
    }
}
