using Vulcan.Extensions;

namespace Vulcan.Tests.Extensions.Types;

[TestSubject(typeof(EqualsAnyTypedExtensions))]
public static class EqualsAnyTypedExtensionsTests
{
    // ── Typed overloads (struct: no boxing) ───────────────────────────────────

    public class SingleArg
    {
        [Fact]
        public void Match_ReturnsTrue() => 42.EqualsAny(42).ShouldBeTrue();

        [Fact]
        public void NoMatch_ReturnsFalse() => 42.EqualsAny(99).ShouldBeFalse();

        [Fact]
        public void NullValue_MatchesNullArg() => ((string?)null).EqualsAny((string?)null).ShouldBeTrue();

        [Fact]
        public void NullValue_NoMatchNonNull() => ((string?)null).EqualsAny("x").ShouldBeFalse();

        [Fact]
        public void NonNullValue_NoMatchNull() => "x".EqualsAny((string?)null).ShouldBeFalse();
    }

    public class TwoArgs
    {
        [Fact]
        public void MatchesFirst() => 1.EqualsAny(1, 2).ShouldBeTrue();

        [Fact]
        public void MatchesSecond() => 2.EqualsAny(1, 2).ShouldBeTrue();

        [Fact]
        public void NoMatch_ReturnsFalse() => 3.EqualsAny(1, 2).ShouldBeFalse();

        // null als Argument (nicht als this): string? → T=string, null ist gültiger string-Wert
        [Fact]
        public void NullArg_NoMatchNonNullValue() => "hello".EqualsAny(null, "123").ShouldBeFalse();

        [Fact]
        public void NullArg_MatchesWhenValueIsNull() => ((string?)null).EqualsAny(null, "123").ShouldBeTrue();
    }

    public class ThreeArgs
    {
        [Fact]
        public void MatchesAny() => "b".EqualsAny("a", "b", "c").ShouldBeTrue();

        [Fact]
        public void NoMatch_ReturnsFalse() => "z".EqualsAny("a", "b", "c").ShouldBeFalse();
    }

    public class FourArgs
    {
        [Fact]
        public void MatchesLast() => 4.EqualsAny(1, 2, 3, 4).ShouldBeTrue();

        [Fact]
        public void NoMatch_ReturnsFalse() => 5.EqualsAny(1, 2, 3, 4).ShouldBeFalse();
    }

    public class FiveArgs
    {
        [Fact]
        public void MatchesThird() => 3.EqualsAny(1, 2, 3, 4, 5).ShouldBeTrue();

        [Fact]
        public void NoMatch_ReturnsFalse() => 9.EqualsAny(1, 2, 3, 4, 5).ShouldBeFalse();
    }

    // ── Custom comparer ───────────────────────────────────────────────────────

    public class WithComparer
    {
        readonly IEqualityComparer<string> _oi = StringComparer.OrdinalIgnoreCase;

        [Fact]
        public void SingleArg_ComparerUsed() => "HELLO".EqualsAny("hello", _oi).ShouldBeTrue();

        [Fact]
        public void TwoArgs_ComparerUsed() => "WORLD".EqualsAny("hello", "world", _oi).ShouldBeTrue();

        [Fact]
        public void ThreeArgs_ComparerUsed() => "FOO".EqualsAny("bar", "baz", "foo", _oi).ShouldBeTrue();

        [Fact]
        public void FourArgs_ComparerUsed() => "C".EqualsAny("a", "b", "c", "d", _oi).ShouldBeTrue();

        [Fact]
        public void FiveArgs_ComparerUsed() => "E".EqualsAny("a", "b", "c", "d", "e", _oi).ShouldBeTrue();

        [Fact]
        public void NoMatch_ReturnsFalse() => "z".EqualsAny("a", "b", _oi).ShouldBeFalse();
    }

    // ── params fallback ───────────────────────────────────────────────────────

    public class ParamsFallback
    {
        [Fact]
        public void MatchesSixthArg() => 6.EqualsAny(1, 2, 3, 4, 5, 6).ShouldBeTrue();

        [Fact]
        public void NoMatch_ReturnsFalse() => 99.EqualsAny(1, 2, 3, 4, 5, 6).ShouldBeFalse();

        [Fact]
        public void EmptyParams_ReturnsFalse() => 1.EqualsAny([]).ShouldBeFalse();

        [Fact]
        public void WithComparer_MatchesCaseInsensitive()
            => "FOO".EqualsAny(["bar", "baz", "foo", "qux"], StringComparer.OrdinalIgnoreCase).ShouldBeTrue();

        [Fact]
        public void WithComparer_NoMatch_ReturnsFalse()
            => "z".EqualsAny(["a", "b", "c"], StringComparer.OrdinalIgnoreCase).ShouldBeFalse();
    }

    // ── Struct / value-type sanity check ──────────────────────────────────────

    public class StructTypes
    {
        [Fact]
        public void Int_Match() => 7.EqualsAny(7).ShouldBeTrue();

        [Fact]
        public void Double_Match() => 3.14.EqualsAny(3.14).ShouldBeTrue();

        [Fact]
        public void Enum_Match() => DayOfWeek.Monday.EqualsAny(DayOfWeek.Tuesday, DayOfWeek.Monday).ShouldBeTrue();

        [Fact]
        public void Enum_NoMatch() => DayOfWeek.Friday.EqualsAny(DayOfWeek.Monday, DayOfWeek.Tuesday).ShouldBeFalse();
    }
}