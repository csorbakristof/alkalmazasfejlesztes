import QtQuick 2.4
import QtQuick.Controls 1.3
import QtQuick.Window 2.2
import QtQuick.Dialogs 1.2

ApplicationWindow {
    title: qsTr("Hello World")
    width: 640
    height: 480
    visible: true

    signal connectRobotCpp(string s)
    signal disconnectRobotCpp(string s)
    signal startRobotCpp(string s)
    signal stopRobotCpp(string s)

    /* Istance of the MenuBar is the main menu bar. It contains Menu controls, which contain MenuItem controls. */
    menuBar: MenuBar {
        Menu {
            /* The Menu has a title property. The & sign indicates the hotkey: Alt-C enters this menu. */
            title: qsTr("&Exit")
            MenuItem {
                text: qsTr("E&xit")
                onTriggered: Qt.quit();
            }
        }
    }

    /* Istantiating the MainForm control which contains everything in the main form,
        except the menu. */
    MainForm {
        id: mainFormControl
        anchors.fill: parent

        onConnectRobot: {
            messageDialog.show(qsTr("Connecting robot..."));
            connectRobotCpp("HELLO");
        }
        onDisconnectRobot: {
            messageDialog.show(qsTr("Disconnecting robot..."));
            disconnectRobotCpp("HELLO");
        }
        onStartRobot: {
            messageDialog.show(qsTr("Starting robot..."));
            startRobotCpp("HELLO");
        }
        onStopRobot: {
            messageDialog.show(qsTr("Stopping robot..."));
            stopRobotCpp("HELLO");
        }

    }

    MessageDialog {
        id: messageDialog
        title: qsTr("Default message...")

        function show(caption) {
            messageDialog.text = caption;
            messageDialog.open();
        }
    }
}
