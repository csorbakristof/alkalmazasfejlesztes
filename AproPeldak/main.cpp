#include <QtGlobal>
#include <iostream>
#include <memory>
#include <QPoint>
#include "SignalPelda.h"

using namespace std;

void UniquePtrPelda()
{
    QPoint *egyik = new QPoint(2,3);
    std::unique_ptr<QPoint> masik =
        std::make_unique<QPoint>(4,5);
    auto harmadik = std::make_unique<QPoint>(6,7);
    std::cout << egyik->rx() << endl;
    cout << masik->rx() << endl;
    cout << harmadik->rx() << endl;
    delete egyik;
}

void signalPelda()
{
    SignalPelda sp;
    QObject::connect(&sp,SIGNAL(jelzesSignal(int)),
                     &sp, SLOT(jelzesSlot(int)));
    //QObject::connect(&sp,&SignalPelda::jelzesSignal,
    //                 &sp, &SignalPelda::jelzesSlot);
    sp.start();
    QObject::disconnect(&sp,SIGNAL(jelzesSignal(int)),
                        &sp, SLOT(jelzesSlot(int)));
}

int main(int argc, char *argv[])
{
    Q_UNUSED(argc);
    Q_UNUSED(argv);
    qDebug() << "UniquePtr példa:";
    UniquePtrPelda();
    qDebug() << "Signal példa:";
    signalPelda();
    return 0;
}
