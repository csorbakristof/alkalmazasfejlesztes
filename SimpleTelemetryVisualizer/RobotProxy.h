#pragma once
#ifndef ROBOTPROXY_H
#define ROBOTPROXY_H
#include <QObject>
#include "RobotState.h"
#include "Communication/Communication.h"

class RobotStateHistory;
class Communication;

class RobotProxy : public QObject
{
    Q_OBJECT

public:
    RobotProxy(RobotStateHistory& history, Communication& communication);
    ~RobotProxy() = default;

    void reset()
    {
        RobotState newState;
        newState.setStatus(RobotState::Status::Reset);
        communication.send(newState);
        qDebug() << "Reset command sent to robot.";
    }

    void accelerate()
    {
        RobotState newState;
        newState.setStatus(RobotState::Status::Accelerate);
        newState.setA(1);
        communication.send(newState);
        qDebug() << "Accelerate command sent to robot.";
    }

    void stop()
    {
        RobotState newState;
        newState.setStatus(RobotState::Status::Stopping);
        communication.send(newState);
        qDebug() << "Stop command sent to robot.";
    }

public slots:
    // A whole message has been received.
    // Connected to the communication object.
    void dataReady(QDataStream& stream);

private:
    /** This is used to store all the states of the robot. */
    RobotStateHistory& history;

    /** Communication object */
    Communication& communication;
};

#endif // ROBOTPROXY_H
