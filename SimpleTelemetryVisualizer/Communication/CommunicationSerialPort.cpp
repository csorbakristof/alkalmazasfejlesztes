#include <QDebug>
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

    QObject::connect(&serialPort, SIGNAL(error(QSerialPort::SerialPortError)),
                     this, SLOT(handleError(QSerialPort::SerialPortError)));
}

void CommunicationSerialPort::connect()
{
    if (!serialPort.open(QIODevice::ReadWrite))
    {
        emit errorOccurred("Cannot open serial port " + serialPort.portName());
        return;
    }

    // A foradási adatfolyam csatlakoztatása
    // (Ha volt korábbi, az most megszűnik a unique_ptr miatt.)
    receiveStream = std::make_unique<QDataStream>(&serialPort);
    QObject::connect(&serialPort, SIGNAL(readyRead()), this, SLOT(dataReceived()));

    qDebug() << "Soros port nyitva, adatfogadás készen áll.";
}

bool CommunicationSerialPort::isConnected() const
{
    return serialPort.isOpen();
}

void CommunicationSerialPort::sendBufferContent()
{
    if (!isConnected())
    {
        emit errorOccurred(QString("HIBA: Küldésnél nincs nyitva a soros port."));
        return;
    }

    qDebug() << "CommunicationTcpSocket::send() " << sendBuffer.size() << " bájt:\n" << sendBuffer.toHex();
    serialPort.write(sendBuffer);
    sendBuffer.clear();
}

void CommunicationSerialPort::handleError(QSerialPort::SerialPortError error)
{
    Q_UNUSED(error);
    emit this->errorOccurred(serialPort.errorString());
}
