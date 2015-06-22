#ifndef COMMUNICATIONTCPSOCKETCLIENT_H
#define COMMUNICATIONTCPSOCKETCLIENT_H
#include "CommunicationTcpSocket.h"

/** Client side for TcpSocket based communication. */
class CommunicationTcpSocketClient : public CommunicationTcpSocket
{
public:
    CommunicationTcpSocketClient();
    ~CommunicationTcpSocketClient();

    void connect(QString url, int port);

private:
    /** The underlying QTcpSocket instance. */
    QTcpSocket socket;

};

#endif // COMMUNICATIONTCPSOCKETCLIENT_H
