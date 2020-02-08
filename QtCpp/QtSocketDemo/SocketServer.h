#pragma once
#ifndef SOCKETSERVER_H
#define SOCKETSERVER_H
#include <QTcpServer>
#include <QDataStream>
#include <memory>

// Ez kell a currentConnectionSocket deklarálásához. Viszont az csak pointer,
//  így felesleges lenne beincludolni, mert nem kell tudni a tartalmát (méretét).
class QTcpSocket;

/**
 * @brief A szerver oldali kommunikáció osztálya.
 * A serverSocket köré épül. Ha új kapcsolat jön, a newConnection slot kapja meg
 * a sockettől a jelzést. Ekkor elmenti a currentConnectionSocket-be a socketet
 * és beköti a handleError és dataReceived slotokat.
 *
 * A dataReady signal jelzi, ha egy teljes üzenet beérkezett.
 *
 * A szerver és a kliens a már felépített kommunikáció használatában megegyezik.
 * Itt didaktikai okokból szándékosan duplázott a forráskód.
 */
class SocketServer : public QObject
{
    // Ez kell ahhoz, hogy rendes QObjectünk legyen és a slotok működjenek.
    Q_OBJECT

public:
    /** Konstruktor */
    SocketServer();
    ~SocketServer() = default;

    /** Elindítja a szervert és a megadott porton fog figyelni. */
    void start(unsigned int port);

    /** Üzenet küldése. */
    void send(QString text);

private:
    /** A szerver oldali kommunikáció mögött álló QTcpServer socket. */
    QTcpServer serverSocket;

    /** Az éppen nyitott kapcsolat sockete. */
    QTcpSocket *currentConnectionSocket = nullptr;

    /** Az adatfogadáshoz használt stream. */
    std::unique_ptr<QDataStream> receiveStream;

    /** Az éppen fogadott üzenet mérete. */
    qint32 currentMessageSize;

signals:
    /** Ez jelzi, ha egy egész üzenet beérkezett. A fogadó oldalnak
     * az egész üzenetet ki kell olvasnia a streamből. */
    void dataReady(QDataStream&);

private slots:
    /** A szerver socket jelzi, hogy új kapcsolat érkezett. */
    void newConnection();

    /** A kapcsolat lezárását jelzi. */
    void disconnected();

    /** A socket hibát jelez. */
    void handleError(QAbstractSocket::SocketError socketError);

    /** Adat érkezett. Nem biztos, hogy egy teljes üzenetnyi. */
    void dataReceived();
};

#endif // SOCKETSERVER_H
