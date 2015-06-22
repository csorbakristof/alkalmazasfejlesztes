#include "CommunicationTcpSocketClient.h"

CommunicationTcpSocketClient::CommunicationTcpSocketClient()
{

}

CommunicationTcpSocketClient::~CommunicationTcpSocketClient()
{

}

void CommunicationTcpSocketClient::connect(QString url, int port)
{
    socket.connectToHost(url, port, QIODevice::ReadWrite);
    // Connect underlying layers to this socket.
    setSocket(&socket);
}
