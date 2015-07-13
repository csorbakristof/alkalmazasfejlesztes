#pragma once
#ifndef SOCKETSERVER_H
#define SOCKETSERVER_H
#include <QTcpServer>
#include <memory>

class QTcpSocket;

class SocketServer : public QObject
{
    Q_OBJECT

public:
    SocketServer();
    ~SocketServer() = default;

    void start(unsigned int port);

    void send(QString text);

private:
    QTcpServer serverSocket;

    QTcpSocket *currentConnectionSocket = nullptr;

    std::unique_ptr<QDataStream> receiveStream;

    QByteArray sendBuffer;

    qint32 currentMessageSize;


signals:
    void dataReady(QDataStream&);

private slots:
    void newConnection();

    void disconnected();

    void handleError(QAbstractSocket::SocketError socketError);

    void dataReceived();
};

#endif // SOCKETSERVER_H
