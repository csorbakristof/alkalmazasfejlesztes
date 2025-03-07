using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureMonitoring.Model
{
    public class TempHum
    {
        public required string DeviceName { get; set; }
        public required long Timestamp { get; set; }
        public DateTime Time { get; set; }
        public float? Temperature { get; set; }
        public float? Humidity { get; set; }
    }
}
