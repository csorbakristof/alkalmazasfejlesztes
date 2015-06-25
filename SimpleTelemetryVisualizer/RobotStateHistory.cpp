#include "RobotStateHistory.h"
#include "RobotState.h"

RobotStateHistory::RobotStateHistory()
    : QObject(nullptr), currentState(nullptr)
{
}

void RobotStateHistory::Add(const RobotState& state)
{
    // TODO Add history cleanup function!
    std::unique_ptr<RobotState> newState = std::make_unique<RobotState>();
    newState->CopyFrom(state);
    stateList.append(newState.get());
    currentState = (RobotState*)newState.get(); // This has to be done before moving ownership
    container.push_back(std::move(newState));

    // Assemble graph data set
    graphTimestamps.clear();
    graphVelocities.clear();
    graphAcceleration.clear();
    int graphStateNumber = stateList.size() < shownStateNumber ? stateList.size() : shownStateNumber;
    auto it = container.end()-graphStateNumber;
    for(;it!=container.end();++it)
    {
        RobotState *currentState = it->get();
        graphTimestamps.append(currentState->timestamp());
        graphVelocities.append(currentState->v());
        graphAcceleration.append(currentState->a());
    }
/*    for(int i=0; i<20; i++)
    {
        graphTimestamps.append(i);
        graphVelocities.append(i+rand()%20);
        graphAcceleration.append(20-i+rand()%20);
    } */

    emit historyChanged();
}
