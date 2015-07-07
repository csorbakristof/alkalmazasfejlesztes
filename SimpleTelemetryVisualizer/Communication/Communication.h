#pragma once
#ifndef COMMUNICATION_H
#define COMMUNICATION_H
#include <memory>
#include <QDataStream>
#include <QDateTime>
#include <QAbstractSocket>

/** Stream based communication functions.
 * After creation, use connectToDevice to connect the receiving dats stream.
 * (You may call connectToDevice multiple times.)
*/
class Communication : public QObject
{
    Q_OBJECT

public:
    /** Constructor */
    Communication();
    /** Destructor */
    ~Communication();

    /** Connects to a device. It may be a QTcpSocket for example. */
    void connectToDevice(QIODevice *device);

    /** Returns connection status. */
    virtual bool isConnected() const = 0;

    /** Returns the stream used to receive data. */
    QDataStream *getReceiveStream();

    /** Writes the object content to the sendBuffer
     * and calls sendBufferContent().
     *
     * This method enables sending any class that has operator<< for QDataStream.
     * This way, no base class for the valid parameters is required.
     */
    template<typename T>
    void send(const T& toSendObject)
    {
        // TODO: check connection status
        auto stream = getSendStream();

        // Store start position in stream to calculate message size
        const qint64 startPos = stream->device()->size();
        qint32 msgSize = 0;
        // Temporally, write 0 message size.
        // We will come back here and set the correct value
        //  after serialization is complete.
        *stream << msgSize;
        // Send ID, timestamp and value
        *stream << toSendObject;
        const qint64 endPos = stream->device()->size();

        // Jump back to the beginning and write the correct message size.
        stream->device()->seek(startPos);
        msgSize = endPos - startPos;
        *stream << msgSize;
        // Jump to the end of the serialized data stream.
        stream->device()->seek(endPos);

        // Send the data (w.r.t. the used protocol)
        sendBufferContent();
    }

signals:
    /** Indicates an error in the communication. */
    // A socket a handleError-on kereszül emittálja.
    void errorOccurred(const QString&);

    /** A whole message has been received. */
    void dataReady(QDataStream& stream);

protected:
    /** Reception stream initialized in connectToDevice. */
    QDataStream *receiveStream;

    /** Send buffer accessed via currentSendStream. Use getSendStream to access it. */
    QByteArray sendBuffer;

    /** Returns a stream. Data written to it will be sent by send().
     * Use sendValue to send data and commands. This is only used by sendValue internally. */
    // TODO: can we send correctly if the stream is already distroyed when calling send() ?!
    std::unique_ptr<QDataStream> getSendStream();

    /** Really send all data serialized into sendBuffer earlier. Derived classes
     * have to override this to send the data w.r.t. the protocol. */
    virtual void sendBufferContent() = 0;

private:
    /** Size of message currently under reception. Used by dataReceived. */
    qint32 currentMessageSize;

private slots:
    /** Data received, not necessarily a whole message yet.
     * Should be connected in derived classes like CommunicationTcpSocket.
     */
    void dataReceived();
};

#endif // COMMUNICATION_H
