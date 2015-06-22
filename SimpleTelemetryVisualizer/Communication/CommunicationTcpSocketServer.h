#ifndef COMMUNICATIONTCPSOCKETSERVER_H
#define COMMUNICATIONTCPSOCKETSERVER_H
#include <QTcpServer>
#include "CommunicationTcpSocket.h"

// TODO: make this behave like an automatic reconnecting client socket.
// Or move socket data handling into separate location so that it can be shared
//  between client and server socket.
class CommunicationTcpSocketServer : public CommunicationTcpSocket
{
    Q_OBJECT

public:
    CommunicationTcpSocketServer(int port);
    ~CommunicationTcpSocketServer();

private:
    /** Underlying server socket instance. */
    QTcpServer serverSocket;

private slots:
    /** Server socket received new connection. */
    // Link data reception signal, enable send, set connected status
    void newConnection();

    /** Client has disconnected. */
    // TODO: disconnect client socket,
    //  suspend send functions, unlink data reception signal
    void disconnected();
};

#endif // COMMUNICATIONTCPSOCKETSERVER_H
