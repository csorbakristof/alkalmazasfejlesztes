#ifndef DEMOWEBREQUEST_H
#define DEMOWEBREQUEST_H
#include <QNetworkAccessManager>
#include <QNetworkRequest>
#include <QNetworkReply>
#include <iostream>
#include <QDebug>
#include <QCoreApplication>
using namespace std;

/**
 * @brief Demó webes kérés küldése és a válasz fogadása
 */
class DemoWebRequest : public QObject
{
    Q_OBJECT

public:
    explicit DemoWebRequest()
        : QObject(nullptr)
    {
        // Magas szinten kell egy manager. Ezt lehet megkérni
        //  kérések elküldésére, a válasz teljes beérkezését pedig
        //  egy signallal jelzi, amit itt egyből bekötünk.
        manager = new QNetworkAccessManager(this);
        connect(manager, SIGNAL(finished(QNetworkReply*)),
                this, SLOT(replyFinished(QNetworkReply*)));
    }

    void SendDemoRequest()
    {
        // A demó kérés elküldéséhez összeállítjuk magát a kérést
        //  (egy HTTP header sor beállításával kiegészítve).
        QNetworkRequest request;
        request.setUrl(QUrl("http://www.aut.bme.hu"));
        request.setRawHeader("User-Agent", "DemoBrowser 1.0");

        // A kérés elküldése után megkapjuk a választ reprezentáló osztályt,
        //  ami aszinkron módon signalok segítségével fogja jelezni, ha
        //  történt valami.
        cout << "- HTTP keres kuldese..." << endl;
        QNetworkReply *reply = manager->get(request);
        connect(reply, SIGNAL(readyRead()), this, SLOT(slotReadyRead()));
        connect(reply, SIGNAL(error(QNetworkReply::NetworkError)),
                this, SLOT(slotError(QNetworkReply::NetworkError)));
        connect(reply, SIGNAL(sslErrors(QList<QSslError>)),
                this, SLOT(slotSslErrors(QList<QSslError>)));
    }

signals:
    // Ezzel a signallal jelezzük a demó végét, a main() ezt a DemoQuit::Quit slothoz
    //  kötötte, ami az alkalmazás leállását eredményezi.
    void DemoFinished();

public slots:
    void replyFinished(QNetworkReply* reply)
    {
        // Megérkezett a teljes válasz. Kiírjuk a tartalom méretét és a HTTP header
        //  bejegyzéseit. Végül jelezzük a demó végét.
        cout << "- Megerkezett a valasz." << endl;
        QByteArray answer = reply->readAll();
        cout << "- Tartalom merete: " << answer.size() << endl;
        cout << "- HTTP header bejegyzesek: " << endl;
        QList<QNetworkReply::RawHeaderPair> headerList = reply->rawHeaderPairs();
        for(auto h : headerList)
            cout << "  " << h.first.toStdString() << ": " << h.second.toStdString() << endl;

        emit DemoFinished();
    }

    void slotReadyRead()
    {
        // Ezt a reply objektum hívja akkor, amikor adatokat kap a socketen keresztül.
        //  A replyFinished mellett igazából nem szükséges figyelni...
        cout << "- Adat erkezett..." << endl;
    }

    void slotError(QNetworkReply::NetworkError)
    {
        cout << "- Kommunikacios hiba. :(" << endl;
        emit DemoFinished();
    }

    void slotSslErrors(QList<QSslError>)
    {
        cout << "- SSL hiba. :(" << endl;
        emit DemoFinished();
    }

private:
    QNetworkAccessManager *manager = nullptr;
};

#endif // DEMOWEBREQUEST_H
