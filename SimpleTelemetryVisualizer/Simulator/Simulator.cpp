#include "Simulator.h"

Simulator::Simulator(int port)
    : communication(port), state()
{
    // Fogadni akarjuk a parancsokat
    connect(&communication, SIGNAL(dataReady(QDataStream&)), this, SLOT(dataReady(QDataStream&)));
    // Periodikusan futnia kell a szimulációnak
    connect(&timer, SIGNAL(timeout()), this, SLOT(tick()));
}

void Simulator::start(float intervalSec)
{
    dt = intervalSec;
    state.setStatus(RobotState::Status::Default);
    state.setTimestamp(0);
    state.setX(0.0F);
    state.setV(0.0F);
    state.setA(0.0F);
    state.setLight(0);
    timer.start((long)(intervalSec*1000.0F));
}

void Simulator::tick()
{
    // Fizikai szimuláció
    state.setTimestamp(state.timestamp() + dt);
    state.setX(state.x() + state.v()*dt);
    state.setV(state.v() + state.a()*dt);

    if (state.v()<-10.0)
    {
        state.setV( -10.0F );
    }
    if (state.v()>10.0)
    {
        state.setV( 10.0F );
    }

    state.setLight( state.v()==10.0F ? 1.0F : 0.0F );

    // Magasabb szintű funkciók
    switch(state.status())
    {
    case RobotState::Status::Default:
        break;
    case RobotState::Status::Reset:
        qDebug() << "Simulator: Reset";
        state.setStatus(RobotState::Status::Default);
        state.setX(0.0F);
        state.setV(0.0F);
        state.setA(0.0F);
        state.setLight(0);
        break;
    case RobotState::Status::Stopping:
        if (state.v() > 1.5F)
        {
            qDebug() << "Simulator: Stop parancs, gyors lassítás";
            state.setA(-1.0F);
        }
        else if (state.v() > 0.1F)
        {
            qDebug() << "Simulator: Stop parancs, lassú lassítás";
            state.setA(-0.05F);
        }
        else if (state.v() < -1.5F)
        {
            qDebug() << "Simulator: Stop parancs, gyorsítás előre";
            state.setA(1.0F);
        }
        else if (state.v() < -0.1F)
        {
            qDebug() << "Simulator: Stop parancs, lassú gyorsítás előre";
            state.setA(0.05F);
        }
        else
        {
            // Majdnem megállt
            qDebug() << "Simulator: Megállt.";
            state.setStatus(RobotState::Status::Default);
            state.setA(0.0F);
        }
        break;
    case RobotState::Status::Accelerate:
        // Megjegyzés: a gyorsulás kért értékét már a parancs fogadásakor beállítottuk
        qDebug() << "HIBA: A szimulátor nem kerülhetne a Status::Accelerate állapotba.";
        break;
    default:
        Q_UNREACHABLE();
    }

    qDebug() << "Simulator: tick (" << state.timestamp()
             << "): állapot=" << state.getStatusName()
             << ", x=" << state.x()
             << ", v=" << state.v()
             << ", a=" << state.a()
             << ", lámpa:" << state.light();

    // Állapot küldése
    if (communication.isConnected())
    {
        communication.send(state);
    }
}

void Simulator::dataReady(QDataStream &inputStream)
{
    RobotState receivedState;
    receivedState.ReadFrom(inputStream);

    switch(receivedState.status())
    {
    case RobotState::Status::Default:
        qDebug() << "Simulator: OK parancs. Igen, minden OK, köszönöm!";
        break;
    case RobotState::Status::Reset:
        qDebug() << "Simulator: Reset parancs.";
        state.setStatus(RobotState::Status::Reset);
        break;
    case RobotState::Status::Stopping:
        qDebug() << "Simulator: Stop parancs.";
        state.setStatus(RobotState::Status::Stopping);
        break;
    case RobotState::Status::Accelerate:
        qDebug() << "Simulator: Gyorsítási parancs.";
        state.setStatus(RobotState::Status::Default);
        state.setA(receivedState.a());
        break;
    default:
        Q_UNREACHABLE();
    }
}
