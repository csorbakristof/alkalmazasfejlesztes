using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Algos
{
    public class ExpAlgo : IAlgo
    {
        private int _baseValue;
        private double _currentValue;
        public ExpAlgo(int baseValue)
        {
            _baseValue = baseValue;
            _currentValue = _baseValue / _baseValue;
        }
        public int GetNext()
        {
            _currentValue *= _baseValue;
            return Convert.ToInt32(Math.Ceiling(_currentValue));
        }
    }
}
