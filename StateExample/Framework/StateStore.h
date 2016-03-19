#ifndef STATESTORE
#define STATESTORE
#include <string>
#include <map>
#include <memory>
#include "State.h"

class StateStore
{
public:
    static StateStore Instance;

    enum States
    {
        Default = 0,
        Fast = 1,
        EmergencyLineSearch = 2,
        stateCount = 3
    };

    StateStore();

    void Init(Robot& robot);

    State* GetState(States state);

private:
    State **states;
};

#endif // STATESTORE
