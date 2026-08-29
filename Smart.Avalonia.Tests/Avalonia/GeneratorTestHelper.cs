namespace Smart.Avalonia;

using System.Collections.Generic;

using Microsoft.CodeAnalysis;

using Smart.Avalonia.Generator;

using SourceGenerateHelper.Testing;

internal static class GeneratorTestHelper
{
    private static GeneratorTestRunner Runner => GeneratorTestRunner
        .For<StyledPropertyGenerator>()
        .WithReference(typeof(StyledPropertyAttribute).Assembly)
        .WithReference(typeof(global::Avalonia.AvaloniaObject).Assembly)
        .WithReference(typeof(global::Avalonia.Data.BindingMode).Assembly)
        .WithDiagnosticPrefix("SAV")
        .VerifyCompiles();

    public static IReadOnlyList<Diagnostic> GetDiagnostics(string source) => Runner.GetDiagnostics(source);

    public static IReadOnlyList<Diagnostic> GetDiagnosticsAll(string source) => Runner.GetDiagnosticsAll(source);

    public static string GetGeneratedSource(string source) => Runner.GetGeneratedSource(source);

    public static IncrementalRunResult RunIncremental(string source, string addedSource) =>
        Runner.WithTracking().RunIncremental(source, addedSource);
}
