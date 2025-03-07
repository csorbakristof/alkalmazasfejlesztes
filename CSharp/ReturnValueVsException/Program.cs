using System;

namespace ReturnValueVsException
{
    class Program
    {
        public static void Main()
        {
            Robot r = new Robot();
            var status = r.Accelerate(10);
            if (status != Robot.AccelerationResultEnum.OK)
            {
                // Handle and log error...
            }

            try
            {
                r.Turn(27);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // Log error...
                r.EmergencyStop();
            }
            finally
            {
                // ...
            }
        }
    }
}
