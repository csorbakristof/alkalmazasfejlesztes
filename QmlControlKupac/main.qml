import QtQuick 2.4
import QtQuick.Controls 1.3
import QtQuick.Window 2.2
import QtQuick.Dialogs 1.2

ApplicationWindow {
    title: qsTr("Hello World")
    width: 640
    height: 480
    visible: true

    // Used by C++ to locate
    objectName: "ApplicationWindow"

    signal addGreenEntry();

    menuBar: MenuBar {
        Menu {
            title: qsTr("&Minden")
            MenuItem {
                text: qsTr("&Zöld")
                onTriggered: addGreenEntry();
            }
            MenuItem {
                text: qsTr("&Kilépés")
                onTriggered: Qt.quit();
            }
        }
    }

    RadioCanvasList {
    }
}
