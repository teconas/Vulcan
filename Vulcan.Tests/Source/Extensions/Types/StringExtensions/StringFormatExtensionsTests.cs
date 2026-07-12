using System.Diagnostics.CodeAnalysis;
using Vulcan.Extensions;

namespace Vulcan.Tests.Extensions.Types.StringExtensions;

[TestSubject(typeof(StringFormatExtensions))]
[SuppressMessage("ReSharper", "RedundantArgumentDefaultValue")]
public class StringFormatExtensionsTests
{
    [Fact]
    public void Format_SingleArg_Positional()
        => "Hello {0}".Format("World").ShouldBe("Hello World");

    [Fact]
    public void Format_TwoArgs_Positional()
        => "{0} {1}".Format("a", "b").ShouldBe("a b");

    [Fact]
    public void Format_ThreeArgs_Positional()
        => "{0}{1}{2}".Format("a", "b", "c").ShouldBe("abc");

    [Fact]
    public void Format_FourArgs_Positional()
        => "{0}{1}{2}{3}".Format("a", "b", "c", "d").ShouldBe("abcd");

    [Fact]
    public void Format_ParamsArgs_Positional()
        => "{0}{1}{2}{3}{4}".Format("a", "b", "c", "d", "e").ShouldBe("abcde");

    [Fact]
    public void Format_ReuseSameIndex_RepeatsValue()
        => "{0}{0}{0}".Format("x").ShouldBe("xxx");

    [Fact]
    public void Format_OutOfOrderIndexes()
        => "{1}{0}".Format("a", "b").ShouldBe("ba");

    [Fact]
    public void Format_TooFewArgsForHighestIndex_Throws()
        => Should.Throw<FormatException>(() => "{0}{1}".Format("only-one"));

    [Fact]
    public void Format_MalformedTemplate_Throws()
        => Should.Throw<FormatException>(() => "{0".Format("x"));

    [Fact]
    public void Format_EscapedBraces_RenderLiteral()
        => "{{{0}}}".Format("x").ShouldBe("{x}");

    [Fact]
    public void Format_NoPlaceholders_ReturnsTemplateUnchanged()
        => "no placeholders".Format("ignored").ShouldBe("no placeholders");

    [Fact]
    public void Format_NullArg_RendersEmpty()
        => "[{0}]".Format((object?)null).ShouldBe("[]");

    [Fact]
    public void Format_HexSpecifier_CultureAgnostic()
        => "{0:X}".Format(255).ShouldBe("FF");

    [Fact]
    public void Format_ZeroPaddedSpecifier_CultureAgnostic()
        => "{0:D3}".Format(7).ShouldBe("007");

    [Fact]
    public void Format_ZeroArgs_BindsParamsOverload_ReturnsSourceVerbatim()
        => "no args".Format().ShouldBe("no args");

    [Fact]
    public void Format_ZeroArgs_WithPlaceholder_Throws()
        => Should.Throw<FormatException>(() => "{0}".Format());

    [Fact]
    public void Format_ObjectArray_SpreadsViaParams()
        => "{0}-{1}".Format(new object?[] { "a", "b" }).ShouldBe("a-b");
}
