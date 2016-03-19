#ifndef ROBOT_H
#define ROBOT_H
#include <QObject>
#include <QTimer>
#include <iostream>
#include "Framework/State.h"

class Robot : public QObject
{
    Q_OBJECT

public:
    Robot()
    {
        currentState = nullptr;
    }

    void SetState(State* newState)
    {
        if (this->currentState != nullptr)
            this->currentState->OnLeavingState();
        this->currentState = newState;
        this->currentState->OnEnteringState();
    }

    void SetReferenceSpeed(float speedMeterPerSecond)
    {
        std::cout << "Robot::SetReferenceSpeed: reference speed is now " << speedMeterPerSecond << std::endl;
    }

    void OverrideSteering(int direction)
    {
        std::cout << "Robot::OverrideSteering: direction: " << direction << std::endl;
    }

    void AutoSteering()
    {
        std::cout << "Robot::AutoSteering" << std::endl;
    }

    void Tick()
    {
        this->currentState->Tick();
    }

    void AccelerationStartMarkDetected()
    {
        currentState->AccelerationStartMarkDetected();
    }

    void AccelerationEndMarkDetected()
    {
        currentState->AccelerationEndMarkDetected();
    }

    void LineLost(int lastKnownLocation)
    {
        currentState->LineLost(lastKnownLocation);
    }

    void LineFound()
    {
        currentState->LineFound();
    }


private:
    State* currentState;
};


#endif // ROBOT_H
