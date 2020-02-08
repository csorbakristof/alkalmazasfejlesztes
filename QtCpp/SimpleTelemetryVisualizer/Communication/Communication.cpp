#include <QTimer>
#include "Communication.h"

Communication::Communication()
    : receiveStream(nullptr), currentMessageSize(0)
{
}

Communication::~Communication()
{
    if (receiveStream != nullptr)
    {
        // (Ha volt korábbi érték, az most megszűnik a unique_ptr miatt.)
        receiveStream = nullptr;
    }
}

void Communication::connectToDevice(QIODevice *device)
{
    // A fogadási adatfolyam csatlakozatotása az eszközhöz (pl. sockethez)
    // (Ha volt korábbi, az most megszűnik a unique_ptr miatt.)
    receiveStream = std::make_unique<QDataStream>(device);
}

std::unique_ptr<QDataStream> Communication::getSendStream()
{
    return std::unique_ptr<QDataStream>(new QDataStream(&sendBuffer, QIODevice::WriteOnly));
}

QDataStream *Communication::getReceiveStream()
{
    return receiveStream.get();
}

void Communication::dataReceived()
{
    // Addig olvasunk, amíg nem jön meg egy egész üzenet.
    //  Utána kiadunk egy Communication::dataReady signalt.
    QDataStream &inStream = *getReceiveStream();
    QIODevice *socket = inStream.device();

    if (currentMessageSize == 0) {
        // Új üzenet kezdődik
        // Még nem tudjuk a csomag méretét
        if (socket->bytesAvailable() < (int) sizeof(qint32)) {
            // Még a csomag mérete sem jött meg.
            return;
        }

        // Üzenet hosszának beolvasása
        inStream >> currentMessageSize;
    }

    // Már tudjuk a csomag méretét.

    if (socket->bytesAvailable() < (int) (currentMessageSize - sizeof(qint32))) {
        // Nem jött még meg az egész csomag.
        return;
    }

    /* Jelezzük, hogy van új adat. Amit átadunk, az az id és méret utáni adattartalom.
     * Tömb esetében a QVector úgy szerializálja ki magát, hogy abban benne van a méret is. */
    emit dataReady(inStream);

    // Lehet, hogy még egy következő üzenet elejét is megkaptuk.
    currentMessageSize = 0;
    if (socket->bytesAvailable() > 0) {
        /* A QTimer-t használva még egyszer belelövünk
         * ebbe a slotba, hogy feldolgozza a maradék fogadott bytokat is. */
        QTimer::singleShot(0, this, SLOT(dataReceived()));
    }
}
