#include <QCoreApplication>
#include <QDebug>
#include "Application.h"
#include "Simulator.h"

Application::Application(int argc, char *argv[])
    : QCoreApplication(argc, argv),
      simulator(2),
      timer()
{
    // Sok hibának az az oka, hogy
    // - valamelyik objektum nem publikusan származik a QObject-ből
    // - az objektum nem tartalmazza a Q_OBJECT makrót
    // - valamelyik & jel lemarad
    connect(&timer, &QTimer::timeout, &simulator, &Simulator::tick);
    connect(&simulator, &Simulator::arrived,
            this, &Application::simulatorArrivedToNicePlace);

    // 1000ms a periódus idő
    timer.start(1000);
}

void Application::simulatorArrivedToNicePlace(int where)
{
    qDebug() << "A szimulátor szép helyre ért: " << where;
    exit(0);
}
