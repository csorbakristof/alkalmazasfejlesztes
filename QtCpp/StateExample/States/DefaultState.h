#ifndef DEFAULTSTATE_H
#define DEFAULTSTATE_H
#include <iostream>
#include "Framework/Robot.h"
#include "Framework/StateStore.h"

class DefaultState : public State
{
public:
    DefaultState(Robot& robot)
        : State(robot)
    { }

    virtual void AccelerationStartMarkDetected() override
    {
        robot.SetState(StateStore::Instance.GetState(StateStore::States::Fast));
    }

    virtual void OnEnteringState() override
    {
        std::cout << "Robot state changed to: DefaultState" << std::endl;
        robot.SetReferenceSpeed(1.0F);
    }

};

#endif // DEFAULTSTATE_H
