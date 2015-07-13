#include <QObject>
#include <QDebug>
#include <QTimer>
#include <memory>
#include "SocketClient.h"

SocketClient::SocketClient()
    : currentMessageSize(0)
{

}

void SocketClient::connect(QString url, int port)
{
    qWarning() << "SocketClient::connect: Csatlakozás a szerverhez...";
    socket.connectToHost(url, port, QIODevice::ReadWrite);

    qDebug() << "SocketClient::connect: Socket signaljainak csatlakoztatása.";
    QObject::connect(&socket, SIGNAL(error(QAbstractSocket::SocketError)), this, SLOT(handleError(QAbstractSocket::SocketError)));
    QObject::connect(&socket, SIGNAL(readyRead()), this, SLOT(dataReceived()));

    receiveStream = std::make_unique<QDataStream>(&socket);

    qWarning() << "SocketClient::connect: Az új kapcsolat felépült.";
}

void SocketClient::handleError(QAbstractSocket::SocketError socketError)
{
    Q_UNUSED(socketError)
    qDebug() << "SocketClient::handleError: A socket hibát jelzett: " << socketError;
}

void SocketClient::dataReceived()
{
    qDebug() << "SocketClient::dataReceived: új adat érkezett";
    // Read as long as a whole message is received. After that, emit Communication::dataReady signal.
    QDataStream &inStream = *receiveStream;
    QIODevice *socket = inStream.device();

    // It's a new block
    if (currentMessageSize == 0) {
        /* Még nem tudjuk a csomag méretét... */
        // There's not enough bytes arrived to determine the size
        if (socket->bytesAvailable() < (int) sizeof(qint32)) {
            /* Még a csomag mérete sem jött meg. */
            return;
        }

        // Computing blockSize
        inStream >> currentMessageSize;
    }
    /* Már tudjuk a csomag méretét. */

    if (socket->bytesAvailable() < (int) (currentMessageSize - sizeof(qint32))) {
        /* Nem jött még meg az egész csomag. */
        return;
    }

    /* Jelezzük, hogy van új adat. Amit átadunk, az az id és méret utáni adattartalom.
     * Tömb esetében a QVector úgy szerializálja ki magát, hogy abban benne van a méret is. */
    emit dataReady(inStream);

    // Maybe we got the first bytes of a next packet
    currentMessageSize = 0;
    if (socket->bytesAvailable() > 0) {
        /* A QTimer-t használva még egyszer belelövünk
         * ebbe a slotba, hogy feldolgozza a maradék fogadott bytokat is. */
        QTimer::singleShot(0, this, SLOT(dataReceived()));
    }
}

void SocketClient::send(QString text)
{
    auto stream = std::unique_ptr<QDataStream>(new QDataStream(&sendBuffer, QIODevice::WriteOnly));

    // Store start position in stream to calculate message size
    const qint64 startPos = stream->device()->size();
    qint32 msgSize = 0;
    // Temporally, write 0 message size.
    // We will come back here and set the correct value
    //  after serialization is complete.
    *stream << msgSize;

    *stream << text;
    const qint64 endPos = stream->device()->size();

    // Jump back to the beginning and write the correct message size.
    stream->device()->seek(startPos);
    msgSize = endPos - startPos;
    *stream << msgSize;
    // Jump to the end of the serialized data stream.
    stream->device()->seek(endPos);

    // Send the data (w.r.t. the used protocol)

    qDebug() << "CommunicationTcpSocket::send() " << sendBuffer.size() << " bytes:\n" << sendBuffer.toHex();
    socket.write(sendBuffer);
    sendBuffer.clear();
}
