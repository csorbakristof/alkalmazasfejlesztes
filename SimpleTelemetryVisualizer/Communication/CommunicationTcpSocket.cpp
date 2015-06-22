#include <QTimer>
#include "CommunicationTcpSocket.h"

CommunicationTcpSocket::CommunicationTcpSocket()
    : Communication(), socket(nullptr)
{
    // Connect base classes slots to the socket.
}

CommunicationTcpSocket::~CommunicationTcpSocket()
{

}

void CommunicationTcpSocket::setSocket(QTcpSocket *newSocket)
{
    // Connect slots to the new socket.
    if (socket != nullptr && newSocket != socket)
    {
        QObject::disconnect(socket, SIGNAL(error(QAbstractSocket::SocketError)), this, SLOT(CommunicationTcpSocket::handleError(QAbstractSocket::SocketError)));
        QObject::disconnect(socket, SIGNAL(readyRead()), this, SLOT(dataReceived()));
    }
    socket = newSocket;
    QObject::connect(socket, SIGNAL(error(QAbstractSocket::SocketError)), this, SLOT(handleError(QAbstractSocket::SocketError)));
    QObject::connect(socket, SIGNAL(readyRead()), this, SLOT(dataReceived()));

    // Connect underlying layer to the socket.
    Communication::connectToDevice(socket);
}

bool CommunicationTcpSocket::isConnected() const
{
    if (socket == nullptr)
    {
        return false;
    }
    return socket->state() == QTcpSocket::ConnectedState;
}

void CommunicationTcpSocket::disconnect()
{
    if (socket != nullptr)
    {
        socket->disconnectFromHost();
    }
}

void CommunicationTcpSocket::sendBufferContent()
{
    if (socket == nullptr)
    {
        emit errorOccurred(QString("ERROR: Tried to send data without a valid socket."));
        return;
    }
    if (!isConnected())
    {
        emit errorOccurred(QString("ERROR: Tried to send data with socket not connected."));
        return;
    }

    //qDebug() << "CommunicationTcpSocket::send() " << sendBuffer.size() << " bytes:\n" << sendBuffer.toHex();
    socket->write(sendBuffer);
    sendBuffer.clear();
}

void CommunicationTcpSocket::handleError(QAbstractSocket::SocketError socketError)
{
    // Do not use socketError variable.
    Q_UNUSED(socketError)
    // Emit signal with error string.
    emit this->errorOccurred(socket->errorString());
}
