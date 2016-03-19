#include "StateStore.h"
#include "States/DefaultState.h"
#include "States/FastState.h"
#include "States/EmergencyLineSearchState.h"

StateStore StateStore::Instance;

StateStore::StateStore()
{
}

void StateStore::Init(Robot& robot)
{
    states["Default"] = std::make_unique<DefaultState>(robot);
    states["Fast"] = std::make_unique<FastState>(robot);
    states["EmergencyLineSearch"] = std::make_unique<EmergencyLineSearchState>(robot);
}

State* StateStore::GetState(std::string name)
{
    return states[name].get();
}
