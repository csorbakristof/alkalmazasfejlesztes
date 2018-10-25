#pragma once
#ifndef SIMULATOR_H
#define SIMULATOR_H
#include <QObject>
#include <QTimer>
#include "Communication/CommunicationTcpSocketServer.h"
#include "RobotState.h"

/**
 * @brief A robot szimulátor.
 *
 * Van egy belső, RobotState típusú állapota, melyet egy QTimer segítségével periodikusan frissít.
 * Létrehoz egy CommunicationTcpSocketServer objektumot a kommunikációhoz, amihez lehet csatlakozni.
 * Minden szimulációs lépés után elküldi az állapotát.
 * Ha egy RobotState objektumot kap, azt parancsként értelmezi.
 */
class Simulator : public QObject
{
    Q_OBJECT

public:
    /** Konstruktor.
     * @param port  A port, amin a létrehozott szerver hallgatózik.
    */
    explicit Simulator(Communication *communication);
    ~Simulator() = default;

    /** Elindítja a szimulátort.
     * @param intervalSec   A szimulátor periódusideje.
     */
    void start(float intervalSec);

protected:
    /** Alapértékre állítja a belső paramétereket.
     * @param intervalSec   A szimulátor periódusideje.
     */
    void reset(float intervalSec);

    /** Belső szerver a kommunikációhoz. */
    Communication *communication;

    /** Időzítő a tick() metódus periodikus hívására. */
    QTimer timer;

    /** A periódus idő (másodpercben). */
    float dt;

    /** A szimulátor pillanatnyi állapota. */
    RobotState state;

protected slots:
    /** A timer hívja meg, meghatározza a robot
     * állapotát a következő időpillanatban. */
    void tick();

    /** Új üzenet fogadását jelzi. */
    void dataReady(QDataStream& inputStream);
};

#endif // SIMULATOR_H
