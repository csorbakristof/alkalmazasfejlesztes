import QtQuick 2.4
import QtQuick.Controls 1.3
import QtQuick.Layouts 1.1

Item {
    anchors.fill: parent
    objectName: "RadioCanvasList"

    property int margin: 10

    property color selectedColor : "grey"

    // C++ oldal is el tudja érni
    property int lineWidth : 3

    // C++ oldal is el tudja érni
    function selectColor(messageText, color)
    {
        selectedColor = color;
        drawingCanvas.requestPaint();
        eventLogModel.append( { message: messageText, colorCode: color } );
        console.log("selectColor(" + messageText + ", " + color + ")");
    }

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
                        selectColor("Váltás pirosra.", "red");
                    }
                }

                RadioButton {
                    id: blueRadioButton
                    text: "Kék"
                    exclusiveGroup: radioButtonExclusiveGroup
                    onClicked: {
                        selectColor("Váltás kékre.", "blue");
                    }
                }

                Canvas {
                    id: drawingCanvas
                    width: 100
                    Layout.fillHeight: true

                    onPaint: {
                        var context = getContext("2d");
                        context.reset();
                        context.fillStyle = selectedColor
                        context.fillRect(0, 0, width/2, height);
                        context.lineWidth = lineWidth;
                        context.strokeStyle = "rgba(255,255,0,1)";
                        context.ellipse(width/2-30,height/2-30,60,60);
                        context.stroke();
                        console.log("drawingCanvas.onPaint kész");
                    }
                }
            }
        }

        // 2. oszlop

        GroupBox
        {
            Layout.fillHeight: true
            Layout.fillWidth: true

            ListView {
                id: eventLog
                anchors.fill: parent
                anchors.margins: 10
                delegate: GroupBox {
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
                            text: message
                            anchors.verticalCenter: parent.verticalCenter
                            font.bold: true
                        }
                        spacing: 10
                    }
                }
                model: ListModel {
                    id: eventLogModel
                    ListElement {
                        message: "Indul a program..."
                        colorCode: "grey"
                    }
                    ListElement {
                        message: "Pirossal kezdünk"
                        colorCode: "red"
                    }
                }
            }
        }

        // 3. oszlop

        GroupBox {
            Layout.fillHeight: true
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
                        eventLogModel.append( { message: messageTextField.text, colorCode: "grey" } );
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

