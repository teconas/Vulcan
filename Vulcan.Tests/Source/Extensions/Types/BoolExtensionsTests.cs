using System.Globalization;
using Vulcan.Extensions;
using static Vulcan.DeferTool;

namespace Vulcan.Tests.Extensions.Types;

[TestSubject(typeof(BoolExtensions))]
public static class BoolExtensionsTests
{
    public class TryParse
    {
        [Theory]
        [InlineData("true"), InlineData("1"), InlineData("YES"), InlineData("y")]
        [InlineData("jA"), InlineData("J"), InlineData("ok"), InlineData("okay")]
        [InlineData("TRUE"), InlineData("Ja"), InlineData(" true "), InlineData("\tyes\n")]
        [InlineData("t"), InlineData("wahr"), InlineData("yeah"), InlineData("yep"), InlineData("yup")]
        [InlineData("sure"), InlineData("jo"), InlineData("jep"), InlineData("jup")]
        [InlineData("oui"), InlineData("si"), InlineData("Sí"), InlineData("sì"), InlineData("k"), InlineData("KK")]
        public void Truthy(string input)
            => bool.TryParse(input).ShouldBe(true);

        [Theory]
        [InlineData("TRUE", true), InlineData("YES", true), InlineData("NEIN", false)]
        public void IsCultureInvariant(string input, bool expected)
        {
            var original = CultureInfo.CurrentCulture;
            using var restore = Defer(() => CultureInfo.CurrentCulture = original);
            CultureInfo.CurrentCulture = new CultureInfo("tr-TR");

            bool.TryParse(input).ShouldBe(expected);
        }

        [Theory]
        [InlineData("false"), InlineData("0"), InlineData("NO"), InlineData("n")]
        [InlineData("nEiN"), InlineData("ne"), InlineData("nö"), InlineData("nope")]
        [InlineData("nop"), InlineData("f"), InlineData("Falsch"), InlineData("nah"), InlineData("nee")]
        [InlineData("noe"), InlineData("NÖ"), InlineData("non")]
        public void Falsy(string? input)
            => bool.TryParse(input).ShouldBe(false);

        [Theory]
        [InlineData(""), InlineData(null), InlineData("idk"), InlineData("wtf")]
        [InlineData("15"), InlineData("-1"), InlineData(" "), InlineData("maybe")]
        [InlineData("true\0"), InlineData("yes please"), InlineData("o"), InlineData("none")]
        [InlineData("null"), InlineData("x"), InlineData("+"), InlineData("-")]
        public void Unknown(string? input)
            => bool.TryParse(input).ShouldBe(null);
    }

    public class Parse
    {
        [Theory]
        [InlineData(1), InlineData(2), InlineData(-3), InlineData(4), InlineData(-5)]
        public void TruthyNumber(int input)
            => bool.Parse(input).ShouldBeTrue();

        [Fact]
        public void FalsyNumber()
            => bool.Parse(0).ShouldBeFalse();
    }
}
