#include "Application.h"
#include <QDataStream>

Application::Application(int argc, char *argv[], SocketServer &server, SocketClient &client)
    : QCoreApplication(argc,argv), server(server), client(client), counter(0)
{
    connect(&client, &SocketClient::dataReady, this, &Application::clientDataReady);
    connect(&server, &SocketServer::dataReady, this, &Application::serverDataReady);
    connect(&timer, &QTimer::timeout, this, &Application::tick);
}

void Application::clientDataReady(QDataStream& inStream)
{
    QString text;
    inStream >> text;
    qWarning() << "A kliens socketen üzenet érkezett: " << text;
}

void Application::serverDataReady(QDataStream& inStream)
{
    QString text;
    inStream >> text;
    qWarning() << "A szerver socketen üzenet érkezett: " << text;

    if (text == "Mizu?")
    {
        server.send("Minden OK.");
    }
}

void Application::startSending()
{
    timer.start(1000);
}

void Application::tick()
{
    qDebug() << "Application::tick hívás";
    counter++;
    if (counter > 10)
    {
        qDebug() << "10-szer lefutott a tick, itt a demó vége.";
        // A QCoreApplication osztálynak van exit() metódusa.
        this->exit(0);
        return;
    }
    client.send(QString("Mizu?"));
}
