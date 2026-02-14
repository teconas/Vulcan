using JetBrains.Annotations;

namespace Vulcan.Tests.Helper;

[TestSubject(typeof(BoolExtensions))]
public static class BoolExtensionsTests
{
    public class TryParse
    {
        [Theory]
        [InlineData("true"), InlineData("1"), InlineData("YES"), InlineData("y")]
        [InlineData("jA"), InlineData("J"), InlineData("ok"), InlineData("okay")]
        public void Truthy(string input)
            => bool.TryParse(input).ShouldBe(true);

        [Theory]
        [InlineData("false"), InlineData("0"), InlineData("NO"), InlineData("n")]
        [InlineData("nEiN"), InlineData("ne"), InlineData("nö"), InlineData("nope")]
        [InlineData("nop")]
        public void Falsy(string? input)
            => bool.TryParse(input).ShouldBe(false);

        [Theory]
        [InlineData(""), InlineData(null), InlineData("idk"), InlineData("wtf")]
        [InlineData("15"), InlineData("-1"), InlineData(" ")]
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