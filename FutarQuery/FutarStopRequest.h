#ifndef DEMOWEBREQUEST_H
#define DEMOWEBREQUEST_H
#include <QNetworkAccessManager>
#include <QNetworkRequest>
#include <QNetworkReply>
#include <iostream>
#include <QDebug>
#include <QCoreApplication>
#include <QJsonDocument>
#include <QJsonArray>
#include <QJsonObject>
#include <QStringList>
using namespace std;

/**
 * @brief Demó webes kérés küldése és a válasz fogadása
 */
class FutarStopRequest : public QObject
{
    Q_OBJECT

public:
    explicit FutarStopRequest(bool verbose=false)
        : QObject(nullptr), verbose(verbose)
    {
        manager = new QNetworkAccessManager(this);
        connect(manager, SIGNAL(finished(QNetworkReply*)),
                this, SLOT(replyFinished(QNetworkReply*)));
    }

    // Example stopID: "F02222"
    void SendRequest(QString stopID)
    {
        QUrl url("http://futar.bkk.hu/bkk-utvonaltervezo-api/ws/otp/api/where/arrivals-and-departures-for-stop.json?stopId=BKK_"
                 + stopID);

        QNetworkRequest request;
        request.setUrl(url);
        request.setRawHeader("accept-encoding", "identity");    // do not send gzipped json...

        QNetworkReply *reply = manager->get(request);
        connect(reply, SIGNAL(error(QNetworkReply::NetworkError)),
                this, SLOT(slotError(QNetworkReply::NetworkError)));
        connect(reply, SIGNAL(sslErrors(QList<QSslError>)),
                this, SLOT(slotSslErrors(QList<QSslError>)));
    }

signals:
    // Ezzel a signallal jelezzük a demó végét, a main() ezt a DemoQuit::Quit slothoz
    //  kötötte, ami az alkalmazás leállását eredményezi.
    void QueryFinished();

public slots:
    void replyFinished(QNetworkReply* reply)
    {
        // Megérkezett a teljes válasz. Kiírjuk a tartalom méretét és a HTTP header
        //  bejegyzéseit. Végül jelezzük a demó végét.
        QByteArray answer = reply->readAll();
        QList<QNetworkReply::RawHeaderPair> headerList = reply->rawHeaderPairs();
        for(auto h : headerList)
            cout << "  " << h.first.toStdString() << ": " << h.second.toStdString() << endl;
        cout << "----- RAW ANSWER -----" << endl;
        if (verbose)
            cout << answer.toStdString() << endl;

        QJsonDocument json = QJsonDocument::fromJson(answer);
        QJsonObject rootElement = json.object();
        cout << "----- ROOT OBJECT -----" << endl;
        if (verbose) ShowKeysOfJsonObject(rootElement);

        cout << "----- ROOT->DATA OBJECT -----" << endl;
        QJsonObject data = rootElement["data"].toObject();
        if (verbose) ShowKeysOfJsonObject(data);
        cout << "----- ROOT->DATA->ENTRY OBJECT -----" << endl;
        QJsonObject entry = data["entry"].toObject();
        if (verbose) ShowKeysOfJsonObject(entry);

        cout << "----- ROOT->DATA->ENTRY->STOPTIMES ARRAY -----" << endl;
        QJsonArray stopTimes = entry["stopTimes"].toArray();
        for(const QJsonValue& stopTime : stopTimes)
        {
            QJsonObject stopTimeObj = stopTime.toObject();
            if (verbose) ShowKeysOfJsonObject(stopTimeObj);
            double arrivalTime = stopTimeObj["arrivalTime"].toDouble();
            cout << "-- -- Arrival time: " << TimestampToString(arrivalTime).toStdString() << endl;
        }

        emit QueryFinished();
    }

    QString TimestampToString(double t)
    {
        QDateTime timestamp;
        timestamp.setTime_t(t);
        return timestamp.toString(Qt::SystemLocaleShortDate);
    }

    void ShowKeysOfJsonObject(const QJsonObject& jo)
    {
        QStringList	keys = jo.keys();
        for(QString k : keys)
        {
            cout << "-- key: " << k.toStdString() << " = ";
            QJsonValue value = jo[k];
            ShowValueWithType(value);
        }
    }

    void ShowValueWithType(const QJsonValue& v)
    {
        if (v.isDouble())
        {
            double d = v.toDouble();
            cout << "DOUBLE (" << d << ")" << endl;
        } else if (v.isString())
        {
            QString s = v.toString();
            cout << "STRING (" << s.toStdString() << ")" << endl;
        } else if (v.isArray())
        {
            cout << "ARRAY" << endl;
        } else if (v.isBool())
        {
            bool b = v.toBool();
            cout << "BOOL (" << (b?"1":"0") << ")" << endl;
        } else if (v.isNull())
        {
            cout << "NULL" << endl;
        } else if (v.isObject())
        {
            cout << "OBJECT" << endl;
        } else if (v.isUndefined())
        {
            cout << "UNDEFINED" << endl;
        } else
        {
            cout << "UNKNOWN" << endl;
        }
    }

    void slotError(QNetworkReply::NetworkError)
    {
        cout << "- Kommunikacios hiba. :(" << endl;
        emit QueryFinished();
    }

    void slotSslErrors(QList<QSslError>)
    {
        cout << "- SSL hiba. :(" << endl;
        emit QueryFinished();
    }

private:
    QNetworkAccessManager *manager = nullptr;
    bool verbose;
};

#endif // DEMOWEBREQUEST_H
