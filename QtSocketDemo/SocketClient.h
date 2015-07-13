#pragma once
#ifndef SOCKETCLIENT_H
#define SOCKETCLIENT_H
#include <memory>
#include <QTcpSocket>
#include <QDataStream>

class SocketClient : public QObject
{
    Q_OBJECT

public:
    SocketClient();
    ~SocketClient() = default;

    void connect(QString url, int port);

    void send(QString text);

private:
    /** The underlying QTcpSocket instance. */
    QTcpSocket socket;

    std::unique_ptr<QDataStream> receiveStream;
    QByteArray sendBuffer;

    qint32 currentMessageSize;

signals:
    void dataReady(QDataStream&);

private slots:

    void handleError(QAbstractSocket::SocketError socketError);
    void dataReceived();

};

#endif // SOCKETCLIENT_H
