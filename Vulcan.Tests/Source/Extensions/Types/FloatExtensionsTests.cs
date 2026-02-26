using Vulcan.Extensions;

namespace Vulcan.Tests.Extensions.Types;

public class FloatExtensionsTests
{
    const float Tolerance = 1e-10f;

    public class Rounding
    {
        public class Round
        {
            [Theory]
            [InlineData(1, 1)]
            [InlineData(1.4, 1)]
            [InlineData(1.6, 2)]
            [InlineData(1.5, 2)]
            public void RoundTo(float input, float expected)
                => input.RoundTo().ShouldBe(expected, Tolerance);

            [Theory]
            [InlineData(1, 1, 1)]
            [InlineData(1.44, 0.1, 1.4)]
            [InlineData(1.66, 0.1, 1.7)]
            [InlineData(1.555, 0.01, 1.56)]
            [InlineData(11.111, 2, 12)]
            public void RoundTo_Precision(float input, float precision, float expected)
                => input.RoundTo(precision).ShouldBe(expected, Tolerance);

            [Theory]
            [InlineData(1, 1)]
            [InlineData(1.4, 1)]
            [InlineData(1.6, 2)]
            [InlineData(1.5, 2)]
            public void RoundToInt(float input, int expected)
                => input.RoundToInt().ShouldBe(expected);
        }

        public class Floor
        {
            [Theory]
            [InlineData(1, 1)]
            [InlineData(1.4, 1)]
            [InlineData(1.99, 1)]
            [InlineData(1.5, 1)]
            public void FloorTo(float input, float expected)
                => input.FloorTo().ShouldBe(expected, Tolerance);

            [Theory]
            [InlineData(1, 1, 1)]
            [InlineData(1.44, 0.1, 1.4)]
            [InlineData(1.66, 0.1, 1.6)]
            [InlineData(1.555, 0.01, 1.55)]
            [InlineData(11.111, 2, 10)]
            public void FloorTo_Precision(float input, float precision, float expected)
                => input.FloorTo(precision).ShouldBe(expected, Tolerance);

            [Theory]
            [InlineData(1, 1)]
            [InlineData(1.4, 1)]
            [InlineData(1.6, 1)]
            [InlineData(1.5, 1)]
            public void FloorToInt(float input, int expected)
                => input.FloorToInt().ShouldBe(expected);
        }

        public class Ceil
        {
            [Theory]
            [InlineData(1, 1)]
            [InlineData(1.4, 2)]
            [InlineData(1.6, 2)]
            [InlineData(1.5, 2)]
            public void CeilTo(float input, float expected)
                => input.CeilTo().ShouldBe(expected, Tolerance);

            [Theory]
            [InlineData(1, 1, 1)]
            [InlineData(1.44, 0.1, 1.5)]
            [InlineData(1.66, 0.1, 1.7)]
            [InlineData(1.555, 0.01, 1.56)]
            [InlineData(10.111, 2, 12)]
            public void CeilTo_Precision(float input, float precision, float expected)
                => input.CeilTo(precision).ShouldBe(expected, Tolerance);

            [Theory]
            [InlineData(1, 1)]
            [InlineData(1.4, 2)]
            [InlineData(1.6, 2)]
            [InlineData(1.5, 2)]
            public void CeilToInt(float input, int expected)
                => input.CeilToInt().ShouldBe(expected);
        }
    }

    public class NanToNull
    {
        [Theory]
        [InlineData(0,false)]
        [InlineData(15,false)]
        [InlineData(float.MaxValue,false)]
        [InlineData(float.MinValue,false)]
        [InlineData(float.PositiveInfinity,false)]
        [InlineData(float.NegativeInfinity,false)]
        [InlineData(float.NaN,true)]
        public void NanToNull_Value(float input, bool nullExpected)
            => input.NanToNull().ShouldBe(nullExpected ? null : input);
        
        [Theory]
        [InlineData(0,false)]
        [InlineData(15,false)]
        [InlineData(float.MaxValue,false)]
        [InlineData(float.MinValue,false)]
        [InlineData(float.PositiveInfinity,false)]
        [InlineData(float.NegativeInfinity,false)]
        [InlineData(float.NaN,true)]
        public void NanToNull_Nullable(float input, bool nullExpected)
            => ((float?)input).NanToNull().ShouldBe(nullExpected ? null : input);
    }

    public class Modulus
    {
        [Theory]
        [InlineData(5f, 3f, 2f)]
        [InlineData(4f, 2f, 0f)]
        [InlineData(0f, 3f, 0f)]
        [InlineData(-5f, 3f, 1f)]
        [InlineData(-4f, 3f, 2f)]
        [InlineData(-1f, 3f, 2f)]
        [InlineData(5f, -3f, -1f)]
        [InlineData(-5f, -3f, -2f)]
        [InlineData(7.5f, 2.5f, 0f)]
        [InlineData(7.5f, 2f, 1.5f)]
        [InlineData(-7.5f, 2f, 0.5f)]
        public void Mod_Float(float input, float mod, float expected)
            => input.Mod(mod).ShouldBe(expected, Tolerance);

        [Theory]
        [InlineData(5f, 3, 2f)]
        [InlineData(4f, 2, 0f)]
        [InlineData(0f, 3, 0f)]
        [InlineData(-5f, 3, 1f)]
        [InlineData(-4f, 3, 2f)]
        [InlineData(-1f, 3, 2f)]
        [InlineData(5f, -3, -1f)]
        [InlineData(-5f, -3, -2f)]
        [InlineData(7.5f, 2, 1.5f)]
        [InlineData(-7.5f, 2, 0.5f)]
        public void Mod_Int(float input, int mod, float expected)
            => input.Mod(mod).ShouldBe(expected, Tolerance);
    }

    [Theory]
    // innerhalb
    [InlineData(5f, 0f, 10f, 5f)]
    [InlineData(0f, 0f, 10f, 0f)]
    [InlineData(10f, 0f, 10f, 10f)]
    [InlineData(5.5f, 0f, 10f, 5.5f)]
    // unter min
    [InlineData(-1f, 0f, 10f, 0f)]
    [InlineData(-100f, -50f, 50f, -50f)]
    [InlineData(-0.1f, 0f, 1f, 0f)]
    // über max
    [InlineData(11f, 0f, 10f, 10f)]
    [InlineData(100f, -50f, 50f, 50f)]
    [InlineData(1.1f, 0f, 1f, 1f)]
    // negative ranges
    [InlineData(-5f, -10f, -1f, -5f)]
    [InlineData(-15f, -10f, -1f, -10f)]
    [InlineData(0f, -10f, -1f, -1f)]
    // gemischte vorzeichen
    [InlineData(-5f, -2f, 2f, -2f)]
    [InlineData(5f, -2f, 2f, 2f)]
    // degenerate (min == max)
    [InlineData(5f, 3f, 3f, 3f)]
    [InlineData(3f, 3f, 3f, 3f)]
    [InlineData(1f, 3f, 3f, 3f)]
    public void Clamp(float input, float min, float max, float expected)
        => input.Clamp(min, max).ShouldBe(expected, Tolerance);

    [Theory]
    // exakt gleich
    [InlineData(5f, 5f, 0f, true)]
    [InlineData(5f, 5f, 0.0001f, true)]
    // innerhalb der precision
    [InlineData(5f, 5.1f, 0.1f, true)]
    [InlineData(5f, 4.9f, 0.1f, true)]
    [InlineData(5f, 5.00001f, 0.001f, true)]
    [InlineData(-5f, -5.05f, 0.1f, true)]
    // exakt auf der Grenze
    [InlineData(5f, 5.1f, 0.1f, true)]
    [InlineData(5f, 4.9f, 0.1f, true)]
    [InlineData(-5f, -4.9f, 0.1f, true)]
    // außerhalb der precision
    [InlineData(5f, 5.11f, 0.1f, false)]
    [InlineData(5f, 4.89f, 0.1f, false)]
    [InlineData(-5f, -4.8f, 0.1f, false)]
    // große Werte
    [InlineData(1_000_000f, 1_000_000.5f, 1f, true)]
    [InlineData(1_000_000f, 1_000_002f, 1f, false)]
    // zero cases
    [InlineData(0f, 0f, 0f, true)]
    [InlineData(0f, 0.0001f, 0.001f, true)]
    [InlineData(0f, 0.01f, 0.001f, false)]
    public void Approximately(float input, float operanf, float precision, bool expected)
        => input.Approximately(operanf, precision).ShouldBe(expected);
}