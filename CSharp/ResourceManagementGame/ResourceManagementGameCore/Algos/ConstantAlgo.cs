using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Algos
{
    public class ConstantAlgo : IAlgo
    {
        private int _value;
        public ConstantAlgo(int value)
        {
            _value = value;
        }

        public int GetNext()
        {
            return _value;
        }
    }
}
