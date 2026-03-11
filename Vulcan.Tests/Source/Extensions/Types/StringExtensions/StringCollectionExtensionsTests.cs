using Vulcan.Extensions;

namespace Vulcan.Tests.Extensions.Types.StringExtensions;

[TestSubject(typeof(StringCollectionExtensions))]
public static class StringCollectionExtensionsTests
{
    public class WhereSet
    {
        [Fact]
        public void RemovesNullEntries()
            => new[] { "a", null, "b" }.WhereSet().ShouldBe(["a", "b"]);

        [Fact]
        public void RemovesEmptyEntries()
            => new[] { "a", "", "b" }.WhereSet().ShouldBe(["a", "b"]);

        [Fact]
        public void KeepsWhitespaceEntries()
            => new[] { "a", " ", "b" }.WhereSet().ShouldBe(["a", " ", "b"]);

        [Fact]
        public void AllEmpty_ReturnsEmpty()
            => new[] { null, "", null }.WhereSet().ShouldBeEmpty();

        [Fact]
        public void Empty_ReturnsEmpty()
            => Array.Empty<string?>().WhereSet().ShouldBeEmpty();
    }

    public class WhereTrimmedSet
    {
        [Fact]
        public void RemovesNullEntries()
            => new[] { "a", null, "b" }.WhereTrimmedSet().ShouldBe(["a", "b"]);

        [Fact]
        public void RemovesEmptyEntries()
            => new[] { "a", "", "b" }.WhereTrimmedSet().ShouldBe(["a", "b"]);

        [Fact]
        public void RemovesWhitespaceEntries()
            => new[] { "a", " ", "b" }.WhereTrimmedSet().ShouldBe(["a", "b"]);

        [Fact]
        public void AllBlank_ReturnsEmpty()
            => new[] { null, "", "  " }.WhereTrimmedSet().ShouldBeEmpty();

        [Fact]
        public void Empty_ReturnsEmpty()
            => Array.Empty<string?>().WhereTrimmedSet().ShouldBeEmpty();
    }

    public class TrimAndFilter
    {
        [Fact]
        public void TrimsEntries()
            => new[] { " a ", " b " }.TrimAndFilter().ShouldBe(["a", "b"]);

        [Fact]
        public void RemovesNullEntries()
            => new[] { "a", null, "b" }.TrimAndFilter().ShouldBe(["a", "b"]);

        [Fact]
        public void RemovesEmptyEntries()
            => new[] { "a", "", "b" }.TrimAndFilter().ShouldBe(["a", "b"]);

        [Fact]
        public void RemovesWhitespaceOnlyEntries()
            => new[] { "a", "   ", "b" }.TrimAndFilter().ShouldBe(["a", "b"]);

        [Fact]
        public void Empty_ReturnsEmpty()
            => Array.Empty<string?>().TrimAndFilter().ShouldBeEmpty();
    }

    public class TrimAndFilterWithChars
    {
        [Fact]
        public void TrimsSpecifiedChars()
            => new[] { "-a-", "-b-" }.TrimAndFilter('-').ShouldBe(["a", "b"]);

        [Fact]
        public void RemovesEntriesReducedToEmpty()
            => new[] { "a", "---", "b" }.TrimAndFilter('-').ShouldBe(["a", "b"]);

        [Fact]
        public void DoesNotTrimOtherChars()
            => new[] { " a " }.TrimAndFilter('-').ShouldBe([" a "]);

        [Fact]
        public void RemovesNullEntries()
            => new[] { "a", null, "b" }.TrimAndFilter('-').ShouldBe(["a", "b"]);

        [Fact]
        public void Empty_ReturnsEmpty()
            => Array.Empty<string?>().TrimAndFilter('-').ShouldBeEmpty();
    }
}
