import QtQuick 2.0
import QtQuick.Controls 1.3
import QtQuick.Layouts 1.1

Item {
    id: mainFormControl
    width: 500
    height: 500
    anchors.fill: parent

    signal resetCommand;
    signal accelerateCommand;
    signal stopCommand;

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
                id: resetBtn
                anchors.left: parent.left
                anchors.right: parent.right
                text: qsTr("Reset")
                anchors.leftMargin: 0
                anchors.rightMargin: 0
                onClicked: mainFormControl.resetCommand()
            }
            Button {
                id: accelerateBtn
                anchors.left: parent.left
                anchors.right: parent.right
                text: qsTr("Accelerate")
                anchors.rightMargin: 0
                anchors.leftMargin: 0
                onClicked: mainFormControl.accelerateCommand()
            }
            Button {
                id: stopBtn
                anchors.left: parent.left
                anchors.right: parent.right
                text: qsTr("Stop")
                anchors.rightMargin: 0
                anchors.leftMargin: 0
                onClicked: mainFormControl.stopCommand()
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

        // Delegate: this is the appearance of a list item
        Component {
            id: stateDelegate
            Row {
                id: aState
                Text { text: " X: " + model.x.toString() }
                Text { text: " V: " + v.toString() }
                Text { text: " A: " + a.toString() }
                // TODO: x nem jó helyre bindol; depends on non-NOTIFYable properties
            }
        }

        // This is the actual list view
        ListView {
            id: stateHistoryModel
            width: 100; height: 100
            model: historyModel
            delegate: stateDelegate
        }
    }
}
