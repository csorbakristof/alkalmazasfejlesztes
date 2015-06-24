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
}
