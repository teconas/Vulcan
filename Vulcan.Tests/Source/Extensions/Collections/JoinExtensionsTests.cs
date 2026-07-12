using Vulcan.Extensions;

namespace Vulcan.Tests.Extensions.Collections;

[TestSubject(typeof(JoinExtensions))]
public static class JoinExtensionsTests
{
    public class Join
    {
        private record Token(string Value)
        {
            public override string ToString() => Value;
        }

        [Fact]
        public void StringSeparator_Ints_JoinsWithSeparator()
            => new[] { 1, 2, 3 }.Join(", ").ShouldBe("1, 2, 3");

        [Fact]
        public void StringSeparator_Strings_JoinsWithSeparator()
            => new[] { "a", "b", "c" }.Join("-").ShouldBe("a-b-c");

        [Fact]
        public void StringSeparator_Empty_ReturnsEmpty()
            => Array.Empty<int>().Join(",").ShouldBeEmpty();

        [Fact]
        public void StringSeparator_SingleElement_NoSeparator()
            => new[] { 42 }.Join(",").ShouldBe("42");

        [Fact]
        public void StringSeparator_EmptySeparator_Concatenates()
            => new[] { "a", "b" }.Join("").ShouldBe("ab");

        [Fact]
        public void StringSeparator_MultiChar_JoinsWithSeparator()
            => new[] { "a", "b" }.Join(" | ").ShouldBe("a | b");

        [Fact]
        public void StringSeparator_NullElements_RenderEmpty()
            => new[] { "a", null, "b" }.Join(",").ShouldBe("a,,b");

        [Fact]
        public void StringSeparator_PreservesOrder()
            => new[] { 3, 1, 2 }.Join(",").ShouldBe("3,1,2");

        [Fact]
        public void StringSeparator_CustomType_UsesToString()
            => new[] { new Token("x"), new Token("y") }.Join(",").ShouldBe("x,y");

        [Fact]
        public void StringSeparator_LazySequence_IsEnumerated()
            => Enumerable.Range(1, 3).Select(x => x * 2).Join(",").ShouldBe("2,4,6");

        [Fact]
        public void CharSeparator_Ints_JoinsWithSeparator()
            => new[] { 1, 2, 3 }.Join(',').ShouldBe("1,2,3");

        [Fact]
        public void CharSeparator_Strings_JoinsWithSeparator()
            => new[] { "a", "b" }.Join('-').ShouldBe("a-b");

        [Fact]
        public void CharSeparator_Empty_ReturnsEmpty()
            => Array.Empty<int>().Join(',').ShouldBeEmpty();

        [Fact]
        public void CharSeparator_SingleElement_NoSeparator()
            => new[] { 42 }.Join(',').ShouldBe("42");

        [Fact]
        public void CharSeparator_MatchesStringSeparator()
        {
            // Arrange
            var seq = new[] { "a", "b", "c" };

            // Act
            var viaChar = seq.Join(',');
            var viaString = seq.Join(",");

            // Assert
            viaChar.ShouldBe(viaString);
        }
    }
}
