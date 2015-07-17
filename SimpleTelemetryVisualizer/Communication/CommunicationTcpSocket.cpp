#include <QTimer>
#include "CommunicationTcpSocket.h"

CommunicationTcpSocket::CommunicationTcpSocket()
    : Communication(), socket(nullptr)
{
}

void CommunicationTcpSocket::setSocket(QTcpSocket *newSocket)
{
    // Slotok csatlakoztatása az új sockethez
    if (socket != nullptr && newSocket != socket)
    {
        QObject::disconnect(socket, SIGNAL(error(QAbstractSocket::SocketError)), this, SLOT(CommunicationTcpSocket::handleError(QAbstractSocket::SocketError)));
        QObject::disconnect(socket, SIGNAL(readyRead()), this, SLOT(dataReceived()));
    }
    socket = newSocket;
    QObject::connect(socket, SIGNAL(error(QAbstractSocket::SocketError)), this, SLOT(handleError(QAbstractSocket::SocketError)));
    QObject::connect(socket, SIGNAL(readyRead()), this, SLOT(dataReceived()));

    // Alsóbb réteg (ősosztály) csatlakoztatása a sockethez
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
        emit errorOccurred(QString("HIBA: Adatküldés socket nélkül."));
        return;
    }
    if (!isConnected())
    {
        emit errorOccurred(QString("HIBA: Adatküldés nyitott socket nélkül."));
        return;
    }

    qDebug() << "CommunicationTcpSocket::send() " << sendBuffer.size() << " bájt:\n" << sendBuffer.toHex();
    socket->write(sendBuffer);
    sendBuffer.clear();
}

void CommunicationTcpSocket::handleError(QAbstractSocket::SocketError socketError)
{
    Q_UNUSED(socketError)
    emit this->errorOccurred(socket->errorString());
}
