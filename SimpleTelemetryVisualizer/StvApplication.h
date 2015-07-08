#ifndef STVAPPLICATION_H
#define STVAPPLICATION_H
#include <QApplication>
#include <QQmlApplicationEngine>
#include "Simulator/Simulator.h"
#include "MainWindowsEventHandling.h"
#include "RobotProxy.h"
#include "Communication/CommunicationTcpSocketClient.h"
#include "RobotStateHistory.h"

/**
 * @brief Application class. main() instantiates it and runs it.
 *
 * Important constructions and signal linkings are performed in the constructor.
 */
class StvApplication : public QApplication
{
public:
    /** Constructor. Important initialization is performed here. */
    StvApplication(int argc, char *argv[]);
    ~StvApplication() = default;

private:
    Simulator simulator;
    QQmlApplicationEngine engine;
    RobotStateHistory history;
    CommunicationTcpSocketClient communication;
    RobotProxy robot;
    MainWindowsEventHandling handler;
};

#endif // STVAPPLICATION_H
