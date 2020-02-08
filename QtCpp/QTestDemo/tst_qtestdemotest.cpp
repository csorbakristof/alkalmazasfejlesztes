#include <QString>
#include <QtTest>

// A tömörség kedvéért most itt definiáljuk
//  a tesztelendő osztályt.
class Calculator
{
public:
    int add(int a, int b)
    {
        return a+b;
    }
};

// Ez itt a unit test osztályunk, ami a teszteket tartalmazza.
// A keretprogramot a Qt Creator autmatikusan generálja.
// A teszt egy sima osztály. Annyi extra van benne, hogy bizonyos
//  metódusait a unit teszt keretrendszer lefuttatja.
class QTestDemoTest : public QObject
{
    Q_OBJECT

public:
    QTestDemoTest();

// Most jönnek azok a metódusok, amik a teszteket képviselik.
private Q_SLOTS:
    void testCase1();
    void testCase2();

private:
    // Felveszünk egy példányt, amit tesztelni fogunk.
    Calculator calculator;
};

QTestDemoTest::QTestDemoTest()
    : calculator()
{
}

void QTestDemoTest::testCase1()
{
    // A QVERIFY2 jelzi az esetleges hibákat a teszt rendszernek,
    //  így ennek segítségével ellenőrizzük a funkciókról,
    //  hogy helyesen működnek-e.
    QVERIFY2(calculator.add(1,2) == 3, "Az 1+2 még nem megy. :(");
}

void QTestDemoTest::testCase2()
{
    // Ezt most direkt nem fog sikerülni.
    QVERIFY2(calculator.add(1,1) == 3, "A hibas unit teszt nem sikerult. :)");
}

// Itt most nem készítünk main() függvényt, azt ez a makró
//  legenerálja nekünk. Lefuttatja majd a tesztet és az eredményt
//  kiírja a konzolra.
QTEST_APPLESS_MAIN(QTestDemoTest)

#include "tst_qtestdemotest.moc"
