using System.Reflection;
using Vulcan.Extensions;

namespace Vulcan.Tests.Extensions.Types;

[TestSubject(typeof(ExceptionExtensions))]
public static class ExceptionExtensionsTests
{
    public class FullMessage
    {
        [Fact]
        public void SingleException()
            => new Exception("Something went wrong.")
                .FullMessage()
                .ShouldBe("Something went wrong.");

        [Fact]
        public void ChainStripsIntermediatePunctuation()
            => new Exception("Outer.", new Exception("Inner."))
                .FullMessage()
                .ShouldBe("Outer: Inner.");

        [Fact]
        public void ThreeLevels()
            => new Exception("Outer.", new Exception("Middle!", new Exception("Inner.")))
                .FullMessage()
                .ShouldBe("Outer: Middle: Inner.");

        [Fact]
        public void NoPunctuationUnchanged()
            => new Exception("Outer", new Exception("Inner"))
                .FullMessage()
                .ShouldBe("Outer: Inner");

        [Theory]
        [InlineData("Message.")]
        [InlineData("Message!")]
        [InlineData("Message?")]
        [InlineData("Message;")]
        [InlineData("Message:")]
        public void AllPunctuationTypesStripped(string outerMessage)
            => new Exception(outerMessage, new Exception("Inner"))
                .FullMessage()
                .ShouldBe("Message: Inner");

        [Fact]
        public void AggregateWithSingleInnerIsUnwrapped()
            => new AggregateException(new Exception("Real error."))
                .FullMessage()
                .ShouldBe("Real error.");

        [Fact]
        public void AggregateWithMultipleInnersKeepsOwnMessage()
            => new AggregateException("Multiple failures.", new Exception("A"), new Exception("B"))
                .FullMessage()
                .ShouldBe("Multiple failures. (A) (B)");

        [Fact]
        public void TargetInvocationExceptionIsUnwrapped()
            => new TargetInvocationException(new Exception("Real error."))
                .FullMessage()
                .ShouldBe("Real error.");

        [Fact]
        public void NestedWrappersAreAllUnwrapped()
            => new AggregateException(new TargetInvocationException(new Exception("Deep error.")))
                .FullMessage()
                .ShouldBe("Deep error.");

        [Fact]
        public void WrapperChainedWithRealException()
            => new Exception("Outer.", new AggregateException(new Exception("Inner.")))
                .FullMessage()
                .ShouldBe("Outer: Inner.");
    }
}
