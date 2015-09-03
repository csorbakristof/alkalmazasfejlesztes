#include <QDebug>
#include <QDataStream>
#include "Application.h"

Application::Application(int argc, char *argv[], CommunicationSerialPort &serialPort)
    : QCoreApplication(argc,argv), serialPort(serialPort), counter(0)
{
    qWarning() << "Application::ctor";
    connect(&serialPort, &CommunicationSerialPort::dataReady, this, &Application::dataReady);
    connect(&timer, &QTimer::timeout, this, &Application::tick);
}

void Application::dataReady(QDataStream& inStream)
{
    QString text;
    inStream >> text;
    qWarning() << "A soros porton üzenet érkezett: " << text;
}

void Application::startSending()
{
    qWarning() << "Application::startSending()";
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
    serialPort.send(QString("Mizu?"));
}
