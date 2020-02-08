#pragma once
#ifndef SIMULATOR_H
#define SIMULATOR_H
#include <QObject>

/** Egy járművet szimulál, ami halad adott sebességgel. */
class Simulator : public QObject
{
    Q_OBJECT

public:
    Simulator(int velocity);
    ~Simulator() = default;

signals:
    /** Jelzi, hogy megérkeztünk egy szép helyre. */
    void arrived(int where);

public slots:
    /** Telik az idő... */
    void tick();

private:
    int x;  /** pozíció */
    int v;  /** sebesség */
};

#endif // SIMULATOR_H
