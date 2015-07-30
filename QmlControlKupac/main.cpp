#include <QApplication>
#include <QQmlApplicationEngine>
#include "MainWindowCppSide.h"

int main(int argc, char *argv[])
{
    QApplication app(argc, argv);

    QQmlApplicationEngine engine;
    engine.load(QUrl(QStringLiteral("qrc:/main.qml")));

    MainWindowCppSide mainWindowCppSide(engine.rootObjects()[0]);
    Q_UNUSED(mainWindowCppSide);

    return app.exec();
}

