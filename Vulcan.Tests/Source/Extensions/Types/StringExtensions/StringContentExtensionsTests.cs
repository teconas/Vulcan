using Vulcan.Extensions;

namespace Vulcan.Tests.Extensions.Types.StringExtensions;

[TestSubject(typeof(StringContentExtensions))]
public class StringContentExtensionsTests
{
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", true)]
    [InlineData("   ", true)]
    [InlineData("\t", true)]
    [InlineData("x", true)]
    [InlineData(" x ", true)]
    public void IsSet(string? value, bool expected)
    {
        // Act
        var result = value.IsSet();

        // Assert
        result.ShouldBe(expected);
    }

    [Theory]
    [InlineData(null, true)]
    [InlineData("", true)]
    [InlineData(" ", false)]
    [InlineData("   ", false)]
    [InlineData("\t", false)]
    [InlineData("x", false)]
    [InlineData(" x ", false)]
    public void IsNotSet(string? value, bool expected)
    {
        // Act
        var result = value.IsNotSet();

        // Assert
        result.ShouldBe(expected);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("", null)]
    [InlineData(" ", " ")]
    [InlineData("\t", "\t")]
    [InlineData("x", "x")]
    [InlineData(" x ", " x ")]
    public void EmptyToNull(string? value, string? expected)
    {
        // Act
        var result = value.EmptyToNull();

        // Assert
        result.ShouldBe(expected);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("", null)]
    [InlineData(" ", null)]
    [InlineData("   ", null)]
    [InlineData("\t", null)]
    [InlineData("x", "x")]
    [InlineData(" x ", " x ")]
    public void EmptyOrWhitespaceToNull(string? value, string? expected)
    {
        // Act
        var result = value.EmptyOrWhitespaceToNull();

        // Assert
        result.ShouldBe(expected);
    }
}
