# Diagnostics

| ID | Severity | Description | How to fix |
|---|---|---|---|
| SAV0001 | ❌ Error | `[StyledProperty]` property is not declared as a partial property | Declare the property as `public partial T Name { get; set; }` |
| SAV0002 | ❌ Error | `[StyledProperty]` property is static, and a static property can not be backed by an instance value | Remove `static` from the property, or register the `StyledProperty` by hand |
| SAV0003 | ❌ Error | `[StyledProperty]` property does not have both accessors, or an accessor has its own accessibility modifier such as `private set` | Declare the property as `{ get; set; }` without accessor modifiers |
| SAV0004 | ❌ Error | The type containing the `[StyledProperty]` property, or one of its outer types, is not partial | Add `partial` to the containing type and to every outer type |
| SAV0005 | ❌ Error | The type containing the `[StyledProperty]` property is not derived from `AvaloniaObject`, so `GetValue` and `SetValue` are not available | Derive the containing type from `AvaloniaObject` |
| SAV0006 | ❌ Error | The type containing the `[StyledProperty]` property is generic, and a static `StyledProperty` field would be created per type argument | Move the property to a non generic type |
| SAV0007 | ❌ Error | `[StyledProperty]` specifies both `DefaultValue` and `DefaultValueExpression`, and only one default value can be used | Remove either `DefaultValue` or `DefaultValueExpression` |
| SAV0008 | ❌ Error | The method specified for `Coerce` or `Validate` of `[StyledProperty]` does not exist in the containing type | Specify the method with `nameof`, and define it in the same type |
| SAV0009 | ❌ Error | The signature of the callback method specified by `[StyledProperty]` does not match, or more than one overload is applicable | Match the signature: `Coerce` is `T (T value)`, `Validate` is `static bool (T value)` |
| SAV0010 | ❌ Error | The value specified for `DefaultValue` of `[StyledProperty]` can not be written as a constant in the generated code | Use `DefaultValueExpression` to give the default value as an expression |
