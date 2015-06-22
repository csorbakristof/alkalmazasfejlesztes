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
