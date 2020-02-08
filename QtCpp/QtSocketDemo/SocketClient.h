#pragma once
#ifndef SOCKETCLIENT_H
#define SOCKETCLIENT_H
#include <memory>
#include <QTcpSocket>
#include <QDataStream>

/**
 * @brief A kliens oldali kommunikációt beburkoló objektum.
 * A benne lévő socket objektum köré épül az összes funkció.
 *
 * A szerver és a kliens a már felépített kommunikáció használatában megegyezik.
 * Itt didaktikai okokból szándékosan duplázott a forráskód.
 */
class SocketClient : public QObject
{
    // Ez minden QObjectben kell, különben nem működnek például a slotok.
    Q_OBJECT

public:
    /** Konstruktor. */
    SocketClient();
    ~SocketClient() = default;

    /** Felépíti a kapcsolatot a szerverrel és a socket signaljait beköti
     * a handleError és dataReceived slotokra. Ezen kívül a fogadáshoz
     * szükséges receiveStream-et létrehozza és a sockethez köti. */
    void connect(QString url, int port);

    /** Szöveges üzenetet küld a socketen keresztül. */
    void send(QString text);

private:
    /** The underlying QTcpSocket instance. */
    QTcpSocket socket;

    /** Az adatfogadásra szolgáló stream. A connect() hozza létre
     * és köti a sockethez. */
    std::unique_ptr<QDataStream> receiveStream;

    /** Az éppen fogadott csomag mérete.
     * Azért kell, mert egy dataReceived hívással nem biztos, hogy
     * egy teljes üzenet jön át. */
    qint32 currentMessageSize;

signals:
    /** Akkor emittálódik, amikor egy teljes üzenet beérkezett.
     * Fontos, hogy a kezelő oldal ürítse is ki a QDataStreamet. */
    void dataReady(QDataStream&);

private slots:
    /** A socket hibák fogadására szolgál. */
    void handleError(QAbstractSocket::SocketError socketError);
    /** A socket adatfogadását jelzi. */
    void dataReceived();
};

#endif // SOCKETCLIENT_H
