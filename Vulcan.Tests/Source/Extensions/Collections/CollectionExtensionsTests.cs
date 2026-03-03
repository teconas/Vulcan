using Vulcan.Extensions;

namespace Vulcan.Tests.Extensions.Collections;

[TestSubject(typeof(Vulcan.Extensions.CollectionExtensions))]
public static class CollectionExtensionsTests
{
    public class AddRange
    {
        [Fact]
        public void Enumerable_AddsAll()
        {
            // Arrange
            ICollection<int> sut = new List<int> { 1 };

            // Act
            sut.AddRange([2, 3, 4]);

            // Assert
            sut.ShouldBe([1, 2, 3, 4]);
        }

        [Fact]
        public void Enumerable_ReturnsSelf()
        {
            // Arrange
            ICollection<int> sut = new List<int>();

            // Act
            var result = sut.AddRange([1, 2]);

            // Assert
            result.ShouldBeSameAs(sut);
        }

        [Fact]
        public void TwoItems_AddsInOrder()
        {
            // Arrange
            ICollection<int> sut = new List<int>();

            // Act
            sut.AddRange(1, 2);

            // Assert
            sut.ShouldBe([1, 2]);
        }

        [Fact]
        public void ThreeItems_AddsInOrder()
        {
            // Arrange
            ICollection<int> sut = new List<int>();

            // Act
            sut.AddRange(1, 2, 3);

            // Assert
            sut.ShouldBe([1, 2, 3]);
        }

        [Fact]
        public void FourItems_AddsInOrder()
        {
            // Arrange
            ICollection<int> sut = new List<int>();

            // Act
            sut.AddRange(1, 2, 3, 4);

            // Assert
            sut.ShouldBe([1, 2, 3, 4]);
        }

        [Fact]
        public void FiveItems_AddsInOrder()
        {
            // Arrange
            ICollection<int> sut = new List<int>();

            // Act
            sut.AddRange(1, 2, 3, 4, 5);

            // Assert
            sut.ShouldBe([1, 2, 3, 4, 5]);
        }

        [Fact]
        public void Items_ReturnsSelf()
        {
            // Arrange
            ICollection<int> sut = new List<int>();

            // Act
            var result = sut.AddRange(1, 2, 3);

            // Assert
            result.ShouldBeSameAs(sut);
        }
    }
}
