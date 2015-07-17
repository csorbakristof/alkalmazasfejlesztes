#pragma once
#ifndef ROBOTPROXY_H
#define ROBOTPROXY_H
#include <QObject>
#include "RobotState.h"
#include "Communication/Communication.h"

class RobotStateHistory;
class Communication;

/**
 * @brief A robot proxyja.
 *
 * Ezen kereszül lehet adatok küldeni és állapotjelzéseket fogadni a robottól.
 * A korábbi állapotokat a kapott RobotStateHistory objektumban tárolja.
 *
 * A konstruktor köti a dataReady() slotot a kommunikációs objektumhoz és kezeli az adatfogadást.
 */
class RobotProxy : public QObject
{
    Q_OBJECT

public:
    /**
     * @brief Konstruktor.
     * @param history   A használandó RobotStateHistory példány.
     * @param communication A használandó kommunikációs objektum.
     */
    RobotProxy(RobotStateHistory& history, Communication& communication);
    ~RobotProxy() = default;

    /**
     * @brief Reseteli a robotot.
     */
    void reset();

    /**
     * @brief Gyorsítási parancsot küld a robotnak.
     */
    void accelerate();

    /**
     * @brief Megállási parancs a robotnak.
     */
    void stop();

public slots:
    /**
     * Akkor hívódik, ha új állapot érkezett a robottól.
     * Feldolgozza és eltárolja az üzenetet.
     *
     * A konstruktor köti a kommunikációs objektum üzenet beérkezést jelző
     * signaljához.
     *
     * @param stream    A bejövő adatfolyam, amiből az állapotot ki kell olvasni.
     */
    void dataReady(QDataStream& stream);

private:
    /** A korábbi és aktuális állapotot tároló RobotStateHistory példány. */
    RobotStateHistory& history;

    /** A kommunikációs objektum */
    Communication& communication;
};

#endif // ROBOTPROXY_H
