#pragma once
#ifndef SIGNALPELDA_H
#define SIGNALPELDA_H
#include <QObject>
#include <QDebug>

class SignalPelda : public QObject
{
    Q_OBJECT
public:
    SignalPelda();
    ~SignalPelda();

    void start()
    {
        emit jelzesSignal(5);
    }

signals:
    void jelzesSignal(int szam);

public slots:
    void jelzesSlot(int szam)
    {
        qDebug() << "Jelzés jött:" << szam << endl;
    }
};

#endif // SIGNALPELDA_H
