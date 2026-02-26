using Vulcan.Extensions;

namespace Vulcan.Tests.Extensions.Types;

public class DoubleExtensionsTests
{
    const double Tolerance = 1e-10;

    public class Rounding
    {
        public class Round
        {
            [Theory]
            [InlineData(1, 1)]
            [InlineData(1.4, 1)]
            [InlineData(1.6, 2)]
            [InlineData(1.5, 2)]
            public void RoundTo(double input, double expected)
                => input.RoundTo().ShouldBe(expected, Tolerance);

            [Theory]
            [InlineData(1, 1, 1)]
            [InlineData(1.44, 0.1, 1.4)]
            [InlineData(1.66, 0.1, 1.7)]
            [InlineData(1.555, 0.01, 1.56)]
            [InlineData(11.111, 2, 12)]
            public void RoundTo_Precision(double input, double precision, double expected)
                => input.RoundTo(precision).ShouldBe(expected, Tolerance);

            [Theory]
            [InlineData(1, 1)]
            [InlineData(1.4, 1)]
            [InlineData(1.6, 2)]
            [InlineData(1.5, 2)]
            public void RoundToInt(double input, int expected)
                => input.RoundToInt().ShouldBe(expected);
        }

        public class Floor
        {
            [Theory]
            [InlineData(1, 1)]
            [InlineData(1.4, 1)]
            [InlineData(1.99, 1)]
            [InlineData(1.5, 1)]
            public void FloorTo(double input, double expected)
                => input.FloorTo().ShouldBe(expected, Tolerance);

            [Theory]
            [InlineData(1, 1, 1)]
            [InlineData(1.44, 0.1, 1.4)]
            [InlineData(1.66, 0.1, 1.6)]
            [InlineData(1.555, 0.01, 1.55)]
            [InlineData(11.111, 2, 10)]
            public void FloorTo_Precision(double input, double precision, double expected)
                => input.FloorTo(precision).ShouldBe(expected, Tolerance);

            [Theory]
            [InlineData(1, 1)]
            [InlineData(1.4, 1)]
            [InlineData(1.6, 1)]
            [InlineData(1.5, 1)]
            public void FloorToInt(double input, int expected)
                => input.FloorToInt().ShouldBe(expected);
        }

        public class Ceil
        {
            [Theory]
            [InlineData(1, 1)]
            [InlineData(1.4, 2)]
            [InlineData(1.6, 2)]
            [InlineData(1.5, 2)]
            public void CeilTo(double input, double expected)
                => input.CeilTo().ShouldBe(expected, Tolerance);

            [Theory]
            [InlineData(1, 1, 1)]
            [InlineData(1.44, 0.1, 1.5)]
            [InlineData(1.66, 0.1, 1.7)]
            [InlineData(1.555, 0.01, 1.56)]
            [InlineData(10.111, 2, 12)]
            public void CeilTo_Precision(double input, double precision, double expected)
                => input.CeilTo(precision).ShouldBe(expected, Tolerance);

            [Theory]
            [InlineData(1, 1)]
            [InlineData(1.4, 2)]
            [InlineData(1.6, 2)]
            [InlineData(1.5, 2)]
            public void CeilToInt(double input, int expected)
                => input.CeilToInt().ShouldBe(expected);
        }
    }

    public class NanToNull
    {
        [Theory]
        [InlineData(0,false)]
        [InlineData(15,false)]
        [InlineData(double.MaxValue,false)]
        [InlineData(double.MinValue,false)]
        [InlineData(double.PositiveInfinity,false)]
        [InlineData(double.NegativeInfinity,false)]
        [InlineData(double.NaN,true)]
        public void NanToNull_Value(double input, bool nullExpected)
            => input.NanToNull().ShouldBe(nullExpected ? null : input);
        
        [Theory]
        [InlineData(0,false)]
        [InlineData(15,false)]
        [InlineData(double.MaxValue,false)]
        [InlineData(double.MinValue,false)]
        [InlineData(double.PositiveInfinity,false)]
        [InlineData(double.NegativeInfinity,false)]
        [InlineData(double.NaN,true)]
        public void NanToNull_Nullable(double input, bool nullExpected)
            => ((double?)input).NanToNull().ShouldBe(nullExpected ? null : input);
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
        public void Mod_Float(double input, double mod, double expected)
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
        public void Mod_Int(double input, int mod, double expected)
            => input.Mod(mod).ShouldBe(expected, Tolerance);
    }

    [Theory]
    // innerhalb
    [InlineData(5d, 0d, 10d, 5d)]
    [InlineData(0d, 0d, 10d, 0d)]
    [InlineData(10d, 0d, 10d, 10d)]
    [InlineData(5.5d, 0d, 10d, 5.5d)]
    // unter min
    [InlineData(-1d, 0d, 10d, 0d)]
    [InlineData(-100d, -50d, 50d, -50d)]
    [InlineData(-0.1d, 0d, 1d, 0d)]
    // über max
    [InlineData(11d, 0d, 10d, 10d)]
    [InlineData(100d, -50d, 50d, 50d)]
    [InlineData(1.1d, 0d, 1d, 1d)]
    // negative ranges
    [InlineData(-5d, -10d, -1d, -5d)]
    [InlineData(-15d, -10d, -1d, -10d)]
    [InlineData(0d, -10d, -1d, -1d)]
    // gemischte vorzeichen
    [InlineData(-5d, -2d, 2d, -2d)]
    [InlineData(5d, -2d, 2d, 2d)]
    // degenerate (min == max)
    [InlineData(5d, 3d, 3d, 3d)]
    [InlineData(3d, 3d, 3d, 3d)]
    [InlineData(1d, 3d, 3d, 3d)]
    public void Clamp(double input, double min, double max, double expected)
        => input.Clamp(min, max).ShouldBe(expected, Tolerance);

    [Theory]
    // exakt gleich
    [InlineData(5d, 5d, 0d, true)]
    [InlineData(5d, 5d, 0.0001d, true)]
    // innerhalb der precision
    [InlineData(5d, 5.1d, 0.1d, true)]
    [InlineData(5d, 4.9d, 0.1d, true)]
    [InlineData(5d, 5.00001d, 0.001d, true)]
    [InlineData(-5d, -5.05d, 0.1d, true)]
    // exakt auf der Grenze
    [InlineData(5d, 5.1d, 0.1d, true)]
    [InlineData(5d, 4.9d, 0.1d, true)]
    [InlineData(-5d, -4.9d, 0.1d, true)]
    // außerhalb der precision
    [InlineData(5d, 5.11d, 0.1d, false)]
    [InlineData(5d, 4.89d, 0.1d, false)]
    [InlineData(-5d, -4.8d, 0.1d, false)]
    // große Werte
    [InlineData(1_000_000d, 1_000_000.5d, 1d, true)]
    [InlineData(1_000_000d, 1_000_002d, 1d, false)]
    // zero cases
    [InlineData(0d, 0d, 0d, true)]
    [InlineData(0d, 0.0001d, 0.001d, true)]
    [InlineData(0d, 0.01d, 0.001d, false)]
    public void Approximately(double input, double operand, double precision, bool expected)
        => input.Approximately(operand, precision).ShouldBe(expected);
}