import QtQuick 2.4
import QtQuick.Controls 1.3
import QtQuick.Window 2.2
import QtQuick.Dialogs 1.2

ApplicationWindow {
    title: qsTr("Hello World")
    width: 640
    height: 480
    visible: true

    signal resetCommandCpp()
    signal accelerateCommandCpp()
    signal stopCommandCpp()

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

        onResetCommand: {
//            messageDialog.show(qsTr("Resetting robot simulator..."));
            resetCommandCpp();
        }
        onAccelerateCommand: {
//            messageDialog.show(qsTr("Accelerating robot..."));
            accelerateCommandCpp();
        }
        onStopCommand: {
//            messageDialog.show(qsTr("Stopping robot..."));
            stopCommandCpp();
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
