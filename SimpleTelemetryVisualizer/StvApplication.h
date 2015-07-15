#pragma once
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
 * @brief Alkalmazás osztály. A main() példányosítja és indítja el.
 *
 * A konstruktor elvégzi az alapvető példányosításokat és a signalok összekapcsolását.
 */
class StvApplication : public QApplication
{
public:
    /** Konstruktor. Alapvető példányosítások és signal bekötések. */
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
