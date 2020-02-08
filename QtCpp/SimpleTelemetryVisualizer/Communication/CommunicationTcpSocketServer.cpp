#include "CommunicationTcpSocketServer.h"

CommunicationTcpSocketServer::CommunicationTcpSocketServer(int port)
    : serverSocket(this)
{
    if (!serverSocket.listen(QHostAddress::Any, port))
    {
        qWarning() << "Nem sikerült megnyitni a szerver socketet: ";
        qWarning() << serverSocket.errorString();
    }
    else
    {
        qWarning() << "A szerver socket kapcsolatra vár.";
    }

    connect(&serverSocket, SIGNAL(newConnection()), this, SLOT(newConnection()));
}

void CommunicationTcpSocketServer::newConnection()
{
    QTcpSocket *newSocket = serverSocket.nextPendingConnection();
    if (newSocket)
    {
        if (newSocket->isOpen()) {
            connect(newSocket, SIGNAL(disconnected()), this, SLOT(disconnected()));
        }
        setSocket(newSocket);
        qWarning() << "Új kapcsolat létesült.\n";
    }
}

void CommunicationTcpSocketServer::disconnected()
{
    qWarning() << "A szerver oldali socket lezáródott.";
}
