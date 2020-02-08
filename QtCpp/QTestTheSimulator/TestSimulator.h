#ifndef TESTSIMULATOR_H
#define TESTSIMULATOR_H
#include "Simulator.h"

class TestSimulator : public Simulator
{
public:
    TestSimulator(Communication &comm)
        : Simulator(&comm)
    {
    }

    void reset(float intervalSec)
    {
        Simulator::reset(intervalSec);
    }

    void SetStateA(double a)
    {
        this->state.setA(a);
    }

    double GetStateV() const
    {
        return this->state.v();
    }

    void Tick()
    {
        Simulator::tick();
    }

    void DataReady(QDataStream& inputStream)
    {
        Simulator::dataReady(inputStream);
    }
};

#endif // TESTSIMULATOR_H
