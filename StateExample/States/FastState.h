#ifndef FASTSTATE_H
#define FASTSTATE_H
#include "Framework/State.h"
#include "Framework/Robot.h"
#include "Framework/StateStore.h"
#include "States/EmergencyLineSearchState.h"

class FastState : public State
{
public:
    FastState(Robot& robot)
        : State(robot)
    { }

    virtual void OnEnteringState() override
    {
        std::cout << "Robot state changed to: FastState" << std::endl;
        robot.SetReferenceSpeed(4.0F);
        tickCounter = 0;
        tickCountBeforeBreaking = -1;
    }

    virtual void Tick() override
    {
        tickCounter++;
        if (tickCounter > maxTickCountInThisState)
        {
            std::cout << "Warning FastState: suspiciously long time has passed in this state. Fallback to default state." << std::endl;
            robot.SetState(StateStore::Instance.GetState("Default"));
        }

        if (tickCountBeforeBreaking>0)
        {
            tickCountBeforeBreaking--;
            if (tickCountBeforeBreaking==0)
            {
                robot.SetState(StateStore::Instance.GetState("Default"));
            }
        }
    }

    virtual void AccelerationEndMarkDetected() override
    {
        std::cout << "FastState: Preparing to decelerate..." << std::endl;
        tickCountBeforeBreaking = InitialTickCountBeforeBreaking;
    }

    virtual void LineLost(int lastKnownLocation) override
    {
        std::cout << "ERROR FastState: Lost the line!" << std::endl;
        EmergencyLineSearchState *s = (EmergencyLineSearchState*)StateStore::Instance.GetState("EmergencyLineSearch");
        s->SetLastKnownLineLocation(lastKnownLocation);
        robot.SetState(s);
    }

    // Jobb minden allapot belepesenel beallitani a sebesseget, nem az OnLeavingState-ben...

private:
    int tickCounter;

    // Pozitiv ertek: elertuk a gyorsulasi szakasz veget, de meg egy kicsit varunk a fekezessel.
    int tickCountBeforeBreaking;

    // Ez kesobb lehet globalis konfig beallitas.
    const int maxTickCountInThisState = 100;
    const int InitialTickCountBeforeBreaking = 2;

};

#endif // FASTSTATE_H
