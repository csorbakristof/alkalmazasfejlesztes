using System;
using System.Collections.Generic;
using System.Text;

namespace ReturnValueVsException
{
    class Robot
    {
        public void Turn(int degrees)
        {
            if (Math.Abs(degrees) > 25)
                throw new ArgumentOutOfRangeException(
                    $"Robot cannot turn that much ({degrees} deg).");
            // Turning operation...
        }

        public enum AccelerationResultEnum {
            OK,
            OverCurrent,
            LowVoltage
        }

        public AccelerationResultEnum Accelerate(int acceleration)
        {
            // Acceleration...
            return AccelerationResultEnum.OK;
        }

        public void EmergencyStop()
        {
            // Full stop and power down...
        }
    }
}
