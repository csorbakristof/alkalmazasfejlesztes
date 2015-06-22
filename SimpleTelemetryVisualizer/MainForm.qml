import QtQuick 2.0
import QtQuick.Controls 1.3
import QtQuick.Layouts 1.1

Item {
    id: mainFormControl
    width: 500
    height: 500
    anchors.fill: parent

    signal connectRobot;
    signal disconnectRobot;
    signal startRobot;
    signal stopRobot;

    //property alias button3: button3

    Component.onCompleted: mainFormControl.connectRobot

    GroupBox {
        id: connectionGB
        title: "Connection and commands"
        anchors.left : parent.left
        anchors.top : parent.top
        width: 200

        ColumnLayout {
            id: columnLayout1
            anchors.fill: parent

            Button {
                id: connectBtn
                anchors.left: parent.left
                anchors.right: parent.right
                text: qsTr("Connect")
                anchors.leftMargin: 0
                anchors.rightMargin: 0
                onClicked: mainFormControl.connectRobot()
            }
            Button {
                id: disconnectBtn
                anchors.left: parent.left
                anchors.right: parent.right
                text: qsTr("Disconnect")
                anchors.rightMargin: 0
                anchors.leftMargin: 0
                onClicked: mainFormControl.disconnectRobot()
            }
            Button {
                id: startBtn
                anchors.left: parent.left
                anchors.right: parent.right
                text: qsTr("Start")
                anchors.rightMargin: 0
                anchors.leftMargin: 0
                onClicked: mainFormControl.startRobot()
            }
            Button {
                id: stopBtn
                anchors.left: parent.left
                anchors.right: parent.right
                text: qsTr("Stop")
                anchors.rightMargin: 0
                anchors.leftMargin: 0
                onClicked: mainFormControl.stopRobot()
            }
        }
    }

    GroupBox {
        id: currentValuesGB
        anchors.right: parent.right
        anchors.rightMargin: 0
        title: "Current values"
        anchors.left : connectionGB.right
        anchors.top : parent.top
        anchors.bottom: connectionGB.bottom

        ColumnLayout {
            anchors.fill: parent

            Button {
                text: qsTr("E")
            }
        }
    }

    GroupBox {
        id: graphGB
        anchors.right: parent.right
        anchors.rightMargin: 0
        anchors.left: parent.left
        anchors.leftMargin: 0
        anchors.bottom: parent.bottom
        anchors.bottomMargin: 0
        anchors.top: connectionGB.bottom
        anchors.topMargin: 0
        title: qsTr("Graph (history)")

/*        Rectangle {
            id: graphRect
            color: "#ffffff"
            clip: false
            visible: true
            anchors.fill: parent
        } */
    }
}
