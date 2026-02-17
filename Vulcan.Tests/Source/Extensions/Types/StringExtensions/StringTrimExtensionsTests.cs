using System.Diagnostics.CodeAnalysis;
using Vulcan.Extensions;

namespace Vulcan.Tests.Extensions.Types.StringExtensions;

[TestSubject(typeof(StringTrimExtensions))]
[SuppressMessage("ReSharper", "RedundantArgumentDefaultValue")]
public class StringTrimExtensionsTests
{
    const string Text = "Text";

    [Theory]
    [MemberData(nameof(TestData))]
    public void TrimStart_CaseSensitive(string prefix, string token, string expectedTemplate, string? _)
    {
        // Arrange
        var input = GetInput(prefix, true, false);
        var expected = GetInput(expectedTemplate, true, false);

        // Act
        var result = input.TrimStart(token, StringComparison.Ordinal);

        // Assert
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void TrimStart_CaseInvariant(string prefix, string token, string expectedTemplate, string? expectedInvariantTemplate)
    {
        // Arrange
        var input = GetInput(prefix, true, false);
        var expected = GetInput(expectedInvariantTemplate ?? expectedTemplate, true, false);

        // Act
        var result = input.TrimStart(token, StringComparison.OrdinalIgnoreCase);

        // Assert
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void TrimEnd_CaseSensitive(string suffix, string token, string expectedTemplate, string? _)
    {
        // Arrange
        var input = GetInput(suffix, false, true);
        var expected = GetInput(expectedTemplate, false, true);

        // Act
        var result = input.TrimEnd(token, StringComparison.Ordinal);

        // Assert
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void TrimEnd_CaseInvariant(string suffix, string token, string expectedTemplate, string? expectedInvariantTemplate)
    {
        // Arrange
        var input = GetInput(suffix, false, true);
        var expected = GetInput(expectedInvariantTemplate ?? expectedTemplate, false, true);

        // Act
        var result = input.TrimEnd(token, StringComparison.OrdinalIgnoreCase);

        // Assert
        result.ShouldBe(expected);
    }
    
    [Theory]
    [MemberData(nameof(TestData))]
    public void Trim_CaseSensitive(string suffix, string token, string expectedTemplate, string? _)
    {
        // Arrange
        var input = GetInput(suffix, true, true);
        var expected = GetInput(expectedTemplate, true, true);

        // Act
        var result = input.Trim(token, StringComparison.Ordinal);

        // Assert
        result.ShouldBe(expected);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void Trim_CaseInvariant(string suffix, string token, string expectedTemplate, string? expectedInvariantTemplate)
    {
        // Arrange
        var input = GetInput(suffix, true, true);
        var expected = GetInput(expectedInvariantTemplate ?? expectedTemplate, true, true);

        // Act
        var result = input.Trim(token, StringComparison.OrdinalIgnoreCase);

        // Assert
        result.ShouldBe(expected);
    }

    public static TheoryData<string, string, string, string?> TestData()
    {
        return new TheoryData<string, string, string, string?>
        {
            { "", "Token", "", null },
            { "Token", "Token", "", null },
            { "TokenToken", "Token", "", null },
            { "Token|Token", "token", "Token|Token", "" },
            { "token|Token", "token", "Token", "" },
            { "Token", "", "Token", null }
        };
    }

    static string GetInput(string inputToken, bool start = false, bool end = false)
    {
        var prefix = start ? inputToken.Replace("|", string.Empty) : string.Empty;
        var suffix = end ? inputToken.Split('|').Reverse().Join("") : string.Empty;
        
        return prefix + Text + suffix;
    }
}