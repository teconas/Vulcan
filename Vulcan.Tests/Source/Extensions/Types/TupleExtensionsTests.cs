using Vulcan.Extensions;

namespace Vulcan.Tests.Extensions.Types;

[TestSubject(typeof(Vulcan.Extensions.TupleExtensions))]
public static class TupleExtensionsTests
{
    public class Swap
    {
        [Fact]
        public void SwapsElements()
            => (1, 2).Swap().ShouldBe((2, 1));

        [Theory]
        [InlineData("a", "b"), InlineData("x", "y")]
        public void SwapsPairs(string first, string second)
            => (first, second).Swap().ShouldBe((second, first));
    }
}
