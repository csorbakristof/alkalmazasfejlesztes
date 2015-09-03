#include "Application.h"
#include "CommunicationSerialPort.h"

int main(int argc, char *argv[])
{
    CommunicationSerialPort serialPort(QString("COM5"),9600,QSerialPort::Data8, QSerialPort::EvenParity, QSerialPort::OneStop);
    Application app(argc, argv, serialPort);

    serialPort.connect();

    app.startSending();

    return app.exec();
}
