#include <QString>
#include <QtTest>
#include "Simulator.h"

class QTestTheSimulatorTest : public QObject
{
    Q_OBJECT

public:
    QTestTheSimulatorTest();

private Q_SLOTS:
    void testInstantiation();
};

QTestTheSimulatorTest::QTestTheSimulatorTest()
{
}

void QTestTheSimulatorTest::testInstantiation()
{
    auto s = new Simulator(0);
    QVERIFY2(true, "Failure");
}

QTEST_APPLESS_MAIN(QTestTheSimulatorTest)

#include "tst_qtestthesimulatortest.moc"
