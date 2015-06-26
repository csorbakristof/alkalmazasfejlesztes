#include <QApplication>
#include <QQmlApplicationEngine>
#include <QDebug>
#include <QQmlContext>
#include "MainWindowsEventHandling.h"
#include "Simulator/Simulator.h"
#include "RobotProxy.h"
#include "Communication/CommunicationTcpSocketClient.h"
#include "RobotStateHistory.h"

int main(int argc, char *argv[])
{
    QApplication app(argc, argv);

    // Set up and start the simulator
    Simulator simulator(3333);
    simulator.start(1.0F);

    QQmlApplicationEngine engine;
    QQmlContext *context = engine.rootContext();

    RobotStateHistory history;
    CommunicationTcpSocketClient communication;
    RobotProxy robot(history, communication);
    MainWindowsEventHandling handler(robot, *context, history);

    // Connect the simulator
    communication.connect(QStringLiteral("localhost"),3333);

    // TODO: mk Communication for SerialPort as well! Demonstrate object decomposition advantage!
    // simulate a history change
    handler.historyChanged();

    engine.load(QUrl(QStringLiteral("qrc:/main.qml")));

    auto rootObjects = engine.rootObjects();
    if (rootObjects.size() == 0)
    {
        qDebug() << "ERROR: Could not create QML root objects. See QML debug info for details.";
        return -1;
    }
    // Now we are ready to connect to the signals/slots of the QML side.
    QObject *rootObject = rootObjects[0];
    handler.ConnectQmlSignals(rootObject);

    QObject::connect(rootObject, SIGNAL(resetCommandCpp()),
                     &handler, SLOT(resetCommand()));
    QObject::connect(rootObject, SIGNAL(accelerateCommandCpp()),
                     &handler, SLOT(accelerateCommand()));
    QObject::connect(rootObject, SIGNAL(stopCommandCpp()),
                     &handler, SLOT(stopCommand()));

    return app.exec();
}
