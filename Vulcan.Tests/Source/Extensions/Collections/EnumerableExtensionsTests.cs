using Vulcan.Extensions;

namespace Vulcan.Tests.Extensions.Collections;

[TestSubject(typeof(EnumerableExtensions))]
public static class EnumerableExtensionsTests
{
    public class ForEach
    {
         readonly int[] _simpleArray = [1, 2, 3];

        [Fact]
        public void CallsActionForEachElement()
        {
            // Arrange
            var result = new List<int>();

            // Act
            _simpleArray.ForEach(result.Add);

            // Assert
            result.ShouldBe(_simpleArray);
        }

        [Fact]
        public void ReturnsCollection()
            => _simpleArray.ForEach(_ => { }).ShouldBeOfType<int[]>();

        [Fact]
        public void Empty_DoesNotCallAction()
        {
            // Arrange
            var called = new LinearBool();

            // Act
            Array.Empty<int>().ForEach(_ => called.Set());

            // Assert
            called.ShouldBeFalse();
        }
    }

    public class SelectMany
    {
        [Fact]
        public void FlattensNestedEnumerable()
            => new[] { new[] { 1, 2 }, new[] { 3, 4 } }.SelectMany().ShouldBe([1, 2, 3, 4]);

        [Fact]
        public void Empty_ReturnsEmpty()
            => Array.Empty<int[]>().SelectMany().ShouldBeEmpty();

        [Fact]
        public void EmptyInner_IsSkipped()
            => new[] { new[] { 1 }, Array.Empty<int>(), new[] { 2 } }.SelectMany().ShouldBe([1, 2]);
    }

    public class Distinct
    {
        [Fact]
        public void KeepsFirstOccurrence()
        {
            // Arrange

            // Act
            var result = new[] { (1, "a"), (2, "b"), (1, "c") }.Distinct(x => x.Item1).ToArray();

            // Assert
            result.ShouldBe([(1, "a"), (2, "b")]);
        }

        [Fact]
        public void AllUnique_ReturnsAll()
            => new[] { 1, 2, 3 }.Distinct(x => x).ShouldBe([1, 2, 3]);

        [Fact]
        public void AllSameKey_ReturnsSingle()
            => new[] { "a", "b", "c" }.Distinct(x => x.Length).Count().ShouldBe(1);

        [Fact]
        public void Empty_ReturnsEmpty()
            => Array.Empty<int>().Distinct(x => x).ShouldBeEmpty();
    }

    public class WhereNotNull
    {
        [Fact]
        public void RemovesNullElements()
            => new[] { "a", null, "b", null, "c" }.WhereNotNull().ShouldBe(["a", "b", "c"]);

        [Fact]
        public void NullSource_ReturnsEmpty()
            => ((string?[]?)null).WhereNotNull().ShouldBeEmpty();

        [Fact]
        public void NoNulls_ReturnsAll()
            => new[] { "x", "y", "z" }.WhereNotNull().ShouldBe(["x", "y", "z"]);

        [Fact]
        public void AllNull_ReturnsEmpty()
            => new string?[] { null, null }.WhereNotNull().ShouldBeEmpty();
    }

    public class None
    {
        readonly int[] _sourceArray = [1, 2, 3];
        
        [Fact]
        public void Empty_ReturnsTrue()
            => Array.Empty<int>().None().ShouldBeTrue();

        [Fact]
        public void NonEmpty_ReturnsFalse()
            => _sourceArray.None().ShouldBeFalse();

        [Fact]
        public void WithPredicate_AllMatch_ReturnsFalse()
            => _sourceArray.None(x => x > 0).ShouldBeFalse();

        [Fact]
        public void WithPredicate_NoneMatch_ReturnsTrue()
            => _sourceArray.None(x => x > 10).ShouldBeTrue();

        [Fact]
        public void WithPredicate_SomeMatch_ReturnsFalse()
            => _sourceArray.None(x => x > 2).ShouldBeFalse();

        [Fact]
        public void WithPredicate_Empty_ReturnsTrue()
            => Array.Empty<int>().None(x => x > 0).ShouldBeTrue();
    }
}
