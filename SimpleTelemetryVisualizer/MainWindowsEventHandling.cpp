#include "MainWindowsEventHandling.h"
#include "RobotProxy.h"
#include <QQmlContext>
#include "RobotStateHistory.h"

MainWindowsEventHandling::MainWindowsEventHandling(
        RobotProxy &robot, QQmlContext& qmlContext, RobotStateHistory& history)
    : robot(robot), qmlContext(qmlContext), history(history)
{
    QObject::connect(&history, SIGNAL(historyChanged()), this, SLOT(historyChanged()));
}

void MainWindowsEventHandling::accelerateCommand()
{
    robot.accelerate();
}

void MainWindowsEventHandling::stopCommand()
{
    robot.stop();
}

void MainWindowsEventHandling::resetCommand()
{
    robot.reset();
}

void MainWindowsEventHandling::historyChanged()
{
//    qDebug() << "MainWindowsEventHandling::historyChanged()";
    // Reset model of history view to apply changed
    qmlContext.setContextProperty(QStringLiteral("historyModel"), QVariant::fromValue(history.stateList));
    qmlContext.setContextProperty(QStringLiteral("currentState"), QVariant::fromValue(history.currentState));

    qmlContext.setContextProperty(QStringLiteral("historyGraphTimestamps"), QVariant::fromValue(history.graphTimestamps));
    qmlContext.setContextProperty(QStringLiteral("historyGraphVelocity"), QVariant::fromValue(history.graphVelocities));
    qmlContext.setContextProperty(QStringLiteral("historyGraphAcceleration"), QVariant::fromValue(history.graphAcceleration));

    // Invoking QML methods:
    // http://doc.qt.io/qt-5/qtqml-cppintegration-interactqmlfromcpp.html
    QObject *historyGraph = qmlContext.findChild<QObject*>("historyGraph");
    if (historyGraph)
    {
        QMetaObject::invokeMethod(historyGraph, "repaint");
    }
    else
    {
        qDebug() << "ERROR: Cannot find QML object historyGraph...";
    }

    emit historyContextUpdated();
}
