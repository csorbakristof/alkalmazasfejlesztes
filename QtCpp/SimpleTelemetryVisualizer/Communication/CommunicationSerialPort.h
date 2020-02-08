#pragma once
#ifndef COMMUNICATIONSERIALPORT_H
#define COMMUNICATIONSERIALPORT_H
#include "Communication/Communication.h"
#include <QtSerialPort/QSerialPort>

// http://doc.qt.io/qt-5/qtserialport-index.html
// http://doc.qt.io/qt-5/qserialport.html
// http://doc.qt.io/qt-5/qiodevice.html#readyRead
// http://doc.qt.io/qt-5/qtserialport-examples.html

/**
 * @brief Soros port kommunikáció
 */
class CommunicationSerialPort : public Communication
{
    Q_OBJECT
public:
    CommunicationSerialPort(QString portName,
            qint32 baudRate,
            QSerialPort::DataBits dataBits,
            QSerialPort::Parity parity,
            QSerialPort::StopBits stopBits);
    virtual ~CommunicationSerialPort() = default;

    virtual void connect();
    virtual bool isConnected() const override;

protected:
    virtual void sendBufferContent() override;

private:
    QSerialPort serialPort;

private slots:
    // Forwards signal to errorOccurred.
    void handleError(QSerialPort::SerialPortError error);

};

#endif // COMMUNICATIONSERIALPORT_H
