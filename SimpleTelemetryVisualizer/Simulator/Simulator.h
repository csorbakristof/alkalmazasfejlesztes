#ifndef SIMULATOR_H
#define SIMULATOR_H
#include <QObject>
#include <QTimer>
#include "Communication/CommunicationTcpSocketServer.h"
#include "RobotState.h"

/** Represents a simulated robot which can communicate via CommunicationTcpSocket. */
class Simulator : public QObject
{
    Q_OBJECT

public:
    explicit Simulator(int port);
    ~Simulator() = default;

    void start(float intervalSec);

private:
    CommunicationTcpSocketServer communication;

    QTimer timer;

    // Sampling rate of the simulation (in seconds)
    float dt;

    RobotState state;

private slots:
    /** Called by QTimer as times goes by.
     * Calculates next robot parameters. */
    void tick();

    /** Data received, called by communication. */
    void dataReady(QDataStream& inputStream);
};

#endif // SIMULATOR_H
