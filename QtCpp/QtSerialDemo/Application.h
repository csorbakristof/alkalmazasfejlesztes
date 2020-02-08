#pragma once
#ifndef APPLICATION_H
#define APPLICATION_H
#include <QCoreApplication>
#include <QTimer>
#include <QDataStream>
#include "CommunicationSerialPort.h"

/**
 * @brief Az alkalmazás osztály.
 *
 * A demó feltételezi, hogy a COM5 porton található egy hardveres loopback eszköz, vagyis amit kiküldünk
 * rá, az azonnal vissza is érkezik.
 *
 * A demó során létrejön egy soros kapcsolat, melyre a kiküldött adatok vissza is érkeznek.
 *
 * Konstruktor paraméterként kapja meg a soros kommunikációs
 * objektumot, amin keresztül a demót futtatja.
 *
 * A dataReady slotot beköti a CommunicationSerialPort
 * dataReady signaljára, a tick-et pedig egy QTimer-re. Az előbbi kiírja, ha jön adat, a tick pedig
 * elküld egy "Mizu?" üzenetet keresztül.
 */
class Application : public QCoreApplication
{
    // A socket rendszer nem működik, ha nem megfelelő QObject az osztályunk.
    // Ez a makró kiegészíti az osztályt a Qt QObject-jéhez szükséges funkcionalitásokkal.
    Q_OBJECT

public:
    /** Konstruktor. A parancssori paramétereket nem használja. A server és client
     * dependency injectionként érkezik. */
    Application(int argc, char *argv[], CommunicationSerialPort& serialPort);
    ~Application() = default;

    /** A demó indítása. Elindítja a timer-t, ami a tick-et fogja triggerelni. */
    void startSending();

private:
    /** Soros kommunikáció. */
    CommunicationSerialPort& serialPort;

    /** A periodikus "Mizu?" üzenetküldésért felelős időzítő. */
    QTimer timer;

    /** Számláló, hogy 10 üzenetküldés után leálljon a program. */
    int counter;

private slots:
    /** A serialPort dataReady signaljához kötött slot. Csak megjeleníti a fogadott üzenetet. */
    void dataReady(QDataStream& inStream);

    /** Periodikusan küld "Mizu?" üzeneteket. */
    void tick();
};

#endif // APPLICATION_H
