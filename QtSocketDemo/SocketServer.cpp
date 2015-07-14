#include <QDebug>
#include <QTcpSocket>
#include <QDataStream>
#include <QTimer>
#include "SocketServer.h"

SocketServer::SocketServer()
    : QObject(nullptr), receiveStream(nullptr), currentMessageSize(0)
{
}

void SocketServer::start(unsigned int port)
{
    // A szerver socket megnyitja a portot.
    if (!serverSocket.listen(QHostAddress::Any, port))
    {
        qWarning() << "SocketServer::Start: Nem sikerült megnyitni a server socketet: ";
        qWarning() << serverSocket.errorString();
    }
    else
    {
        qWarning() << "SocketServer::Start: A server socket bejövő kapcsolatra vár...";
    }

    // Bekötjük a newConnection slotot.
    connect(&serverSocket, &QTcpServer::newConnection, this, &SocketServer::newConnection);
}

void SocketServer::newConnection()
{
    qDebug() << "SocketServer::newConnection: Bejövő kapcsolat...";
    // Elkérjük a server sockettől a kommunikációra használt socketet.
    QTcpSocket *newSocket = serverSocket.nextPendingConnection();
    if (newSocket)
    {
        // Bekötjük a disconnected slotot.
        connect(newSocket, &QTcpSocket::disconnected, this, &SocketServer::disconnected);

        if (currentConnectionSocket != nullptr && newSocket != currentConnectionSocket)
        {
            // Már volt egy kapcsolat, annak leválasztjuk a signaljait.
            qDebug() << "SocketServer::newConnection: Korábbi socket signaljainak leválasztása.";
            // Ősosztályok signaljainál nem mindig működik még az új connect szintaxis:
            //  https://codereview.qt-project.org/#/c/55606/
            QObject::disconnect(currentConnectionSocket, SIGNAL(error(QAbstractSocket::SocketError)), this, SLOT(CommunicationTcpSocket::handleError(QAbstractSocket::SocketError)));
            QObject::disconnect(currentConnectionSocket, SIGNAL(readyRead()), this, SLOT(dataReceived()));
        }
        qDebug() << "SocketServer::newConnection: Új socket signaljainak csatlakoztatása.";
        // Elmentjük a socketet
        currentConnectionSocket = newSocket;
        connect(currentConnectionSocket, SIGNAL(error(QAbstractSocket::SocketError)), this, SLOT(handleError(QAbstractSocket::SocketError)));
        connect(currentConnectionSocket, SIGNAL(readyRead()), this, SLOT(dataReceived()));

        // Létrehozzuk az adatfogadási streamet és a sockethez kötjük.
        receiveStream = std::make_unique<QDataStream>(currentConnectionSocket);

        qWarning() << "SocketServer::newConnection: Az új kapcsolat felépült.";
    }
}

void SocketServer::disconnected()
{
    qWarning() << "SocketServer::disconnected: Lezáródott a kapcsolat.";
}

void SocketServer::dataReceived()
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

void SocketServer::handleError(QAbstractSocket::SocketError socketError)
{
    qDebug() << "SocketServer::handleError: A socket hibát jelzett: " << socketError;
}

void SocketServer::send(QString text)
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
    qDebug() << "SocketServer::send(): " << sendBuffer.size() << " bájt:\n" << sendBuffer.toHex();
    currentConnectionSocket->write(sendBuffer);
    sendBuffer.clear();
}
