#include <QApplication>
#include <QQmlApplicationEngine>
#include <QDebug>
#include "MainWindowsEventHandling.h"

int main(int argc, char *argv[])
{
    QApplication app(argc, argv);

    QQmlApplicationEngine engine;
    engine.load(QUrl(QStringLiteral("qrc:/main.qml")));

    QObject *rootObject = engine.rootObjects()[0];

    MainWindowsEventHandling handler;

    QObject::connect(rootObject, SIGNAL(connectRobotCpp(QString)),
                     &handler, SLOT(connectRobot(QString)));

    return app.exec();
}
