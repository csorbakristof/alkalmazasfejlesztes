#pragma once
#ifndef TCPSOCKETCOMMUNICATION_H
#define TCPSOCKETCOMMUNICATION_H
#include <QTcpSocket>
#include "Communication.h"

/** TCP socket based functions extending Communication.
 * Use derived classes ...Client and ...Server to use it.
 * Note: does not own a QTcpSocket. Use setSocket to set it. */
class CommunicationTcpSocket : public Communication
{
    Q_OBJECT

public:
    CommunicationTcpSocket();
    ~CommunicationTcpSocket();

    // Use nullptr to disconnect signals from the socket.
    void setSocket(QTcpSocket *newSocket);

    virtual bool isConnected() const override;

    void disconnect();

protected:
    virtual void sendBufferContent() override;

private:
    /** Pointer to the underlying socket. May be null if there is currently none. */
    QTcpSocket *socket;

private slots:
    // Forwards signal to errorOccurred.
    void handleError(QAbstractSocket::SocketError socketError);

};

#endif // TCPSOCKETCOMMUNICATION_H
