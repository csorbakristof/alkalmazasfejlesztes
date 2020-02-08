import QtQuick 2.6
import QtQuick.Window 2.2
import QtQuick.Controls 1.3

Window {
    visible: true
    width: 320
    height: 240
    title: qsTr("Web request in QML")

    ListModel {
        id: model
    }

    ListView {
        id: listview
        anchors.fill: parent
        model: model
        delegate: Text {
            text: colorName
        }
    }

    function getData() {
        var xmlhttp = new XMLHttpRequest();
        var url = "http://avalon.aut.bme.hu/~kristof/restapidemo/demo.json";

        xmlhttp.onreadystatechange=function() {
            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                myFunction(xmlhttp.responseText);
            }
        }
        xmlhttp.open("GET", url, true);
        xmlhttp.send();
    }

    function myFunction(json) {
        var obj = JSON.parse(json);
        obj.items.forEach(
            function(entry)
            {
                listview.model.append( {colorName: entry.color });
                console.log("Added: " + entry.color);
            }
            );
    }

    Button {
        anchors.bottom: parent.bottom
        width: parent.width
        text: "GET Data"
        onClicked: getData()
    }
}

/* Content of demo.json:
{
    "aim":"demo",
    "items":
    [
    {
        "color": "black",
        "luminance": "0"
    },
    {
        "color": "gray",
        "luminance": "127"
    },
    {
        "color": "white",
        "luminance": "255"
    }
    ]
}
*/
