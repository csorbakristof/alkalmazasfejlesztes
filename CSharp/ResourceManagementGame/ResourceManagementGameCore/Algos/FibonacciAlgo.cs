using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Algos
{

    public class FibonacciAlgo : IAlgo
    {
        private int _first;
        private int _second;
        private int _third;
        public FibonacciAlgo()
        {
            _first = 1;
            _second = 1;
            first = true;
            second = true;
        }
        private bool first = true;
        private bool second = true;
        public int GetNext()
        {
            if (first)
            {
                first = false;
                return _first;
            }
            else if (second)
            {
                second = false;
                return _second;
            }
            else
            {
                _third = _first + _second;
                _first = _second;
                _second = _third;
                return _third;
            }
        }
    }

}
