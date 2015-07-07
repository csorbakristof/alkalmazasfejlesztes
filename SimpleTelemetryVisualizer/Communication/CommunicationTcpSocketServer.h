#ifndef COMMUNICATIONTCPSOCKETSERVER_H
#define COMMUNICATIONTCPSOCKETSERVER_H
#include <QTcpServer>
#include "CommunicationTcpSocket.h"

/**
 * @brief TCP/IP socket server.
 *
 * After instantiation, it listens to the given port and accepts new connections.
 * New connections are automatically handled.
 *
 * @warning After instantiation, do not forget to connect the signals inherited from Communication.
 */
class CommunicationTcpSocketServer : public CommunicationTcpSocket
{
    Q_OBJECT

public:
    /** Constructor */
    CommunicationTcpSocketServer(int port);
    ~CommunicationTcpSocketServer() = default;

private:
    /** Underlying server socket instance. */
    QTcpServer serverSocket;

private slots:
    /** Server socket received new connection.
     * Links data reception signal, enables send and sets connected status
     * This slot is connected by the ctor.
     */
    void newConnection();

    /** Client has disconnected. */
    // TODO: disconnect client socket, suspend send functions, unlink data reception signal
    void disconnected();
};

#endif // COMMUNICATIONTCPSOCKETSERVER_H
