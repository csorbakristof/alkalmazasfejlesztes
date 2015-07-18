#include <QDebug>
#include "Simulator.h"

Simulator::Simulator(int velocity)
    : QObject(nullptr), x(0), v(velocity)
{
}

void Simulator::tick()
{
    x += v;
    qDebug() << "Szimulátor: x =" << x
             << "és v =" << v;
    if (x % 10 == 0)
    {
        // Kiadjuk a jelet, hogy szép helyre érkeztünk
        emit arrived(x);
    }
}

