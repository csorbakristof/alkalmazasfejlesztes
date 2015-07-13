#ifndef APPLICATION_H
#define APPLICATION_H
#include <QCoreApplication>
#include <QTimer>
#include <QDataStream>
#include "SocketClient.h"
#include "SocketServer.h"

class Application : public QCoreApplication
{
    Q_OBJECT

public:
    Application(int argc, char *argv[], SocketServer& server, SocketClient& client);
    ~Application() = default;

    void startSending();

private:
    SocketServer& server;
    SocketClient& client;

    QTimer timer;

    int counter;

private slots:
    void clientDataReady(QDataStream& inStream);
    void serverDataReady(QDataStream& inStream);
    void tick();

};

#endif // APPLICATION_H
