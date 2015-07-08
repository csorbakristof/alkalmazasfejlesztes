import QtQuick 2.0
import QtQuick.Controls 1.3
import QtQuick.Layouts 1.1

Item {
    width: 500
    height: 500
    anchors.fill: parent

    signal resetCommand;
    signal accelerateCommand;
    signal stopCommand;

    Component.onCompleted: mainFormControl.connectRobot

    GroupBox {
        id: commandsGB
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

    // Delegate: this is the appearance of a list item
    Component {
        id: stateDelegate
        Row {
            id: aState
            Text { text: model.statusName }
            Text { text: " X: " + model.x.toFixed(3) }
            Text { text: " V: " + model.v.toFixed(3) }
            Text { text: " A: " + model.a.toFixed(3) }
        }
    }


    GroupBox {
        id: currentValuesGB
        anchors.right: parent.right
        anchors.rightMargin: 0
        title: "Current values"
        anchors.left : commandsGB.right
        anchors.top : parent.top

        ColumnLayout {
            anchors.top: parent.top
            anchors.bottom: parent.bottom
            anchors.left: parent.left
            Text { text: " Status: " + (currentState!=null ? currentState.statusName : "?") }
            Text { text: " Timestamp: " + (currentState!=null ? currentState.timestamp : "?") }
            Text { text: " X: " + (currentState!=null ? currentState.x.toFixed(3) : "?") }
            Text { text: " V: " + (currentState!=null ? currentState.v.toFixed(3) : "?") }
            Text { text: " A: " + (currentState!=null ? currentState.a.toFixed(3) : "?") }
            Text { text: " Light: " + (currentState!=null ? currentState.light.toString() : "?") }
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
        // From commandsGB and currentValuesGB, align to the lower bottom
        anchors.top: (commandsGB.bottom > currentValuesGB.bottom ? commandsGB.bottom : currentValuesGB.bottom )
        anchors.topMargin: 0
        title: qsTr("Graph (history)")

        RowLayout {
            anchors.fill: parent
            spacing: 0
            // This is the actual list view
            ScrollView {
                Layout.fillWidth: true
                Layout.minimumWidth: 250
                Layout.preferredWidth: 250
                Layout.maximumWidth: 300
                Layout.minimumHeight: 150

                ListView {
                    id: stateHistoryList
                    model: historyModel
                    delegate: stateDelegate

                    onCountChanged: {
                        // Keep the last element selected
                        stateHistoryList.currentIndex = stateHistoryList.count - 1;
                    }
                }
            }

            HistoryGraph {
                id: historyGraph
                // To allow finding it via findChild from C++
                objectName: "historyGraph"

                Layout.fillWidth: true
                Layout.minimumWidth: 200
                Layout.preferredWidth: 400
                Layout.minimumHeight: 150

                graphTimestamps: historyGraphTimestamps
                graphVelocities: historyGraphVelocity
                graphAccelerations: historyGraphAcceleration
            }
        }
    }
}
