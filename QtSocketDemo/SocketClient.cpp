#include <QObject>
#include <QDebug>
#include <QTimer>
#include <memory>
#include "SocketClient.h"

SocketClient::SocketClient()
    : QObject(nullptr), currentMessageSize(0)
{
}

void SocketClient::connect(QString url, int port)
{
    qDebug() << "SocketClient::connect: Csatlakozás a szerverhez...";
    socket.connectToHost(url, port, QIODevice::ReadWrite);

    qDebug() << "SocketClient::connect: Socket signaljainak csatlakoztatása.";
    QObject::connect(&socket, SIGNAL(error(QAbstractSocket::SocketError)), this, SLOT(handleError(QAbstractSocket::SocketError)));
    QObject::connect(&socket, SIGNAL(readyRead()), this, SLOT(dataReceived()));

    receiveStream = std::make_unique<QDataStream>(&socket);

    qWarning() << "SocketClient::connect: Az új kapcsolat felépült.";
}

void SocketClient::handleError(QAbstractSocket::SocketError socketError)
{
    qWarning() << "SocketClient::handleError: A socket hibát jelzett: " << socketError;
}

void SocketClient::dataReceived()
{
    qDebug() << "SocketClient::dataReceived: új adat érkezett";

    QDataStream &inStream = *receiveStream;
    QIODevice *socket = inStream.device();

    if (currentMessageSize == 0) {
        // Teljesen új adatcsomag érkezett, először a mérete jön át.
        if (socket->bytesAvailable() < (int) sizeof(qint32)) {
            // Még nem jött át annyi byte, amennyi a méret mérete.
            return;
        }

        // A csomag méretének beolvasása.
        inStream >> currentMessageSize;
    }
    // Itt már tudjuk a csomag méretét.

    if (socket->bytesAvailable() < (int) (currentMessageSize - sizeof(qint32))) {
        // Még nem jött meg az egész csomag.
        return;
    }

    // Jelezzük, hogy van új adat. Amit átadunk, az a méret utáni adattartalom.
    emit dataReady(inStream);

    // Lehet, hogy több adat is jött, és akkor egy következő üzenet elejét
    //  is megkaptuk.
    currentMessageSize = 0;
    if (socket->bytesAvailable() > 0) {
        // A QTimer-t használva még egyszer belelövünk
        //  ebbe a slotba, hogy feldolgozza a maradék fogadott bytokat is.
        QTimer::singleShot(0, this, SLOT(dataReceived()));
    }
}

void SocketClient::send(QString text)
{
    // Az adatküldésre használt buffer.
    // Ez csak a küldéshez kell, így itt hozzuk létre.
    QByteArray sendBuffer;

    // Létrehozunk egy streamet, amivel tudunk írni a bufferbe.
    // Streamet nem olyan egyszerű létrehozni, mivel pl. nem lehet őket másolni.
    auto stream = std::unique_ptr<QDataStream>(new QDataStream(&sendBuffer, QIODevice::WriteOnly));

    // Az üzenet eleje a méretet tartalmazza, amit meg is kell határozni.
    // Elmentjük, hol állunk most a streamben.
    const qint64 startPos = stream->device()->size();
    // Kihagyjuk a méret információ helyét.
    // Ha majd már tudjuk, visszajövünk és beírjuk ide.
    qint32 msgSize = 0;
    *stream << msgSize;

    // Most kiírjuk a streambe a tényleges üzenetet.
    *stream << text;

    // Elmentjük, hogy most hol állunk.
    const qint64 endPos = stream->device()->size();

    // Visszaugrunk a méret helyére és beírjuk.
    stream->device()->seek(startPos);
    msgSize = endPos - startPos;
    *stream << msgSize;

    // Visszaugrunk a stream végére.
    stream->device()->seek(endPos);

    // Ténylegesen elküldjük a buffer tartalmát, ahova a stream írt.
    qDebug() << "SocketClient::send(): " << sendBuffer.size() << " bájt:\n" << sendBuffer.toHex();
    socket.write(sendBuffer);
    sendBuffer.clear();
}
