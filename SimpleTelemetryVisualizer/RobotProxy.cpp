#include "RobotProxy.h"
#include "RobotStateHistory.h"
#include "Communication/Communication.h"
#include "RobotState.h"

RobotProxy::RobotProxy(RobotStateHistory& history, Communication &communication)
    : history(history), communication(communication)
{
    // We need to get notified if a new RobotState arrives.
    QObject::connect(&communication, SIGNAL(dataReady(QDataStream&)), this, SLOT(dataReady(QDataStream&)));
}

void RobotProxy::dataReady(QDataStream &stream)
{
    // Robot state is received and written into history.
    RobotState state;
    state.ReadFrom(stream);
    history.Add(state);
}

void RobotProxy::reset()
{
    RobotState newState;
    newState.setStatus(RobotState::Status::Reset);
    communication.send(newState);
    qDebug() << "Reset command sent to robot.";
}

void RobotProxy::accelerate()
{
    RobotState newState;
    newState.setStatus(RobotState::Status::Accelerate);
    newState.setA(1);
    communication.send(newState);
    qDebug() << "Accelerate command sent to robot.";
}

void RobotProxy::stop()
{
    RobotState newState;
    newState.setStatus(RobotState::Status::Stopping);
    communication.send(newState);
    qDebug() << "Stop command sent to robot.";
}
