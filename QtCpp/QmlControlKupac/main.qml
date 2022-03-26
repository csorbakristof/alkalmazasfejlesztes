import QtQuick
import QtQuick.Controls
import QtQuick.Window
import QtQuick.Dialogs

ApplicationWindow {
    title: "QmlControlKupac"
    width: 640
    height: 480
    visible: true

    // A C++ oldal számára
    objectName: "ApplicationWindow"
    signal addGreenEntry();

    menuBar: MenuBar {
        Menu {
            title: "&Minden"
            MenuItem {
                text: "&Zöld"
                onTriggered: addGreenEntry();
            }
            MenuItem {
                text: "&Kilépés"
                onTriggered: Qt.quit();
            }
        }
    }

    RadioCanvasList {
    }
}
