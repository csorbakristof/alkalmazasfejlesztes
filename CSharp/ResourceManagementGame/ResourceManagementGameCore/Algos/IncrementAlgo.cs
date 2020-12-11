using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Algos
{
    public class IncrementAlgo : IAlgo
    {
        private double _incrementValue;
        private int _starterValue;
        private double _currentValue;
        public IncrementAlgo(int start, double increment)
        {
            _starterValue = start;
            _incrementValue = increment;
            _currentValue = start - _incrementValue;
        }
        public int GetNext()
        {
            _currentValue += _incrementValue;
            return Convert.ToInt32(Math.Ceiling(_currentValue));
        }
    }
}
