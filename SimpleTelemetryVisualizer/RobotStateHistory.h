#pragma once
#ifndef ROBOTSTATEHISTORY_H
#define ROBOTSTATEHISTORY_H
#include <QtCore>
//#include <QList>
#include <memory>
#include <vector>
#include "RobotState.h"

/**
 * @brief Stores the robot states as a history.
 */
class RobotStateHistory : public QObject
{
    Q_OBJECT

public:
    /**
     * @brief Contructor.
     */
    RobotStateHistory();
    virtual ~RobotStateHistory() = default;

    /**
     * List of states to be binded to the QML models.
     * This list stores only pointers to objects owned by container.
     *
     * @warning This needs to be a QObject* list. Pointers to derived classes are not recognized by QML for proper data binding.
     */
    QList<QObject*> stateList;

    /** Pointer to the most current state. Updated by add(). */
    RobotState *currentState;

    /** This container handles the ownership of the elements referenced in stateList */
    std::vector<std::unique_ptr<RobotState>> container;

    /** Adds a copy of the state to the end of the history. */
    void Add(const RobotState& state);

    /** \addtogroup Containers for direct visualization.
     * They contain only the last shownStateNumber values.
     * Updated by Add().
     *  @{
     */
    QList<int> graphTimestamps;
    QList<int> graphVelocities;
    QList<int> graphAcceleration;
    /** @}*/

    /** The number of shown states. */
    const int shownStateNumber = 20;

signals:
    /** Signal emitted upon Add(), as the history has changed. */
    void historyChanged();
};

#endif // ROBOTSTATEHISTORY_H
