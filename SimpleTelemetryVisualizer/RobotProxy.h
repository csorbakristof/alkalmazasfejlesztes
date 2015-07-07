#pragma once
#ifndef ROBOTPROXY_H
#define ROBOTPROXY_H
#include <QObject>
#include "RobotState.h"
#include "Communication/Communication.h"

class RobotStateHistory;
class Communication;

/**
 * A proxy representation of the robot. It is used to send commands to the robot
 * and it can receive the messages from the robot and store them in the status history.
 *
 * The constructor connects the dataReady() slot to the communication and makes sure
 * that received data will be stored appropriately.
 */
class RobotProxy : public QObject
{
    Q_OBJECT

public:
    /**
     * @brief Constructor.
     * @param history   The history container to use.
     * @param communication The communication object used.
     */
    RobotProxy(RobotStateHistory& history, Communication& communication);
    ~RobotProxy() = default;

    /**
     * @brief Instructs the robot to reset itself.
     */
    void reset();

    /**
     * @brief Instructs the robot to accelerate.
     */
    void accelerate();

    /**
     * @brief Instructs the robot to stop.
     */
    void stop();

public slots:
    /**
     * Called when a whole RobotState message has been received.
     * Processes and stores the message.
     *
     * This slot is connected to the communication object by the constructor.
     *
     * @param stream    The input data stream to read the RobotState from.
     */
    void dataReady(QDataStream& stream);

private:
    /** This is used to store all the states of the robot. */
    RobotStateHistory& history;

    /** Communication object */
    Communication& communication;
};

#endif // ROBOTPROXY_H
