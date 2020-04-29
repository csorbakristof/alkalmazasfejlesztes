using System;
using Xunit;

namespace TestingWithMoq
{
    public class StuffTests
    {
        [Fact]
        public void StuffBaseInstantiates()
        {
            var sb = new StuffBase(null,null);
        }

        [Fact]
        public void StuffBaseUsesSource()
        {
            var srcMock = new Moq.Mock<ISource>();
            srcMock.Setup(i => i.GetValue()).Returns(3);
            var sb = new StuffBase(srcMock.Object, null);
            Assert.Equal(3, sb.GetValueFromInternalSource());
        }

        [Fact]
        public void StuffCallsCommand()
        {
            var mockCmd = new Moq.Mock<ICommand>();
            var mockSrc = new Moq.Mock<ISource>();
            mockSrc.Setup(i => i.GetValue()).Returns(1);
            var sb = new StuffBase(mockSrc.Object, mockCmd.Object);
            sb.ExecuteCommandIfSourceIsNonzero();
            mockCmd.Verify(i => i.Execute());
        }

        [Fact]
        public void StuffCallsVisitor()
        {
            var mockVisitor = new Moq.Mock<IVisitor>();
            var sb = new StuffBase(null,null);
            sb.Accept(mockVisitor.Object);
            mockVisitor.Verify(i => i.Visit(sb));
        }
    }
}
