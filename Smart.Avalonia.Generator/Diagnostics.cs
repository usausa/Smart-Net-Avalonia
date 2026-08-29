namespace Smart.Avalonia.Generator;

using Microsoft.CodeAnalysis;

internal static class Diagnostics
{
    public static DiagnosticDescriptor InvalidPropertyDefinition { get; } = new(
        id: "SAV0001",
        title: "Invalid property definition",
        messageFormat: "[StyledProperty] property must be partial. property=[{0}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor StaticPropertyNotSupported { get; } = new(
        id: "SAV0002",
        title: "Static property not supported",
        messageFormat: "[StyledProperty] static property is not supported. property=[{0}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor InvalidPropertyAccessor { get; } = new(
        id: "SAV0003",
        title: "Invalid property accessor",
        messageFormat: "[StyledProperty] property must have get/set without modifiers. property=[{0}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor ContainingTypeNotPartial { get; } = new(
        id: "SAV0004",
        title: "Containing type not partial",
        messageFormat: "[StyledProperty] containing type must be partial. property=[{0}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor InvalidContainingType { get; } = new(
        id: "SAV0005",
        title: "Invalid containing type",
        messageFormat: "[StyledProperty] containing type is not AvaloniaObject. property=[{0}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor GenericTypeNotSupported { get; } = new(
        id: "SAV0006",
        title: "Generic type not supported",
        messageFormat: "[StyledProperty] generic containing type is not supported. property=[{0}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor DefaultValueConflict { get; } = new(
        id: "SAV0007",
        title: "DefaultValue conflict",
        messageFormat: "[StyledProperty] DefaultValue and DefaultValueExpression conflict. property=[{0}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor CallbackMethodNotFound { get; } = new(
        id: "SAV0008",
        title: "Callback method not found",
        messageFormat: "[StyledProperty] callback method is not found. method=[{0}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor InvalidCallbackMethod { get; } = new(
        id: "SAV0009",
        title: "Invalid callback method",
        messageFormat: "[StyledProperty] callback method signature is invalid. method=[{0}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor InvalidDefaultValue { get; } = new(
        id: "SAV0010",
        title: "Invalid default value",
        messageFormat: "[StyledProperty] DefaultValue is not a supported constant. property=[{0}]",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);
}
