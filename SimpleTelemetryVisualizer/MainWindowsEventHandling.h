#ifndef MAINWINDOWSEVENTHANDLING_H
#define MAINWINDOWSEVENTHANDLING_H
#include <QObject>
#include <QDebug>

class MainWindowsEventHandling : public QObject
{
    Q_OBJECT

public:
    MainWindowsEventHandling();

    ~MainWindowsEventHandling() = default;

public slots:
    void connectRobot(const QString &msg)
    {
        qDebug() << "Called the C++ slot connectRobot with message:" << msg;
    }

    void disconnectRobot(const QString &msg)
    {
        qDebug() << "Called the C++ slot disconnectRobot with message:" << msg;
    }

    void startRobot(const QString &msg)
    {
        qDebug() << "Called the C++ slot startRobot with message:" << msg;
    }

    stopRobot(const QString &msg)
    {
        qDebug() << "Called the C++ slot stopRobot with message:" << msg;
    }
};

#endif // MAINWINDOWSEVENTHANDLING_H
