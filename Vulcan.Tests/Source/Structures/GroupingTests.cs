using JetBrains.Annotations;
using Vulcan.Extensions;
using Vulcan.Structures;

namespace Vulcan.Tests.Structures;

[TestSubject(typeof(Grouping<,>))]
public class GroupingTests
{
    public class Adding
    {
        readonly Grouping<int, int> _sut = [];

        [Fact]
        public void AddingSingleElement()
        {
            // Arrange
            // Act
            _sut.Add(0, 1);

            // Assert
            var result = _sut.Get(0);
            result.Count.ShouldBe(1);
            result.First().ShouldBe(1);
        }

        [Fact]
        public void AddingMultipleElements()
        {
            // Arrange
            // Act
            _sut.Add(0, 1);
            _sut.Add(0, 2);

            // Assert
            var result = _sut.Get(0);
            result.Count.ShouldBe(2);
            result.ShouldBe([1, 2]);
        }

        [Fact]
        public void AddingViaCollection()
        {
            // Arrange
            // Act
            _sut.Get(0).Add(1);

            // Assert
            var result = _sut.Get(0);
            result.Count.ShouldBe(1);
            result.ShouldBe([1]);
        }
    }

    public class Removing
    {
        readonly Grouping<int, int> _sut = [];

        [Fact]
        public void RemovingExisting()
        {
            // Arrange
            _sut.Add(0,1);
            
            // Act
            var result = _sut.Remove(0, 1);
            
            // Assert
            result.ShouldBeTrue();
            _sut.Get(0).ShouldBeEmpty();
        }

        [Fact]
        public void RemovingNonExisting()
        {
            // Arrange
            _sut.Add(0,1);
            
            // Act
            var result = _sut.Remove(0, 2);
            
            // Assert
            result.ShouldBeFalse();
            _sut.Get(0).ShouldBe([1]);
        }

        [Fact]
        public void RemoveDuplicate()
        {
            // Arrange
            _sut.Add(0, 1);
            _sut.Add(1, 1);
            
            // Act
            _sut.Remove(0, 1);
            
            // Assert
            _sut.SelectMany().Count().ShouldBe(1);
        }
        
        [Fact]
        public void RemoveFromCollection()
        {
            // Arrange
            _sut.Add(0, 1);
            _sut.Add(0, 2);
            
            // Act
            _sut.Get(0).Remove(1);
            
            // Assert
            _sut.Get(0).ShouldBe([2]);
        }
    }

    public class Grouping
    {
        readonly Grouping<int, int> _sut = [];
        
        [Fact]
        public void SimpleTest()
        {
            // Arrange
            // Act
            _sut.Add(1, 1);
            _sut[2].Add(1);
            _sut.Add(2,2);
            _sut.Get(3);
            
            // Assert
            _sut.Get(0).ShouldBeEmpty();
            _sut.Get(1).ShouldBe([1]);
            _sut.Get(2).ShouldBe([1,2]);
            _sut.Get(3).ShouldBeEmpty();
        }

        [Fact]
        public void Iteration()
        {
            // Arrange
            _sut.Add(1, 1);
            _sut[2].Add(1);
            _sut.Add(2,2);
            _sut.Get(3);
            
            // Act
            var result = _sut.ToList();
            
            // Assert
            result
                .FirstOrDefault(x => x.Key is 1)
                .ShouldNotBeNull()
                .ShouldBe([1]);
            result
                .FirstOrDefault(x => x.Key is 2)
                .ShouldNotBeNull()
                .ShouldBe([1,2]);
        }
        
        [Fact]
        public void EmptyGroupsAreIgnored()
        {
            // Arrange
            _sut.Add(1, 1);
            _sut[2].Add(1);
            _sut.Add(2,2);
            _sut.Get(3);
            
            // Act
            var result = _sut.ToList();
            
            // Assert
            result.FirstOrDefault(x => x.Key is 0).ShouldBeNull();
            result.FirstOrDefault(x => x.Key is 3).ShouldBeNull();
        }
    }

    public class CustomCollection
    {
        [Fact]
        public void HashSet()
        {
            // Arrange
            var sut = new Grouping<int, int>(()=>new HashSet<int>());
            
            // Act
            var result = sut.Get(0);
            
            // Assert
            result.ShouldBeOfType<HashSet<int>>();
        }
        
        [Fact]
        public void List()
        {
            // Arrange
            var sut = new Grouping<int, int>(()=>new List<int>());
            
            // Act
            var result = sut.Get(0);
            
            // Assert
            result.ShouldBeOfType<List<int>>();
        }
        
        [Fact]
        public void Dictionary()
        {
            // Arrange
            var sut = new Grouping<int, KeyValuePair<int,int>>(()=>new Dictionary<int,int>());
            
            // Act
            var result = sut.Get(0);
            
            // Assert
            result.ShouldBeOfType<Dictionary<int,int>>();
        }
        
    }
    
}