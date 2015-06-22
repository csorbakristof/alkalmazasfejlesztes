#include "Simulator.h"

Simulator::Simulator(int port)
    : communication(port), state()
{
    connect(&communication, SIGNAL(dataReady(QDataStream&)), this, SLOT(dataReady(QDataStream&)));
    connect(&timer, SIGNAL(timeout()), this, SLOT(tick()));
}

void Simulator::start(int intervalMs)
{
    dt = intervalMs / 1000.0;
    state.setStatus(RobotState::Status::Default);
    state.setTimestamp(0);
    state.setX(0.0F);
    state.setV(0.0F);
    state.setA(0.0F);
    state.setLight(0);
    timer.start(intervalMs);
}

void Simulator::tick()
{
    // Physical simulation
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

    // Simulate robot higher functions
    switch(state.status())
    {
    case RobotState::Status::Default:
        break;
    case RobotState::Status::Reset:
        qDebug() << "Resetting simulator.";
        state.setStatus(RobotState::Status::Default);
        state.setX(0.0F);
        state.setV(0.0F);
        state.setA(0.0F);
        state.setLight(0);
        break;
    case RobotState::Status::Stopping:
        if (state.v() > 1.5F)
        {
            qDebug() << "Answering stop command: quick decelerating";
            state.setA(-1.0F);
        }
        else if (state.v() > 0.1F)
        {
            qDebug() << "Answering stop command: slow decelerating";
            state.setA(-0.05F);
        }
        else if (state.v() < -1.5F)
        {
            qDebug() << "Answering stop command: quick accelerating to stop reverse movement";
            state.setA(1.0F);
        }
        else if (state.v() < -0.1F)
        {
            qDebug() << "Answering stop command: slow accelerating to stop reverse movement";
            state.setA(0.05F);
        }
        else
        {
            // Almost stopped
            qDebug() << "Stopped.";
            state.setStatus(RobotState::Status::Default);
            state.setA(0.0F);
        }
        break;
    case RobotState::Status::Accelerate:
        // Note: acceleration was already set upon command reception
        qDebug() << "ERROR: Simulator should not have got into (Status::Accelerate).";
        break;
    default:
        Q_UNREACHABLE();
    }

    qDebug() << "Simulator tick (" << state.timestamp()
             << "): status=" << state.getStatusName()
             << ", x=" << state.x()
             << ", v=" << state.v()
             << ", a=" << state.a()
             << ", light:" << state.light();

    // Send results
    if (communication.isConnected())
    {
        communication.send(state);
    }
}

void Simulator::dataReady(QDataStream &inputStream)
{
    // TODO: check message size and framing

    RobotState receivedState;
    receivedState.ReadFrom(inputStream);

    switch(receivedState.status())
    {
    case RobotState::Status::Default:
        qDebug() << "Simulator: OK command received. Well, yes, I am OK, thanks.";
        break;
    case RobotState::Status::Reset:
        qDebug() << "Simulator: Reset command received.";
        state.setStatus(RobotState::Status::Reset);
        break;
    case RobotState::Status::Stopping:
        qDebug() << "Simulator: Stopping command received.";
        state.setStatus(RobotState::Status::Stopping);
        break;
    case RobotState::Status::Accelerate:
        qDebug() << "Simulator: Accelerate command received. Acceleration set.";
        state.setStatus(RobotState::Status::Default);
        state.setA(receivedState.a());
        break;
    default:
        Q_UNREACHABLE();
    }
}
