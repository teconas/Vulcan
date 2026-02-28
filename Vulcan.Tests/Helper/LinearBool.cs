namespace Vulcan.Tests.Helper;

public record LinearBool
{
    public bool Value { get; private set; }

    public void Set() => Value = true;

    public static implicit operator bool(LinearBool value) => value.Value;

    public void ShouldBeTrue() => Value.ShouldBeTrue();
    public void ShouldBeFalse() => Value.ShouldBeFalse();
    public void ShouldBe(bool expected) => Value.ShouldBe(expected);
}