#pragma once
#ifndef MAINWINDOWCPPSIDE_H
#define MAINWINDOWCPPSIDE_H
#include <QObject>
#include <QQuickItem>

class MainWindowCppSide : public QObject
{
    Q_OBJECT

public:
    MainWindowCppSide(QObject *rootObject);
    ~MainWindowCppSide() = default;

public slots:
    /** Eseménykezelő a QML oldali addGreenEntry signalhoz. */
    void addGreenEntryHandler();
private:
    QQuickItem* findItemByName(const QString& name);
    QQuickItem* findItemByName(QObject *rootObject, const QString& name);
    QQuickItem* findItemByName(QList<QObject*> nodes, const QString& name);
    QQuickItem* mainWindowObject;
};

#endif // MAINWINDOWCPPSIDE_H
