#pragma once
#ifndef MAINWINDOWSEVENTHANDLING_H
#define MAINWINDOWSEVENTHANDLING_H
#include <QObject>
#include <QDebug>

class RobotProxy;
class QQmlContext;
class RobotStateHistory;

class QQuickItem;
class QQmlApplicationEngine;

class MainWindowsEventHandling : public QObject
{
    Q_OBJECT

public:
    MainWindowsEventHandling(RobotProxy& robot, QQmlContext &qmlContext, RobotStateHistory &history);

    ~MainWindowsEventHandling() = default;

    /** Connects the used QML signals. Call this after the QML environment is set up. */
    void ConnectQmlSignals(QObject *rootObject);

public slots:
    void accelerateCommand();

    void stopCommand();

    void resetCommand();

    void historyChanged();

signals:
    // Used to instruct the history graph (QML control) to redraw itself
    void historyContextUpdated();

private:
    RobotProxy& robot;

    // Followings are needed for data binding refresh operations
    QQmlContext &qmlContext;
    RobotStateHistory &history;

    static QQuickItem* FindItemByName(QList<QObject*> nodes, const QString& name);

    static QQuickItem* FindItemByName(QObject *rootObject, const QString& name);
};

#endif // MAINWINDOWSEVENTHANDLING_H
