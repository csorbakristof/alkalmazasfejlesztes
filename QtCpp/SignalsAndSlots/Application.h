#pragma once
#ifndef APPLICATION_H
#define APPLICATION_H
#include <QCoreApplication>
#include <QObject>
#include <QTimer>
#include "Simulator.h"

class Application : public QCoreApplication
{
    Q_OBJECT

public:
    Application(int argc, char *argv[]);
    ~Application() = default;

private:
    Simulator simulator;
    QTimer timer;

private slots:
    void simulatorArrivedToNicePlace(int where);

};

#endif // APPLICATION_H
