using ResourceManagementGameCore.Algos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Factories
{
    public static class AlgoFactory
    {
        private static Dictionary<AlgoType, IAlgoFactory> Factories { get; set; } = new Dictionary<AlgoType, IAlgoFactory>();
        public static void AddFactory(AlgoType algoType)
        {
            if (Factories.ContainsKey(algoType))
                return;
            switch (algoType)
            {
                case AlgoType.Constant:
                    Factories.Add(AlgoType.Constant, new ConstantAlgoFactory());
                    break;
                case AlgoType.Increment:
                    Factories.Add(AlgoType.Increment, new IncrementAlgoFactory());
                    break;
                case AlgoType.Multiple:
                    Factories.Add(AlgoType.Multiple, new MultipleAlgoFactory());
                    break;
                case AlgoType.Exp:
                    Factories.Add(AlgoType.Exp, new ExpAlgoFactory());
                    break;
                case AlgoType.Fibonacci:
                    Factories.Add(AlgoType.Fibonacci, new FibonacciAlgoFactory());
                    break;
                case 0:
                    throw new Exception("Invalid algo type");
            }
        }
        public static IAlgo CreateAlgo(AlgoType algoType, int? start = null, double? value = null)
        {
            IAlgoFactory factory = null;
            if (!Factories.ContainsKey(algoType))
                throw new Exception("Invalid algo type");
            switch (algoType)
            {
                case AlgoType.Constant:
                    Factories.TryGetValue(AlgoType.Constant, out factory);
                    break;
                case AlgoType.Increment:
                    Factories.TryGetValue(AlgoType.Increment, out factory);
                    break;
                case AlgoType.Multiple:
                    Factories.TryGetValue(AlgoType.Multiple, out factory);
                    break;
                case AlgoType.Exp:
                    Factories.TryGetValue(AlgoType.Exp, out factory);
                    break;
                case AlgoType.Fibonacci:
                    Factories.TryGetValue(AlgoType.Fibonacci, out factory);
                    break;

            }
            try
            {
                IAlgo algo = null;
                if (value.HasValue)
                {
                    algo = factory.CreateAlgo(start.Value, value.Value);
                }
                else if (start.HasValue)
                {
                    algo = factory.CreateAlgo(start.Value);
                }
                else
                    algo = factory.CreateAlgo();

                return algo;
            }
            catch
            {
                throw new Exception($"Invalid parameters for algo type: {algoType.ToString()}");
            }
        }
        public static void CleanFactories()
        {
            Factories = new Dictionary<AlgoType, IAlgoFactory>();
        }
    }
    public interface IAlgoFactory
    {
        IAlgo CreateAlgo();
        IAlgo CreateAlgo(int start);
        IAlgo CreateAlgo(int start, double value);
    }
    public class ConstantAlgoFactory : IAlgoFactory
    {
        public IAlgo CreateAlgo()
        {
            throw new NotSupportedException();
        }

        public IAlgo CreateAlgo(int start)
        {
            return new ConstantAlgo(start);
        }

        public IAlgo CreateAlgo(int start, double value)
        {
            throw new NotSupportedException();
        }
    }
    public class MultipleAlgoFactory : IAlgoFactory
    {
        public IAlgo CreateAlgo()
        {
            throw new NotSupportedException();
        }

        public IAlgo CreateAlgo(int start)
        {
            throw new NotSupportedException();
        }

        public IAlgo CreateAlgo(int start, double value)
        {
            return new MultipleAlgo(start, value);
        }
    }
    public class IncrementAlgoFactory : IAlgoFactory
    {
        public IAlgo CreateAlgo()
        {
            throw new NotSupportedException();
        }

        public IAlgo CreateAlgo(int start)
        {
            throw new NotSupportedException();
        }

        public IAlgo CreateAlgo(int start, double value)
        {
            return new IncrementAlgo(start, value);
        }
    }
    public class ExpAlgoFactory : IAlgoFactory
    {
        public IAlgo CreateAlgo()
        {
            throw new NotSupportedException();
        }

        public IAlgo CreateAlgo(int start)
        {
            return new ExpAlgo(start);
        }

        public IAlgo CreateAlgo(int start, double value)
        {
            throw new NotSupportedException();
        }
    }
    public class FibonacciAlgoFactory : IAlgoFactory
    {
        public IAlgo CreateAlgo()
        {
            return new FibonacciAlgo();
        }

        public IAlgo CreateAlgo(int start)
        {
            throw new NotSupportedException();
        }

        public IAlgo CreateAlgo(int start, double value)
        {
            throw new NotSupportedException();
        }
    }
}
