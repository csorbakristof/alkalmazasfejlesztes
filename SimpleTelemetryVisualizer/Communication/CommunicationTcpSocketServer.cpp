#include "CommunicationTcpSocketServer.h"

CommunicationTcpSocketServer::CommunicationTcpSocketServer(int port)
    : serverSocket(this)
{
    if (!serverSocket.listen(QHostAddress::Any, port))
    {
        qWarning() << "Failed to open server socket: ";
        qWarning() << serverSocket.errorString();
    }
    else
    {
        qWarning() << "Server socket is now waiting for connection.\n";
    }

    connect(&serverSocket, SIGNAL(newConnection()), this, SLOT(newConnection()));
}

CommunicationTcpSocketServer::~CommunicationTcpSocketServer()
{

}

void CommunicationTcpSocketServer::newConnection()
{
    QTcpSocket *newSocket = serverSocket.nextPendingConnection();
    if (newSocket)
    {
        if (newSocket->isOpen()) {
            connect(newSocket, SIGNAL(disconnected()), this, SLOT(disconnected()));
        }
        // Connect new socket to underlying layers.
        setSocket(newSocket);
        qWarning() << "New connection established.\n";
    }
}

void CommunicationTcpSocketServer::disconnected()
{
    qWarning() << "Server side client socket was disconnected.\n";
}
