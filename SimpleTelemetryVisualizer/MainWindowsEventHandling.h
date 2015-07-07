#pragma once
#ifndef MAINWINDOWSEVENTHANDLING_H
#define MAINWINDOWSEVENTHANDLING_H
#include <QObject>
#include <QDebug>

class RobotProxy;
class QQmlContext;
class RobotStateHistory;

class QQuickItem;
class QQmlApplicationEngine;

/**
 * The class containing the GUI event handlers.
 *
 * Use it by assigning required components to the constructor and
 * connect the event handling slots to the corresponding signals of the GUI elements.
 *
 * Call ConnectQmlSignals() with the QML root object. The function will link the necessary
 *  notification signals towards the QML GUI.
 */
class MainWindowsEventHandling : public QObject
{
    Q_OBJECT

public:
    /** Constructor
     * @param robot Reference to the robot instance
     * @param qmlContext    Reference to the QML context
     * @param history   Reference to the robot state history instance.
     */
    MainWindowsEventHandling(RobotProxy& robot, QQmlContext &qmlContext, RobotStateHistory &history);

    ~MainWindowsEventHandling() = default;

    /** Connects the used QML signals. Call this after the QML environment is set up.
     * @param rootObject    The QML root object of the user interface.
     */
    void ConnectQmlSignals(QObject *rootObject);

public slots:
    /** Instructs the robot to accelerate. Event handler of the Accelerate button. */
    void accelerateCommand();

    /** Instructs the robot to stop. Event handler of the Stop button. */
    void stopCommand();

    /** Instructs the robot to reset itself. Event handler of the Reset button. */
    void resetCommand();

    /** Indicates that the state history has changed. Typically due to data reception from the simulator.
     * Updates the QML properties and notifies the controls via emitting historyContextUpdated().
    */
    void historyChanged();

signals:
    /** Signal used to instruct the history graph (QML control) to redraw itself */
    void historyContextUpdated();

private:
    /** Internal reference to the robot to control. */
    RobotProxy& robot;

    /** \addtogroup References used for data binding
     *  @{
     */
    /** QML Context used for refreshing the robot data. */
    QQmlContext &qmlContext;
    /** Reference to the state history container. */
    RobotStateHistory &history;
    /** @}*/

    /** Helper method to recursively locate QML items.
     * Used by FindItemByName(QObject *rootObject, const QString& name).
     * @param nodes List of nodes where to look for a QML object with given objectName
     * @param name  The objectName to look for.
     */
    static QQuickItem* FindItemByName(QList<QObject*> nodes, const QString& name);

    /** Helper method to recursively locate QML items.
     * @param rootObject The QML root object to start the search with.
     * @param name The objectName to look for.
     */
    static QQuickItem* FindItemByName(QObject *rootObject, const QString& name);
};

#endif // MAINWINDOWSEVENTHANDLING_H
