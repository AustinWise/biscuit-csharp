using System.Diagnostics;

namespace us.awise.biscuits.generated;

[Conditional("DEBUG")]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
internal sealed class NativeTypeNameAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}
