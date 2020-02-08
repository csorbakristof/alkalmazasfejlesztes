#pragma once
#ifndef TCPSOCKETCOMMUNICATION_H
#define TCPSOCKETCOMMUNICATION_H
#include <QTcpSocket>
#include "Communication.h"

/**
 * @brief TCP socket alapú kommunikációra kialakított leszármazottja az általános
 * Communication osztálynak.
 *
 * A leszármazott osztályain keresztül érdemes használni.
 * Megjegyzés: a leszármaztatott osztályok használjál a setSocket metódust
 * az örökölt belső socket csatlakoztatására. */
class CommunicationTcpSocket : public Communication
{
    Q_OBJECT

public:
    /** Konstruktor */
    CommunicationTcpSocket();
    virtual ~CommunicationTcpSocket() = default;

    /**
     * @brief Beállítja a használt socketet.
     * @param newSocket Az új socket, amihez az adat fogadást csatlakoztatni kell.
     *  nullptr esetén leválasztja a signalokat a belső socketről.
     */
    void setSocket(QTcpSocket *newSocket);

    /** Visszaadja, hogy van-e kapcsolat. */
    virtual bool isConnected() const override;

    /** Kapcsoat bontása. */
    void disconnect();

protected:
    /** A belső küldési buffer tartalmát elküldi. Akkor kell meghívni, miután az üzenet bekerült a bufferbe. */
    virtual void sendBufferContent() override;

private:
    /** Ha van felépült kapcsolat, arra mutató pointer. Egyébként nullptr. */
    QTcpSocket *socket;

private slots:
    /** Továbbítja a hibajelzést. */
    void handleError(QAbstractSocket::SocketError socketError);
};

#endif // TCPSOCKETCOMMUNICATION_H
