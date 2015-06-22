#pragma once
#ifndef MAINWINDOWSEVENTHANDLING_H
#define MAINWINDOWSEVENTHANDLING_H
#include <QObject>
#include <QDebug>

class RobotProxy;

class MainWindowsEventHandling : public QObject
{
    Q_OBJECT

public:
    MainWindowsEventHandling(RobotProxy& robot);

    ~MainWindowsEventHandling() = default;

public slots:
    void accelerateCommand();

    void stopCommand();

    void resetCommand();

private:
    RobotProxy& robot;
};

#endif // MAINWINDOWSEVENTHANDLING_H
