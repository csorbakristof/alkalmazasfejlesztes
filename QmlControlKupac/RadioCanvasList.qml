import QtQuick 2.4
import QtQuick.Controls 1.3
import QtQuick.Layouts 1.1

Item {
//    id: item1
/*    width: 600
    height: 400 */
    anchors.fill: parent

    property int margin: 10
    property color selectedColor : red

    RowLayout {
        id: baseGrid
        anchors.fill: parent
        anchors.margins: margin

        // 1. oszlop

        GroupBox {
            Layout.fillHeight: true

            ExclusiveGroup { id: radioButtonExclusiveGroup }

            ColumnLayout {
                anchors.fill: parent

                RadioButton {
                    id: redRadioButton
                    text: "Piros"
                    exclusiveGroup: radioButtonExclusiveGroup
                    onClicked: {
                        selectedColor = "red";
                        drawingCanvas.requestPaint();
                        eventLogModel.append( { name: "Váltás pirosra.", colorCode: "red" } );
                    }
                }

                RadioButton {
                    id: blueRadioButton
                    text: "Kék"
                    exclusiveGroup: radioButtonExclusiveGroup
                    onClicked: {
                        selectedColor = "blue";
                        drawingCanvas.requestPaint();
                        eventLogModel.append( { name: "Váltás kékre.", colorCode: "blue" } );
                    }
                }

                Canvas {
                    id: drawingCanvas
                    width: 100
                    Layout.fillHeight: true

                    onPaint: {
                        var context = getContext("2d");
                        context.fillStyle = selectedColor
                        context.fillRect(0, 0, width, height);
                        context.lineWidth = 3;
                        context.strokeStyle = "rgba(255,255,0,1)";
                        context.ellipse(width/2-30,height/2-30,60,60);
                        context.stroke();
                    }
                }
            }
        }

        // 2. oszlop

        GroupBox
        {
            Layout.fillHeight: true
            Layout.fillWidth: true
            Layout.rowSpan: 3

            ListView {
                id: eventLog
                anchors.fill: parent
                anchors.margins: 10
                delegate: GroupBox {
                    Layout.fillWidth: true
                    anchors.left: parent.left
                    anchors.right: parent.right
                    Row {
                        id: row2
                        Rectangle {
                            width: 40
                            height: 20
                            color: colorCode
                        }
                        Text {
                            text: name
                            anchors.verticalCenter: parent.verticalCenter
                            font.bold: true
                        }
                        spacing: 10
                    }
                }
                model: ListModel {
                    id: eventLogModel
                    ListElement {
                        name: "Indul a program..."
                        colorCode: "grey"
                    }
                    ListElement {
                        name: "Pirossal kezdünk"
                        colorCode: "red"
                    }
                }
            }
        }

        // 3. oszlop

        GroupBox {
            Layout.fillHeight: true
//            Layout.fillWidth: true
            Layout.rowSpan: 3
            width: 200

            ColumnLayout {
                anchors.fill: parent

                Text {
                    text: "Üzenet:";
                }

                TextField {
                    id: messageTextField
                    Layout.fillWidth: true
                    placeholderText: "Ide írd be az üzenetet..."
                }

                Button {
                    id: addMessageButton
                    Layout.fillWidth: true
                    text: "Üzenet hozzáadása"

                    onClicked: {
                        eventLogModel.append( { name: messageTextField.text, colorCode: "grey" } );
                    }
                }

                // Placeholder
                Item {
                    Layout.fillHeight: true
                }
            }
        }
    }
}

