#ifndef STATE_H
#define STATE_H
#include <QObject>
class Robot;

class State
{
public:
    State(Robot& robot)
        : robot(robot)
    {
    }

    // Default kezelo nem csinal semmit. Jobb, mintha abstract lenne.
    virtual void Tick() { }
    virtual void AccelerationStartMarkDetected() { }
    virtual void AccelerationEndMarkDetected() { }
    virtual void LineLost(int lastKnownLocation) { Q_UNUSED(lastKnownLocation); }
    virtual void LineFound() { }

    virtual void OnEnteringState() { }
    virtual void OnLeavingState() { }

protected:
    Robot& robot;
};

#endif // STATE_H
