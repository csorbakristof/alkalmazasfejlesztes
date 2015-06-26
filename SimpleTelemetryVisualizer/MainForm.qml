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

    // Make historyGraph easier to access from CPP for signal connection
    //property alias historyGraph: graphGB.historyGraph

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
            Text { text: " Status: " + model.statusName }
            Text { text: " X: " + model.x.toString() }
            Text { text: " V: " + model.v.toString() }
            Text { text: " A: " + model.a.toString() }
        }
    }


    GroupBox {
        id: currentValuesGB
        anchors.right: parent.right
        anchors.rightMargin: 0
        title: "Current values"
        anchors.left : commandsGB.right
        anchors.top : parent.top
//        anchors.bottom: connectionGB.bottom

        ColumnLayout {
            anchors.top: parent.top
            anchors.bottom: parent.bottom
            anchors.left: parent.left
            Text { text: " Status: " + currentState.statusName }
            Text { text: " Timestamp: " + currentState.timestamp }
            Text { text: " X: " + currentState.x }
            Text { text: " V: " + currentState.v.toString() }
            Text { text: " A: " + currentState.a.toString() }
            Text { text: " Light: " + currentState.light.toString() }
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

//        RowLayout {
            // This is the actual list view
            ListView {
                id: stateHistoryList
                width: 200
                model: historyModel
                delegate: stateDelegate
                anchors.top: parent.top
                anchors.bottom: parent.bottom
            }

            HistoryGraph {
                id: historyGraph
                // To allow finding it via findChild from C++
                objectName: "historyGraph"

                width: 200
                anchors.left: stateHistoryList.right
                anchors.top: parent.top
                anchors.bottom: parent.bottom

                graphTimestamps: historyGraphTimestamps
                graphVelocities: historyGraphVelocity
                graphAccelerations: historyGraphAcceleration

                Button {
                    text: qsTr("Paint");
                    onClicked: {
                        parent.requestPaint();//historyGraph.redraw()
                    }
                }

/*                function redraw()
                {
                    requestPaint();
                } */
            }
        }
//    }
}
