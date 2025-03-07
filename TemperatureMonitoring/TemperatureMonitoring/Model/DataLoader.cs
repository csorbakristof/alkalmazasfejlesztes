using System.Globalization;

namespace TemperatureMonitoring.Model
{
    public class DataLoader
    {
        public IEnumerable<TempHum> LoadCsv(string filename)
        {
            // Open CSV file and read all lines
            var lines = File.ReadAllLines(filename);
            string deviceName = lines[0];
            // lines[1] is header
            for (int i = 2; i < lines.Length; i++)
            {
                var values = lines[i].Split(';');
                long timestamp = long.Parse(values[0]);
                var measurement = new TempHum
                {
                    DeviceName = deviceName,
                    Timestamp = timestamp,
                    Time = GetDateTimeFromCsvTimestamp(timestamp),
                    Temperature = float.Parse(values[1], CultureInfo.InvariantCulture),
                    Humidity = float.Parse(values[2], CultureInfo.InvariantCulture),
                };
                yield return measurement;
            }
        }

        private DateTime GetDateTimeFromCsvTimestamp(long csvSecondsTimestamp)
        {
            long timestamp = csvSecondsTimestamp * TimeSpan.TicksPerSecond;
            long unixEpochTicks = 621355968000000000;
            var dateTime = new DateTime(unixEpochTicks + timestamp, DateTimeKind.Utc);
            return dateTime;
        }


    }
}
