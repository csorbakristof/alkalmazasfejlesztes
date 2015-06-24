#pragma once
#ifndef ROBOTSTATEHISTORY_H
#define ROBOTSTATEHISTORY_H
#include <QtCore>
//#include <QList>
#include <memory>
#include <vector>
#include "RobotState.h"

class RobotStateHistory : public QObject
{
    Q_OBJECT

public:
    RobotStateHistory();
    virtual ~RobotStateHistory() = default;

    // This needs to be a QObject* list. Pointers to derived classes are
    //  not recognized by QML for proper data binding.
    QList<QObject*> stateList;

    /** This container handles the ownership of the elements referenced in stateList */
    std::vector<std::unique_ptr<RobotState>> container;

    /** Adds a copy of the state to the end of the history. */
    void Add(const RobotState& state);

signals:
    void historyChanged();
};

#endif // ROBOTSTATEHISTORY_H
