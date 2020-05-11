using System;
using System.IO.Ports;
// Serial port is in .NET Platform Extensions: https://docs.microsoft.com/en-us/dotnet/api/system.io.ports.serialport?view=dotnet-plat-ext-3.1
// That means it is available as a nuget package (named System.IO.Ports).
// Do not forget to install that!

// In case you need emulation: https://www.virtual-serial-port.org/

namespace SerialPortDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Available serial ports: ");
            var allPortNames = SerialPort.GetPortNames();
            foreach (var portname in allPortNames)
                Console.WriteLine(portname);

            var sendingPort = CreateSerialPort(allPortNames[0]);
            var receivingPort = CreateSerialPort(allPortNames[1]);

            sendingPort.Open();
            receivingPort.Open();

            Console.WriteLine($"Sending: Mizu?");
            sendingPort.WriteLine("Mizu?");
            var answer = receivingPort.ReadLine();
            Console.WriteLine($"Received: {answer}");

            sendingPort.Close();
            receivingPort.Close();
        }

        private static SerialPort CreateSerialPort(string name)
        {
            var port = new SerialPort();
            port.PortName = name;
            port.BaudRate = 115200;
            port.Parity = Parity.Even;
            port.DataBits = 8;
            port.StopBits = StopBits.One;
            port.Handshake = Handshake.None;
            port.ReadTimeout = 500;
            port.WriteTimeout = 500;
            return port;
        }
    }
}
