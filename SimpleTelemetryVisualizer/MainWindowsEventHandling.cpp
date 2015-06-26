#include "MainWindowsEventHandling.h"
#include "RobotProxy.h"
#include <QQmlContext>
#include "RobotStateHistory.h"

#include <QQuickItem>
#include <QQmlApplicationEngine>

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
    // Reset model of history view to apply changed
    qmlContext.setContextProperty(QStringLiteral("historyModel"), QVariant::fromValue(history.stateList));
    qmlContext.setContextProperty(QStringLiteral("currentState"), QVariant::fromValue(history.currentState));

    qmlContext.setContextProperty(QStringLiteral("historyGraphTimestamps"), QVariant::fromValue(history.graphTimestamps));
    qmlContext.setContextProperty(QStringLiteral("historyGraphVelocity"), QVariant::fromValue(history.graphVelocities));
    qmlContext.setContextProperty(QStringLiteral("historyGraphAcceleration"), QVariant::fromValue(history.graphAcceleration));

    // Now the views can repaint themselves.
    emit historyContextUpdated();
}

QQuickItem* MainWindowsEventHandling::FindItemByName(QList<QObject*> nodes, const QString& name)
{
    for(int i = 0; i < nodes.size(); i++)
    {
//        qDebug() << "Node: " << nodes[i]->objectName();
        // search for node
        if (nodes.at(i) && nodes.at(i)->objectName() == name)
        {
//            qDebug() << "MainWindowsEventHandling::FindItemByName: object found";
            return dynamic_cast<QQuickItem*>(nodes.at(i));
        }
        // search in children
        else if (nodes.at(i) && nodes.at(i)->children().size() > 0)
        {
//            qDebug() << "MainWindowsEventHandling::FindItemByName: recursion to children";
            QQuickItem* item = FindItemByName(nodes.at(i)->children(), name);
            if (item)
                return item;
        }
    }
    // not found
    return NULL;
}

QQuickItem* MainWindowsEventHandling::FindItemByName(QObject *rootObject, const QString& name)
{
    return FindItemByName(rootObject->children(), name);
}

void MainWindowsEventHandling::ConnectQmlSignals(QObject *rootObject)
{
    QQuickItem *historyGraph = FindItemByName(rootObject,QString("historyGraph"));
    if (historyGraph)
    {
        QObject::connect(this, SIGNAL(historyContextUpdated()), historyGraph, SLOT(requestPaint()));
    }
    else
    {
        qDebug() << "ERROR: Cannot find historyGraph object in the QML context.";
    }
}
