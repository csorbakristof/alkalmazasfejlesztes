#include <QCoreApplication>
#include <iostream>
#include <thread>
#include <chrono>
#include "Framework/Robot.h"
#include "Framework/StateStore.h"

using namespace std;
using namespace std::literals;

/*
Ez a példaprogram egy robotot szimulál, mely különböző külső hatásokra
(detektoroktól érkező eseményekre) reagál. Minden eseményt az aktuális állapotát
leíró (State-ből származó) objektumnak továbbítja, mely ennek megfelelően
módosíthatja a robot paramétereit (referencia sebesség, manuális vagy automatikus
kormányzás). Ezen kívül az állapot objektumok a Tick() eseményeken kereszül az idő
múlásával is tisztában vannak. Ez lehetővé teszi, hogy például a gyorsulási szakaszban
a "Fast" állapot amikor észreveszi a gyorsulási szakasz végét, még várjon egy kicsit,
mielőtt elkezd fékezni, vagyis állapotváltást kér a robottól.

Minden állapot rendelkezik OnEnteringState és OnLeavingState eseményekkel is, így
bármely állapotból is érkezünk például a Fast állapotba, a belépéskor az allapot
be tudja állítani a nagyobb referencia sebességet.

Mivel minden állapot objektumból csak egy kell, ezeket a singlaton StateStore
példányosítja, tárolja, és bárhonnan el lehet kérni tőle bármely állapotot.

A tesztelés érdekében a főprogram a SimulateEvents függvény segítségével
szimulálja az időzített eseményeket a robot számára.

(A gyorsabb áttekinthetőség kedvéért számos osztályt csak header fájlokban
implementáltam, hogy a forráskódot bemutatva ne kelljen folyton ugrálni a .h
és .cpp fájlok között.)
*/

void SimulateEvents(Robot& robot, int t)
{
    if (t==3)
    {
        cout << "Simulating event: AccelerationStartMarkDetected" << endl;
        robot.AccelerationStartMarkDetected();
    }
    else if(t==6)
    {
        cout << "Simulating event: LineLost (direction of last known location is -2)" << endl;
        robot.LineLost(-2);
    }
    else if(t==9)
    {
        cout << "Simulating event: LineFound" << endl;
        robot.LineFound();
    }
    else if(t==17)
    {
        cout << "Simulating event: AccelerationEndMarkDetected" << endl;
        // Itt a Default allapotban lesz, mivel egyszer elvesztette a vonalat...
        robot.AccelerationEndMarkDetected();
    }
    else if(t==20)
    {
        cout << "Simulating event: AccelerationStartMarkDetected" << endl;
        robot.AccelerationStartMarkDetected();
    }
    else if(t==25)
    {
        cout << "Simulating event: AccelerationEndMarkDetected" << endl;
        robot.AccelerationEndMarkDetected();
    }
    else if(t==26)
    {
        cout << "(There will be no further simulated events...)" << endl;
    }
}

int main()
{
    Robot robot;
    StateStore::Instance.Init(robot);

    State *defaultState = StateStore::Instance.GetState(StateStore::States::Default);
    robot.SetState(defaultState);

    int timeCounter = 0;
    while(timeCounter < 30)
    {
        cout << "--- tick (t=" << timeCounter << ") ---" << endl;
        robot.Tick();
        std::this_thread::sleep_for(1s);
        timeCounter++;

        SimulateEvents(robot, timeCounter);
    }
}
