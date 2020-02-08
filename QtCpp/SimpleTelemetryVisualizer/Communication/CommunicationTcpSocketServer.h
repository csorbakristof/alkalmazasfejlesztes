#pragma once
#ifndef COMMUNICATIONTCPSOCKETSERVER_H
#define COMMUNICATIONTCPSOCKETSERVER_H
#include <QTcpServer>
#include "CommunicationTcpSocket.h"

/**
 * @brief TCP/IP szerver socket.
 *
 * Inicializálása után a megadott porton hallgatózik és fogadja az új kapcsolatokat.
 *
 * @warning Inicializálás után be kell kötni a Communication-től örökölt signalokat, hogy
 * a bejövő üzenetekről értesítést adjon.
 */
class CommunicationTcpSocketServer : public CommunicationTcpSocket
{
    Q_OBJECT

public:
    /** Konstruktor */
    CommunicationTcpSocketServer(int port);
    virtual ~CommunicationTcpSocketServer() = default;

private:
    /** A belső QTcpServer példány. */
    QTcpServer serverSocket;

private slots:
    /** A szerver sockethez új kapcsolatot érkezett.
     * Beköti az adatfogadási signalt és előkészíti az adat fogadást.
     * Ezt a slotot a konstruktor köti be.
     */
    void newConnection();

    /** A kapcsolat lezárult. */
    void disconnected();
};

#endif // COMMUNICATIONTCPSOCKETSERVER_H
