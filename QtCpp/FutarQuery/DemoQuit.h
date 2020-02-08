#ifndef DEMOQUIT_H
#define DEMOQUIT_H
#include <QObject>
#include <QCoreApplication>

/**
 * @brief Signal hatására befejezi az alkalmazás futását.
 * Ez az osztály azért kell, hogy signal hatására tudjuk
 * leállítani az alkalmazást, miközben a main() az eseménykezelő
 * főciklust futtatja.
 */
class DemoQuit : public QObject
{
    Q_OBJECT

public:
    explicit DemoQuit() : QObject(nullptr) { }

public slots:
    void Quit()
    {
        QCoreApplication::quit();
    }
};

#endif // DEMOQUIT_H
