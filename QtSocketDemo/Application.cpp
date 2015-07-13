#include "Application.h"
#include <QDataStream>

Application::Application(int argc, char *argv[], SocketServer &server, SocketClient &client)
    : QCoreApplication(argc,argv), server(server), client(client), counter(0)
{
    connect(&client, SIGNAL(dataReady(QDataStream&)), this, SLOT(clientDataReady(QDataStream&)));
    connect(&server, SIGNAL(dataReady(QDataStream&)), this, SLOT(serverDataReady(QDataStream&)));
    connect(&timer, SIGNAL(timeout()), this, SLOT(tick()));
}

void Application::clientDataReady(QDataStream& inStream)
{
    QString text;
    inStream >> text;
    qDebug() << "Data ready in the client socket: " << text;
}

void Application::serverDataReady(QDataStream& inStream)
{
    QString text;
    inStream >> text;
    qDebug() << "Data ready in the server socket: " << text;

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
    qDebug() << "Application::tick";
    counter++;
    if (counter > 10)
    {
        qDebug() << "Ran 10 times, now exiting. Bye!";
        this->exit(0);
        return;
    }
    client.send(QString("Mizu?"));
}

