#ifndef COMMUNICATIONTCPSOCKETCLIENT_H
#define COMMUNICATIONTCPSOCKETCLIENT_H
#include "CommunicationTcpSocket.h"

/** Client side for TcpSocket based communication. */
class CommunicationTcpSocketClient : public CommunicationTcpSocket
{
public:
    /** Constructor */
    CommunicationTcpSocketClient();

    /** Destructor */
    ~CommunicationTcpSocketClient() = default;

    /** Connect client to a server on given URL and port. */
    void connect(QString url, int port);

private:
    /** The underlying QTcpSocket instance. */
    QTcpSocket socket;

};

#endif // COMMUNICATIONTCPSOCKETCLIENT_H
