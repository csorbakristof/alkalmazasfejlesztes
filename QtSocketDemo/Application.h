#pragma once
#ifndef APPLICATION_H
#define APPLICATION_H
#include <QCoreApplication>
#include <QTimer>
#include <QDataStream>
#include "SocketClient.h"
#include "SocketServer.h"

/**
 * @brief Az alkalmazás osztály.
 * A demó során létrejön egy TCP socket szerver és egy hozzá kapcsolódó kliens. Bármelyik is fogad adatot (a másiktól),
 * az ebben az osztályban köt ki és itt kezeljük le, hogy mi történjen.
 *
 * Konstruktor paraméterként kapja meg a szerver és kliens socket kommunikációs
 * objektumokat, amiken keresztül a demót futtatja.
 *
 * A clientDataReady és serverDataReady slotokat beköti a SocketServer és SocketClient
 * dataReady signaljára, a tick-et pedig egy QTimer-re. Az előbbiek kiírják, ha jön adat, a tick pedig
 * elküld egy "Mizu?" üzenetet a kliensen keresztül.
 *
 * Ha a szerver oldal kap "Mizu?" stringet, akkor "Minden OK." választ küld.
 */
class Application : public QCoreApplication
{
    // A socket rendszer nem működik, ha nem megfelelő QObject az osztályunk.
    // Ez a makró kiegészíti az osztályt a Qt QObject-jéhez szükséges funkcionalitásokkal.
    Q_OBJECT

public:
    /** Konstruktor. A parancssori paramétereket nem használja. A server és client
     * dependency injectionként érkezik. */
    Application(int argc, char *argv[], SocketServer& server, SocketClient& client);
    ~Application() = default;

    /** A demó indítása. Elindítja a timer-t, ami a tick-et fogja triggerelni. */
    void startSending();

private:
    /** Szerver oldali kommunikáció. */
    SocketServer& server;

    /** Kliens oldali kommunikáció. */
    SocketClient& client;

    /** A periodikus "Mizu?" üzenetküldésért felelős időzítő. */
    QTimer timer;

    /** Számláló, hogy 10 üzenetküldés után leálljon a program. */
    int counter;

private slots:
    /** A client dataReady signaljához kötött slot. Csak megjeleníti a fogadott üzenetet. */
    void clientDataReady(QDataStream& inStream);

    /** A server dataReady signaljához kötött slot. "Mizu?" kérdés esetén
     * ez küldi vissza a választ. */
    void serverDataReady(QDataStream& inStream);

    /** A kliens oldal periodikusan küld "Mizu?" üzeneteket a szervernek. */
    void tick();
};

#endif // APPLICATION_H
