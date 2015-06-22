#include "RobotStateHistory.h"
#include "RobotState.h"

RobotStateHistory::RobotStateHistory()
    : QObject(nullptr)
{
}

void RobotStateHistory::Add(const RobotState& state)
{
    // TODO Add history cleanup function!
    std::unique_ptr<RobotState> newState = std::make_unique<RobotState>();
    newState->CopyFrom(state);
    stateList.append(newState.get());
    container.push_back(std::move(newState));
    emit historyChanged();
}
