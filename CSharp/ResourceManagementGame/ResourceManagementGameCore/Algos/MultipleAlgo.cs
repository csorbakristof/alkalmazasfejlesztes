using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Algos
{
    public class MultipleAlgo : IAlgo
    {
        private int _starterValue;
        private double _multiValue;
        private float _currentValue;
        public MultipleAlgo(int start, double multi)
        {
            _starterValue = start;
            _multiValue = multi;
            _currentValue = Convert.ToSingle(start / multi);
        }
        public int GetNext()
        {
            _currentValue = Convert.ToSingle(_currentValue * _multiValue);
            return Convert.ToInt32(Math.Ceiling(_currentValue));
        }
    }
}
