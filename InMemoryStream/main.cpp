#include <QCoreApplication>
#include <QString>
#include <QBuffer>
#include <QDataStream>
#include <QDebug>

/**
 * We create a data stream in memory, write data to it, and then read it back.
 * This is useful for data structrure cloning using serialization and
 * deserialization without the need to write the data into a real file.
 */
int main(int argc, char *argv[])
{
    Q_UNUSED(argc);
    Q_UNUSED(argv);

    QByteArray buffer;
    QDataStream stream(&buffer, QIODevice::ReadWrite);

    QString text("Hello, this is the data.");

    stream << text;

    QDataStream readStream(&buffer, QIODevice::ReadOnly);
    QString readText;

    readStream >> readText;

    qDebug() << "Read: " << readText;

}
