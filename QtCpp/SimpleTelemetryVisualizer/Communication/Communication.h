#pragma once
#ifndef COMMUNICATION_H
#define COMMUNICATION_H
#include <memory>
#include <QDataStream>
#include <QDateTime>
#include <QAbstractSocket>

/**
 * @brief Stream alapú kommunikációs ősosztály.
 *
 * Példányosítás után a connectToDevice metódussal lehet a belső
 * adatfogadási streamet az eszközhöz csatlakoztatni.
*/
class Communication : public QObject
{
    Q_OBJECT

public:
    /** Konstruktor */
    Communication();
    virtual ~Communication();

    /** Csatlakozás egy eszközhöz (Pl. QTcpSocket), amit használni fog. */
    void connectToDevice(QIODevice *device);

    /** Visszaadja, hogy van-e nyitott kapcsolat. */
    virtual bool isConnected() const = 0;

    /** Visszaad egy pointert az adatfogadási folyamra. */
    QDataStream *getReceiveStream();

    /** Egy objektum tartalmát elküldi: kiírja a sendBuffer-be, majd meghívja
     * a sendBufferContent() metódust.
     *
     * Ezzel a metódussal minden olyan objektumot el lehet küldeni, amit az operator<<
     * segítségével bele lehet írni egy QDataStreambe, vagyis értelmezett rá ez a művelet.
     * Megjegyzés: így nem kell egy ősosztály, amiből az elküldhető osztályoknak származnia kell.
     */
    template<typename T>
    void send(const T& toSendObject)
    {
        auto stream = getSendStream();

        // Elmentjük a jelenlegi stream pozíciót
        const qint64 startPos = stream->device()->size();
        qint32 msgSize = 0;
        // Ideiglenesen az elejére méretnek 0-t írunk. Majd
        //  ha már le tudjuk mérni az üzenet hosszát, visszajövünk ide és
        //  beírjuk a tényleges értéket.
        *stream << msgSize;
        // A tényleges adattartalom sorosítása
        *stream << toSendObject;
        const qint64 endPos = stream->device()->size();

        // Visszaugrunk és beírjuk a helyes üzenet méretet.
        stream->device()->seek(startPos);
        msgSize = endPos - startPos;
        *stream << msgSize;
        // Visszaugrunk az üzenet végére
        stream->device()->seek(endPos);

        // Ténylegesen elküldjük az üzenetet.
        //  (Ez absztrakt metódus, majd minden protokoll implementálja, ahogy kell.)
        sendBufferContent();
    }

signals:
    /** Hibajelzés. */
    // Ezt majd minden protokoll megfelelően beköti.
    void errorOccurred(const QString&);

    /** Egy teljes üzenet megérkezett. */
    void dataReady(QDataStream& stream);

protected:
    /** Az adat fogadási stream. A connectToDevice() állítja be. */
    std::unique_ptr<QDataStream> receiveStream;

    /** Tényleges küldés előtt ide kerül be minden küldendő adat.
     * A getSendStream() metódussal lehet elérni. */
    QByteArray sendBuffer;

    /** Visszaad egy streamet, ami a tényleges adatküldéshez a sendBuffer-be beírja a
     * küldendő adatokat. Utána már csaa a sendBufferContent() metódust kell meghívni.
     * Kívülről a send() metódust kell használni, az tovább hív ide. */
    // TODO: can we send correctly if the stream is already distroyed when calling send() ?!
    std::unique_ptr<QDataStream> getSendStream();

    /** Ténylegesen elküldi a sendBuffer tartalmát. A leszármazott osztályoknak a
     * protokolljuknak megfelelően kell implementálniuk. */
    virtual void sendBufferContent() = 0;

private:
    /** Az éppen fogadott üzenet mérete. A dataReceived() használja. */
    qint32 currentMessageSize;

protected slots:
    /** Adat érkezett, de nem feltétlenül egy egész üzenet.
     * A leszármazott osztályok ezen kereszül jelzik, hogy van új adat.
     */
    void dataReceived();
};

#endif // COMMUNICATION_H
