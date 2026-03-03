using Vulcan.Extensions;

namespace Vulcan.Tests.Extensions.Types;

[TestSubject(typeof(IntegerExtensions))]
public static class IntegerExtensionsTests
{
    public class AsFloat
    {
        [Theory]
        [InlineData(0, 0f)]
        [InlineData(1, 1f)]
        [InlineData(-1, -1f)]
        [InlineData(100, 100f)]
        [InlineData(-100, -100f)]
        public void Converts(int input, float expected)
            => input.AsFloat().ShouldBe(expected);
    }

    public class Modulus
    {
        [Theory]
        // normal
        [InlineData(5, 3, 2)]
        [InlineData(4, 2, 0)]
        [InlineData(0, 3, 0)]
        [InlineData(6, 3, 0)]
        // negative dividend (the purpose of this method)
        [InlineData(-5, 3, 1)]
        [InlineData(-4, 3, 2)]
        [InlineData(-1, 3, 2)]
        [InlineData(-6, 4, 2)]
        // negative mod
        [InlineData(5, -3, -1)]
        [InlineData(-5, -3, -2)]
        public void Mod(int input, int mod, int expected)
            => input.Mod(mod).ShouldBe(expected);
    }
}
