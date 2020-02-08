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
    states = new State*[States::stateCount];

    states[States::Default] = new DefaultState(robot);
    states[States::Fast] = new FastState(robot);
    states[States::EmergencyLineSearch] = new EmergencyLineSearchState(robot);

    // Ezek az objektumok a program teljes életciklusa alatt kelleni fognak,
    //  így felszabadításuktól most eltekintünk.
}

State* StateStore::GetState(States state)
{
    return states[state];
}
