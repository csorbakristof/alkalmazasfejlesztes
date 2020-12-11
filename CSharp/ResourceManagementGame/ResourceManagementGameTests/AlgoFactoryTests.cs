using ResourceManagementGameCore.Algos;
using ResourceManagementGameCore.Factories;
using System;
using Xunit;

namespace ResourceManagementGameTests
{
    [Collection("GameTests")]
    public class AlgoFactoryTests
    {
        public AlgoFactoryTests()
        {
            AlgoFactory.CleanFactories();
        }
        [Fact]
        public void CreateConstantAlgoTest()
        {
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Constant, 1));
            AlgoFactory.AddFactory(AlgoType.Constant);
            var algo = AlgoFactory.CreateAlgo(AlgoType.Constant, 1);
            Assert.True(algo is ConstantAlgo);
            Assert.Equal(1, algo.GetNext());
            Assert.Equal(1, algo.GetNext());
        }
        [Fact]
        public void CreateMoreConstantAlgoTest()
        {
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Constant, 1));
            AlgoFactory.AddFactory(AlgoType.Constant);
            var algo = AlgoFactory.CreateAlgo(AlgoType.Constant, 1);
            var algo2 = AlgoFactory.CreateAlgo(AlgoType.Constant, 2);
            Assert.True(algo is ConstantAlgo);
            Assert.True(algo2 is ConstantAlgo);
            Assert.Equal(1, algo.GetNext());
            Assert.Equal(1, algo.GetNext());
            Assert.Equal(2, algo2.GetNext());
            Assert.Equal(2, algo2.GetNext());
        }
        [Fact]
        public void CreateIncrementAlgoTest()
        {
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Increment, 1, 5));
            AlgoFactory.AddFactory(AlgoType.Increment);
            var algo = AlgoFactory.CreateAlgo(AlgoType.Increment, 1, 5);
            Assert.True(algo is IncrementAlgo);
            Assert.Equal(1, algo.GetNext());
            Assert.Equal(6, algo.GetNext());
            Assert.Equal(11, algo.GetNext());
        }
        [Fact]
        public void CreateMoreIncrementAlgoTest()
        {
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Increment, 1, 3));
            AlgoFactory.AddFactory(AlgoType.Increment);
            var algo = AlgoFactory.CreateAlgo(AlgoType.Increment, 1, 3);
            var algo2 = AlgoFactory.CreateAlgo(AlgoType.Increment, 2, 4);
            Assert.True(algo is IncrementAlgo);
            Assert.True(algo2 is IncrementAlgo);
            Assert.Equal(1, algo.GetNext());
            Assert.Equal(4, algo.GetNext());
            Assert.Equal(7, algo.GetNext());
            Assert.Equal(10, algo.GetNext());
            Assert.Equal(2, algo2.GetNext());
            Assert.Equal(6, algo2.GetNext());
            Assert.Equal(10, algo2.GetNext());
        }
        [Fact]
        public void CreateMultipleAlgoTest()
        {
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Multiple, 1, 2));
            AlgoFactory.AddFactory(AlgoType.Multiple);
            var algo = AlgoFactory.CreateAlgo(AlgoType.Multiple, 1, 2);
            Assert.True(algo is MultipleAlgo);
            Assert.Equal(1, algo.GetNext());
            Assert.Equal(2, algo.GetNext());
            Assert.Equal(4, algo.GetNext());
        }
        [Fact]
        public void CreateMoreMultipleAlgoTest()
        {
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Multiple, 1, 2));
            AlgoFactory.AddFactory(AlgoType.Multiple);
            var algo = AlgoFactory.CreateAlgo(AlgoType.Multiple, 1, 2);
            var algo2 = AlgoFactory.CreateAlgo(AlgoType.Multiple, 2, 1.5);
            Assert.True(algo is MultipleAlgo);
            Assert.True(algo2 is MultipleAlgo);
            Assert.Equal(1, algo.GetNext());
            Assert.Equal(2, algo.GetNext());
            Assert.Equal(4, algo.GetNext());
            Assert.Equal(2, algo2.GetNext());
            Assert.Equal(3, algo2.GetNext());
            Assert.Equal(5, algo2.GetNext()); //4,5
            Assert.Equal(7, algo2.GetNext()); //6,75
        }
        [Fact]
        public void CreateExpAlgoTest()
        {
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Exp, 3));
            AlgoFactory.AddFactory(AlgoType.Exp);
            var algo = AlgoFactory.CreateAlgo(AlgoType.Exp, 3);
            Assert.True(algo is ExpAlgo);
            Assert.Equal(3, algo.GetNext());
            Assert.Equal(9, algo.GetNext());
            Assert.Equal(27, algo.GetNext());
        }
        [Fact]
        public void CreateMoreExpAlgoTest()
        {
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Exp, 3));
            AlgoFactory.AddFactory(AlgoType.Exp);
            var algo = AlgoFactory.CreateAlgo(AlgoType.Exp, 3);
            var algo2 = AlgoFactory.CreateAlgo(AlgoType.Exp, 4);
            Assert.True(algo is ExpAlgo);
            Assert.True(algo2 is ExpAlgo);
            Assert.Equal(3, algo.GetNext());
            Assert.Equal(9, algo.GetNext());
            Assert.Equal(27, algo.GetNext());
            Assert.Equal(4, algo2.GetNext());
            Assert.Equal(16, algo2.GetNext());
            Assert.Equal(64, algo2.GetNext());
        }
        [Fact]
        public void CreateFibonacciAlgoTest()
        {
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Fibonacci));
            AlgoFactory.AddFactory(AlgoType.Fibonacci);
            var algo = AlgoFactory.CreateAlgo(AlgoType.Fibonacci);
            Assert.True(algo is FibonacciAlgo);
            Assert.Equal(1, algo.GetNext());
            Assert.Equal(1, algo.GetNext());
            Assert.Equal(2, algo.GetNext());
            Assert.Equal(3, algo.GetNext());
        }
        [Fact]
        public void CreateMoreFibonacciAlgoTest()
        {
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Fibonacci));
            AlgoFactory.AddFactory(AlgoType.Fibonacci);
            var algo = AlgoFactory.CreateAlgo(AlgoType.Fibonacci);
            var algo2 = AlgoFactory.CreateAlgo(AlgoType.Fibonacci);
            Assert.True(algo is FibonacciAlgo);
            Assert.True(algo2 is FibonacciAlgo);
            Assert.Equal(1, algo.GetNext());
            Assert.Equal(1, algo.GetNext());
            Assert.Equal(2, algo.GetNext());
            Assert.Equal(1, algo2.GetNext());
            Assert.Equal(1, algo2.GetNext());
            Assert.Equal(3, algo.GetNext());
            Assert.Equal(2, algo2.GetNext());
            Assert.Equal(5, algo.GetNext());
        }
        [Fact]
        public void CreateFailTest()
        {
            AlgoFactory.AddFactory(AlgoType.Constant);
            AlgoFactory.AddFactory(AlgoType.Increment);
            AlgoFactory.AddFactory(AlgoType.Multiple);
            AlgoFactory.AddFactory(AlgoType.Exp);
            AlgoFactory.AddFactory(AlgoType.Fibonacci);

            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Constant, 3, 1));
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Constant));
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Increment, 1));
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Increment));
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Multiple, 8));
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Multiple));
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Exp, 1, 7));
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Exp));
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Fibonacci, 10, 1));
            Assert.Throws<Exception>(() => AlgoFactory.CreateAlgo(AlgoType.Fibonacci, 7));
        }
    }
}
