#include <QString>
#include <QtTest>
#include "TestSimulator.h"
#include "CommunicationMock.h"

class QTestTheSimulatorTest : public QObject
{
    Q_OBJECT

public:
    QTestTheSimulatorTest();

private:
    CommunicationMock comm;
    TestSimulator sim;

private Q_SLOTS:
    void testPhysics();
    void testCommunication();
};

QTestTheSimulatorTest::QTestTheSimulatorTest()
    : comm(), sim(comm)
{
}

void QTestTheSimulatorTest::testPhysics()
{
    sim.reset(1.0);
    sim.SetStateA(1.0);
    QVERIFY2(0.0==sim.GetStateV(), "Kezdeti allohelyzet");
    sim.Tick();
    QVERIFY2(1.0==sim.GetStateV(), "Elindult");
}

void QTestTheSimulatorTest::testCommunication()
{
    sim.reset(1.0);
    QVERIFY2(0.0==sim.GetStateV(), "Kezdeti allohelyzet");
    QByteArray buffer;
    QDataStream stream(&buffer,QIODevice::ReadWrite);
    RobotState state(RobotState::Status::Accelerate, 0, 0.0, 0.0, 1.0, false);
    stream << state;
    stream.device()->seek(0);
    sim.DataReady(stream);
    sim.Tick();
    // A gyorsulasi parancsra a sebessegnek nonie kell.
    QVERIFY2(1.0==sim.GetStateV(), "Gyorsitasi parancsra elindul");
    QVERIFY2(comm.DidCommunicate, "A szimulator kezdemenyezett kommunikaciot");
}

QTEST_APPLESS_MAIN(QTestTheSimulatorTest)

#include "tst_qtestthesimulatortest.moc"
