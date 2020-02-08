#include "CommunicationTcpSocketClient.h"

CommunicationTcpSocketClient::CommunicationTcpSocketClient()
{
}

void CommunicationTcpSocketClient::connect(QString url, int port)
{
    socket.connectToHost(url, port, QIODevice::ReadWrite);
    // Alsóbb rétegeket (ősosztály) is csatlakoztatni kell a sockethez.
    setSocket(&socket);
}
