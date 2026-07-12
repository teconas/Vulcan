using System.Diagnostics.CodeAnalysis;
using Vulcan.Extensions;

namespace Vulcan.Tests.Extensions.Types.StringExtensions;

[TestSubject(typeof(Vulcan.Extensions.StringExtensions))]
[SuppressMessage("ReSharper", "RedundantArgumentDefaultValue")]
public class StringExtensionsTests
{
    [Theory]
    [InlineData("ab", 0, "")]
    [InlineData("ab", 1, "ab")]
    [InlineData("ab", 3, "ababab")]
    [InlineData("", 0, "")]
    [InlineData("", 5, "")]
    public void Times_String(string source, int num, string expected)
        => source.Times(num).ShouldBe(expected);

    [Fact]
    public void Times_String_Negative_Throws()
        => Should.Throw<ArgumentOutOfRangeException>(() => "x".Times(-1));

    [Theory]
    [InlineData('x', 0, "")]
    [InlineData('x', 1, "x")]
    [InlineData('-', 5, "-----")]
    public void Times_Char(char source, int num, string expected)
        => source.Times(num).ShouldBe(expected);

    [Fact]
    public void Times_Char_Negative_Throws()
        => Should.Throw<ArgumentOutOfRangeException>(() => 'x'.Times(-1));

    [Theory]
    [MemberData(nameof(SplitLinesData))]
    public void SplitLines(string source, StringSplitOptions options, string[] expected)
        => source.SplitLines(options).ShouldBe(expected);

    public static TheoryData<string, StringSplitOptions, string[]> SplitLinesData()
    {
        return new TheoryData<string, StringSplitOptions, string[]>
        {
            // Each separator variant splits
            { "a\r\nb", StringSplitOptions.None, new[] { "a", "b" } },
            { "a\nb", StringSplitOptions.None, new[] { "a", "b" } },
            { "a\rb", StringSplitOptions.None, new[] { "a", "b" } },
            // CRLF is a single break, not two
            { "a\r\nb\nc\rd", StringSplitOptions.None, new[] { "a", "b", "c", "d" } },
            // Single line
            { "abc", StringSplitOptions.None, new[] { "abc" } },
            // Empty source
            { "", StringSplitOptions.None, new[] { "" } },
            { "", StringSplitOptions.RemoveEmptyEntries, Array.Empty<string>() },
            // Trailing newline
            { "a\n", StringSplitOptions.None, new[] { "a", "" } },
            { "a\n", StringSplitOptions.RemoveEmptyEntries, new[] { "a" } },
            // Interior blank lines
            { "a\n\nb", StringSplitOptions.None, new[] { "a", "", "b" } },
            { "a\n\nb", StringSplitOptions.RemoveEmptyEntries, new[] { "a", "b" } }
        };
    }
}
