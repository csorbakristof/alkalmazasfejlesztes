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

    StateStore();

    void Init(Robot& robot);

    State* GetState(std::string name);

private:
    std::map<std::string, std::unique_ptr<State>> states;
};

#endif // STATESTORE
