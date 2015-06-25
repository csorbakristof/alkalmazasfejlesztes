#include <QApplication>
#include <QQmlApplicationEngine>
#include <QDebug>
#include <QQmlContext>
#include "MainWindowsEventHandling.h"
#include "Simulator/Simulator.h"
#include "RobotProxy.h"
#include "Communication/CommunicationTcpSocketClient.h"
#include "RobotStateHistory.h"

void setupQmlImportPath(QQmlApplicationEngine& engine)
{
    engine.addImportPath(QString(":/"));
    auto list = engine.importPathList();
    for(auto path : list)
    {
        qDebug() << "QML import path item: " << path;
    }
}

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

    /* Add initial history value */
    RobotState initialState(RobotState::Status::Default, 0L, 0.0F, 0.0F, 0.0F, 0);
    history.Add(initialState);  // This also triggers historyChanged() !

    // TODO: mk Communication for SerialPort as well! Demonstrate object decomposition advantage!
    // simulate a history change
    handler.historyChanged();

    setupQmlImportPath(engine);

    // Bind data row to the chart
/*    QList<int> timestamps;
    timestamps.append(0);
    timestamps.append(1);
    timestamps.append(2);
    timestamps.append(3);
    timestamps.append(4);
    QList<int> datarow1;
    datarow1.append(10);
    datarow1.append(20);
    datarow1.append(30);
    datarow1.append(40);
    datarow1.append(50);
    QList<int> datarow2;
    datarow2.append(70);
    datarow2.append(60);
    datarow2.append(50);
    datarow2.append(40);
    datarow2.append(30);
    context->setContextProperty(QStringLiteral("historyGraphTimestamps"), QVariant::fromValue(timestamps));
    context->setContextProperty(QStringLiteral("historyGraphDataRow1"), QVariant::fromValue(datarow1));
    context->setContextProperty(QStringLiteral("historyGraphDataRow2"), QVariant::fromValue(datarow2));*/




    engine.load(QUrl(QStringLiteral("qrc:/main.qml")));

    auto rootObjects = engine.rootObjects();
    if (rootObjects.size() == 0)
    {
        qDebug() << "ERROR: Could not create QML root objects. See QML debug info for details.";
        return -1;
    }
    QObject *rootObject = rootObjects[0];

    QObject::connect(rootObject, SIGNAL(resetCommandCpp()),
                     &handler, SLOT(resetCommand()));
    QObject::connect(rootObject, SIGNAL(accelerateCommandCpp()),
                     &handler, SLOT(accelerateCommand()));
    QObject::connect(rootObject, SIGNAL(stopCommandCpp()),
                     &handler, SLOT(stopCommand()));

    return app.exec();
}
