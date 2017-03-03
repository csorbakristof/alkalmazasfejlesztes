#include <QCoreApplication>
#include <iostream>
#include "DemoWebRequest.h"
#include "DemoQuit.h"
using namespace std;

int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);
    DemoWebRequest request;

    // Ha a demó véget ér, a DemoQuit::Quit slotot meghívva kérjük a program leállását.
    DemoQuit quit;
    QObject::connect(&request, SIGNAL(DemoFinished()), &quit, SLOT(Quit()));
    // Kérés aszinkron elküldése és belépés a végtelen eseménykezelő ciklusba.
    request.SendDemoRequest();

    return a.exec();
}
