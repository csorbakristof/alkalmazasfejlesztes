#pragma once
#ifndef COMMUNICATIONTCPSOCKETCLIENT_H
#define COMMUNICATIONTCPSOCKETCLIENT_H
#include "CommunicationTcpSocket.h"

/** @brief QTcpSocket alapú kommunikáció kliens oldala. */
class CommunicationTcpSocketClient : public CommunicationTcpSocket
{
public:
    /** Konstruktor */
    CommunicationTcpSocketClient();

    virtual ~CommunicationTcpSocketClient() = default;

    /** Kapcsolat felépítése. */
    void connect(QString url, int port);

private:
    /** A belső QTcpSocket példány. */
    QTcpSocket socket;

};

#endif // COMMUNICATIONTCPSOCKETCLIENT_H
