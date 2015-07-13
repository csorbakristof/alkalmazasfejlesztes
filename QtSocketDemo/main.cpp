#include "Application.h"
#include "SocketServer.h"
#include "SocketClient.h"

int main(int argc, char *argv[])
{
    SocketServer server;
    SocketClient client;
    Application app(argc, argv, server, client);

    server.start(3333);

    client.connect(QString("localhost"), 3333);

    app.startSending();

    return app.exec();
}
