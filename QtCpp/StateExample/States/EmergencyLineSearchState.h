#ifndef EMERGENCYLINESEARCHSTATE_H
#define EMERGENCYLINESEARCHSTATE_H
#include "Framework/State.h"
#include "Framework/Robot.h"

class EmergencyLineSearchState : public State
{
public:
    EmergencyLineSearchState(Robot& robot)
        : State(robot)
    {
        lastKnownLineLocation = DefaultLastKnownLineLocation;
        tickCountAfterLineFound = -1;
    }

    void SetLastKnownLineLocation(int location)
    {
        lastKnownLineLocation = location;
    }

    virtual void OnEnteringState() override
    {
        std::cout << "Robot state changed to: EmergencyLineSearchState" << std::endl;
        if (lastKnownLineLocation == DefaultLastKnownLineLocation)
        {
            std::cout << "ERROR EmergencyLineSearchState: lastKnownLineLocation not set prior to entering this state! Fallback..." << std::endl;
            lastKnownLineLocation = 0;
        }
        // Manuálisan kanyarodunk és lassan haladva várjuk, hogy visszaérjünk a vonalra.
        robot.OverrideSteering(lastKnownLineLocation);
        robot.SetReferenceSpeed(0.2F);

        tickCountAfterLineFound = -1;
    }

    virtual void LineFound()
    {
        robot.AutoSteering();
        // Kell egy kis idő, mire rendesen rááll a robot a vonalra,
        //  addig még nem váltunk állapotot.
        tickCountAfterLineFound = InitialTickCountAfterLineFound;
        std::cout << "EmergencyLineSearchState: line found, waiting for proper vehicle alignment." << std::endl;
    }

    virtual void Tick()
    {
        if (tickCountAfterLineFound > 0)
        {
            tickCountAfterLineFound--;
            if (tickCountAfterLineFound == 0)
            {
                // Lehet, hogy nem vettuk eszre a gyorsitasi szakasz veget,
                //  igy kockazatos a Fast allapotba visszaterni.
                robot.SetState(StateStore::Instance.GetState(StateStore::States::Default));
            }
        }
    }

    virtual void OnLeavingState() override
    {
        lastKnownLineLocation = DefaultLastKnownLineLocation;
    }

private:
    int lastKnownLineLocation;
    const int DefaultLastKnownLineLocation = -10000;

    int tickCountAfterLineFound;
    const int InitialTickCountAfterLineFound = 3;
};

#endif // EMERGENCYLINESEARCHSTATE_H
