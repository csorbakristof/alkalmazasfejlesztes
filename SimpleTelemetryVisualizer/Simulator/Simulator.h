#ifndef SIMULATOR_H
#define SIMULATOR_H
#include <QObject>
#include <QTimer>
#include "Communication/CommunicationTcpSocketServer.h"
#include "RobotState.h"

/** Represents a simulated robot which can communicate via CommunicationTcpSocket.
 * The simulator has an internal RobotState.
 * The simulator sends its state to the clients. If a RobotState is received,
 * its status field may be interpreted as a command. (Reset, Stopping, Accelerating status.)
*/
class Simulator : public QObject
{
    Q_OBJECT

public:
    /** Constructor.
     * @param port  The TCP port to bind the server to.
    */
    explicit Simulator(int port);
    ~Simulator() = default;

    /** Starts the simulator.
     * @param intervalSec   The period time of the simulator.
     */
    void start(float intervalSec);

private:
    /** Internal socket server the simulator uses for communication. */
    CommunicationTcpSocketServer communication;

    /** Timer used for periodic triggering the simulator. */
    QTimer timer;

    /** Sampling rate of the simulation (in seconds) */
    float dt;

    /** The current state of the simulator. */
    RobotState state;

private slots:
    /** Called by QTimer as times goes by.
     * Calculates next robot parameters. */
    void tick();

    /** Data received, called by communication. */
    void dataReady(QDataStream& inputStream);
};

#endif // SIMULATOR_H
