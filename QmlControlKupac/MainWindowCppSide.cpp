#include <QQmlProperty>
#include "MainWindowCppSide.h"

// További információ és példák a C++ - QML kapcsolatról:
// http://doc.qt.io/qt-5/qtqml-cppintegration-interactqmlfromcpp.html

MainWindowCppSide::MainWindowCppSide(QObject *rootObject)
    : QObject(nullptr)
{
    if (!rootObject)
    {
        qDebug() << "Nem találom a rootObject-et.";
    }

    mainWindowObject = findItemByName(rootObject, QString("ApplicationWindow"));

    if (!mainWindowObject)
    {
        qDebug() << "Nem találom a ApplicationWindow objektumot.";
    }

    qDebug() << "QML signal csatlakoztatása";
    QObject::connect(mainWindowObject, SIGNAL(addGreenEntry()),
        this, SLOT(addGreenEntryHandler()));

    qDebug() << "MainWindowCppSide inicializálva.";
}

QQuickItem* MainWindowCppSide::findItemByName(const QString& name)
{
    Q_ASSERT(mainWindowObject != nullptr);
    if (mainWindowObject->objectName() == name)
    {
        return mainWindowObject;
    }
    return findItemByName(mainWindowObject->children(), name);
}

/** Eseménykezelő a QML oldali addBlueEntry signalhoz. */
void MainWindowCppSide::addGreenEntryHandler()
{
    qDebug() << "MainWindowCppSide::addGreenEntryHandler()";
    auto radioCanvasList = findItemByName("RadioCanvasList");
    // Metódus meghívása

    QVariant returnedValue;
    QVariant messageText = "Zöldre váltás (és vonalvastagítás) C++-ból!";
    QVariant color = "green";
    qDebug() << "selectColor QML függvény meghívása...";
    QMetaObject::invokeMethod(radioCanvasList, "selectColor",
        Q_RETURN_ARG(QVariant, returnedValue),
        Q_ARG(QVariant, messageText),
        Q_ARG(QVariant, color));

    QQmlProperty::write(radioCanvasList, "lineWidth", 5);
}

QQuickItem* MainWindowCppSide::findItemByName(QObject *rootObject, const QString& name)
{
    Q_ASSERT(rootObject != nullptr);
    if (rootObject->objectName() == name)
    {
        return (QQuickItem*)rootObject;
    }
    return findItemByName(rootObject->children(), name);
}

QQuickItem* MainWindowCppSide::findItemByName(QList<QObject*> nodes, const QString& name)
{
    for(int i = 0; i < nodes.size(); i++)
    {
        // Node keresése
        if (nodes.at(i) && nodes.at(i)->objectName() == name)
        {
            return dynamic_cast<QQuickItem*>(nodes.at(i));
        }
        // Gyerekekben keresés
        else if (nodes.at(i) && nodes.at(i)->children().size() > 0)
        {
            QQuickItem* item = findItemByName(nodes.at(i)->children(), name);
            if (item)
                return item;
        }
    }
    // Nem találtuk.
    return nullptr;
}
