using Vulcan.Extensions;

namespace Vulcan.Tests.Extensions.Collections;

[TestSubject(typeof(DictionaryExtensions))]
public static class DictionaryExtensionsTests
{
    public class GetOrDefault
    {
        [Fact]
        public void Present_ReturnsStoredValue()
        {
            IDictionary<string, int> sut = new Dictionary<string, int> { ["a"] = 1 };
            sut.GetOrDefault("a").ShouldBe(1);
        }

        [Fact]
        public void Absent_ReferenceValue_ReturnsNull()
        {
            IDictionary<string, string?> sut = new Dictionary<string, string?> { ["a"] = "x" };
            sut.GetOrDefault("missing").ShouldBeNull();
        }

        [Fact]
        public void Absent_ValueType_ReturnsZeroNotNull()
        {
            IDictionary<string, int> sut = new Dictionary<string, int> { ["a"] = 1 };
            sut.GetOrDefault("missing").ShouldBe(0);
        }

        [Fact]
        public void Miss_DoesNotMutate()
        {
            // Arrange
            IDictionary<string, int> sut = new Dictionary<string, int> { ["a"] = 1 };

            // Act
            sut.GetOrDefault("missing");

            // Assert
            sut.Count.ShouldBe(1);
            sut.ContainsKey("missing").ShouldBeFalse();
        }

        [Fact]
        public void KeyMappedToNull_ReturnsNull_WhileKeyPresent()
        {
            // Arrange
            IDictionary<string, string?> sut = new Dictionary<string, string?> { ["a"] = null };

            // Act
            var result = sut.GetOrDefault("a");

            // Assert — present-null (ContainsKey true) is indistinguishable by value from absent-null
            result.ShouldBeNull();
            sut.ContainsKey("a").ShouldBeTrue();
        }
    }

    public class GetOrInsertKeyFactory
    {
        [Fact]
        public void Present_ReturnsExisting_FactoryNotInvoked()
        {
            // Arrange
            IDictionary<string, int> sut = new Dictionary<string, int> { ["a"] = 1 };
            var called = new LinearBool();

            // Act
            var result = sut.GetOrInsert("a", key =>
            {
                called.Set();
                return 99;
            });

            // Assert
            result.ShouldBe(1);
            called.ShouldBeFalse();
            sut.Count.ShouldBe(1);
        }

        [Fact]
        public void Absent_InvokesFactoryOnce_InsertsAndReturns()
        {
            // Arrange
            IDictionary<string, int> sut = new Dictionary<string, int> { ["a"] = 1 };
            var callCount = 0;

            // Act
            var result = sut.GetOrInsert("b", _ =>
            {
                callCount++;
                return 42;
            });

            // Assert
            callCount.ShouldBe(1);
            result.ShouldBe(42);
            sut.ContainsKey("b").ShouldBeTrue();
            sut["b"].ShouldBe(42);
            sut.Count.ShouldBe(2);
        }

        [Fact]
        public void Absent_FactoryReceivesLookupKey()
        {
            // Arrange
            IDictionary<string, int> sut = new Dictionary<string, int>();
            string? received = null;

            // Act
            sut.GetOrInsert("theKey", key =>
            {
                received = key;
                return 7;
            });

            // Assert
            received.ShouldBe("theKey");
        }
    }

    public class GetOrInsertParameterlessFactory
    {
        [Fact]
        public void Present_ReturnsExisting_FactoryNotInvoked()
        {
            // Arrange
            IDictionary<string, int> sut = new Dictionary<string, int> { ["a"] = 1 };
            var called = new LinearBool();

            // Act
            var result = sut.GetOrInsert("a", () =>
            {
                called.Set();
                return 99;
            });

            // Assert
            result.ShouldBe(1);
            called.ShouldBeFalse();
            sut.Count.ShouldBe(1);
        }

        [Fact]
        public void Absent_InvokesFactoryOnce_InsertsAndReturns()
        {
            // Arrange
            IDictionary<string, int> sut = new Dictionary<string, int> { ["a"] = 1 };
            var callCount = 0;

            // Act
            var result = sut.GetOrInsert("b", () =>
            {
                callCount++;
                return 42;
            });

            // Assert
            callCount.ShouldBe(1);
            result.ShouldBe(42);
            sut.ContainsKey("b").ShouldBeTrue();
            sut["b"].ShouldBe(42);
            sut.Count.ShouldBe(2);
        }
    }
}
