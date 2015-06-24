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

    Simulator simulator(3333);
    simulator.start(1.0F);

    QQmlApplicationEngine engine;
    QQmlContext *context = engine.rootContext();

    RobotStateHistory history;
    CommunicationTcpSocketClient communication;
    RobotProxy robot(history, communication);
    MainWindowsEventHandling handler(robot, *context, history);

    communication.connect(QStringLiteral("localhost"),3333);

    /* Add initial history value */
    RobotState initialState(RobotState::Status::Default, 0L, 0.0F, 0.0F, 0.0F, 0);
    history.Add(initialState);  // This also triggers historyChanged() !


    // TODO: mk Communication for SerialPort as well! Demonstrate object decomposition advantage!
    // simulate a history change
    handler.historyChanged();

    engine.load(QUrl(QStringLiteral("qrc:/main.qml")));

    QObject *rootObject = engine.rootObjects()[0];

    // Connect the simulator

    QObject::connect(rootObject, SIGNAL(resetCommandCpp()),
                     &handler, SLOT(resetCommand()));
    QObject::connect(rootObject, SIGNAL(accelerateCommandCpp()),
                     &handler, SLOT(accelerateCommand()));
    QObject::connect(rootObject, SIGNAL(stopCommandCpp()),
                     &handler, SLOT(stopCommand()));

    return app.exec();
}
