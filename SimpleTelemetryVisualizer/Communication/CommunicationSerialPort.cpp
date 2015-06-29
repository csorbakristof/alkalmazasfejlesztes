#include "CommunicationSerialPort.h"

CommunicationSerialPort::CommunicationSerialPort(
    QString portName,
    qint32 baudRate,
    QSerialPort::DataBits dataBits,
    QSerialPort::Parity parity,
    QSerialPort::StopBits stopBits)
    : Communication()
{
    serialPort.setPortName(portName);
    serialPort.setBaudRate(baudRate);
    serialPort.setDataBits(dataBits);
    serialPort.setParity(parity);
    serialPort.setStopBits(stopBits);

    serialPort.setFlowControl(QSerialPort::NoFlowControl);

    QObject::connect(&serialPort, SIGNAL(error(QSerialPort::SerialPortError)), this, SLOT(handleError(QSerialPort::SerialPortError)));
}

void CommunicationSerialPort::connect()
{
    if (!serialPort.open(QIODevice::ReadWrite))
    {
        emit errorOccurred("Cannot open serial port " + serialPort.portName());
        return;
    }

    // Connect receive stream
    if (receiveStream != nullptr)
    {
        delete receiveStream;
    }
    receiveStream = new QDataStream(&serialPort);
    QObject::connect(&serialPort, SIGNAL(readyRead()), this, SLOT(dataReceived()));

    qDebug() << "Serial port opened, receive data stream connected.";
}

bool CommunicationSerialPort::isConnected() const
{
    return serialPort.isOpen();
}

void CommunicationSerialPort::sendBufferContent()
{
    if (!isConnected())
    {
        emit errorOccurred(QString("ERROR: Tried to send data with serial port not open."));
        return;
    }

    qDebug() << "CommunicationTcpSocket::send() " << sendBuffer.size() << " bytes:\n" << sendBuffer.toHex();
    serialPort.write(sendBuffer);
    sendBuffer.clear();
}

void CommunicationSerialPort::handleError(QSerialPort::SerialPortError error)
{
    Q_UNUSED(error);
    // Emit signal with error string.
    emit this->errorOccurred(serialPort.errorString());
}
