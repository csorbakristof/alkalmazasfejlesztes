import QtQuick 2.4
import QtQuick.Controls 1.3
import QtQuick.Window 2.2
import QtQuick.Dialogs 1.2

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
