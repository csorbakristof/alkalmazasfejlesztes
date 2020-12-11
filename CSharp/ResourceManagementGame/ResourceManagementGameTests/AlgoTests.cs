using ResourceManagementGameCore.Algos;
using Xunit;

namespace ResourceManagementGameTests
{
    [Collection("GameTests")]
    public class AlgoTests
    {
        [Fact]
        public void ConstantAlgoTest()
        {
            ConstantAlgo algo = new ConstantAlgo(10);
            Assert.Equal(10, algo.GetNext());
            Assert.Equal(10, algo.GetNext());
        }

        [Fact]
        public void ConstantAlgoTest2()
        {
            ConstantAlgo algo = new ConstantAlgo(1);
            Assert.Equal(1, algo.GetNext());
            Assert.Equal(1, algo.GetNext());
        }
        [Fact]
        public void IncrementAlgoTest()
        {
            IncrementAlgo algo = new IncrementAlgo(1, 1);
            Assert.Equal(1, algo.GetNext());
            Assert.Equal(2, algo.GetNext());
            Assert.Equal(3, algo.GetNext());
            Assert.Equal(4, algo.GetNext());
            Assert.Equal(5, algo.GetNext());
        }
        [Fact]
        public void IncrementAlgoTest2()
        {
            IncrementAlgo algo = new IncrementAlgo(42, 3);
            Assert.Equal(42, algo.GetNext());
            Assert.Equal(45, algo.GetNext());
            Assert.Equal(48, algo.GetNext());
            Assert.Equal(51, algo.GetNext());
            Assert.Equal(54, algo.GetNext());
        }
        [Fact]
        public void MultipleAlgoTest()
        {
            MultipleAlgo algo = new MultipleAlgo(1, 2);
            Assert.Equal(1, algo.GetNext());
            Assert.Equal(2, algo.GetNext());
            Assert.Equal(4, algo.GetNext());
            Assert.Equal(8, algo.GetNext());
            Assert.Equal(16, algo.GetNext());
        }
        [Fact]
        public void MultipleAlgoTest2()
        {
            MultipleAlgo algo = new MultipleAlgo(4, 1.3);
            Assert.Equal(4, algo.GetNext());
            Assert.Equal(6, algo.GetNext()); //5,2
            Assert.Equal(7, algo.GetNext()); //6,76
            Assert.Equal(9, algo.GetNext()); //8,788
            Assert.Equal(12, algo.GetNext()); //11,4244
        }
        [Fact]
        public void ExpAlgoTest()
        {
            ExpAlgo algo = new ExpAlgo(2);
            Assert.Equal(2, algo.GetNext());
            Assert.Equal(4, algo.GetNext());
            Assert.Equal(8, algo.GetNext());
            Assert.Equal(16, algo.GetNext());
            Assert.Equal(32, algo.GetNext());
        }
        [Fact]
        public void ExpAlgoTest2()
        {
            ExpAlgo algo = new ExpAlgo(3);
            Assert.Equal(3, algo.GetNext());
            Assert.Equal(9, algo.GetNext());
            Assert.Equal(27, algo.GetNext());
            Assert.Equal(81, algo.GetNext());
            Assert.Equal(243, algo.GetNext());
        }
        [Fact]
        public void FibonacciTest()
        {
            FibonacciAlgo algo = new FibonacciAlgo();
            Assert.Equal(1, algo.GetNext());
            Assert.Equal(1, algo.GetNext());
            Assert.Equal(2, algo.GetNext());
            Assert.Equal(3, algo.GetNext());
            Assert.Equal(5, algo.GetNext());
            Assert.Equal(8, algo.GetNext());
            Assert.Equal(13, algo.GetNext());
            Assert.Equal(21, algo.GetNext());
        }
    }
}
