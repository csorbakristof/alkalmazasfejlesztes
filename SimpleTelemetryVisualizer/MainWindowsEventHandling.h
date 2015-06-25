#pragma once
#ifndef MAINWINDOWSEVENTHANDLING_H
#define MAINWINDOWSEVENTHANDLING_H
#include <QObject>
#include <QDebug>

class RobotProxy;
class QQmlContext;
class RobotStateHistory;

class MainWindowsEventHandling : public QObject
{
    Q_OBJECT

public:
    MainWindowsEventHandling(RobotProxy& robot, QQmlContext &qmlContext, RobotStateHistory &history);

    ~MainWindowsEventHandling() = default;

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
};

#endif // MAINWINDOWSEVENTHANDLING_H
