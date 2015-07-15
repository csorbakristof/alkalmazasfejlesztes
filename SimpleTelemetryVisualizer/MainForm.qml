import QtQuick 2.0
import QtQuick.Controls 1.3
import QtQuick.Layouts 1.1

Item {
    width: 500
    height: 500
    anchors.fill: parent

    // Signalok, melyek a kiadott parancsokat jelzik és a nyomógombok
    //  eseménykezelői aktiválják őket.
    signal resetCommand;
    signal accelerateCommand;
    signal stopCommand;

    // A parancs nyomógombok elemcsoportja
    GroupBox {
        id: commandsGB
        title: "Parancsok"
        // Bal oldalon és fent követi a szülőt. A szélessége fix.
        anchors.left : parent.left
        anchors.top : parent.top
        width: 200

        // A nyomógombokat oszlopba rendezzük
        ColumnLayout {
            id: columnLayout1
            // Az oszlop kitölti a szülőt, vagyis a commandsGB-t.
            anchors.fill: parent

            // Reset nyomógomb. Oldal irányba kitöltik a szülőt, 0 pixel margó kihagyásával.
            //  Megnyomása esetén (Button.Clicked signal) meghívja a resetCommand signalt. (Ez
            //  a signal látható innen, mivel a Button egyik ősében definiáltuk.)
            Button {
                id: resetBtn
                anchors.left: parent.left
                anchors.right: parent.right
                text: qsTr("Reset")
                anchors.leftMargin: 0
                anchors.rightMargin: 0
                onClicked: resetCommand()
            }

            Button {
                id: accelerateBtn
                anchors.left: parent.left
                anchors.right: parent.right
                text: qsTr("Gyorsítás")
                anchors.rightMargin: 0
                anchors.leftMargin: 0
                onClicked: accelerateCommand()
            }
            Button {
                id: stopBtn
                anchors.left: parent.left
                anchors.right: parent.right
                text: qsTr("Stop")
                anchors.rightMargin: 0
                anchors.leftMargin: 0
                onClicked: stopCommand()
            }
        }
    }

    // Aktuális értékek elemcsoportja
    GroupBox {
        id: currentValuesGB
        title: "Pillanatnyi értékek"
        // Fent és jobbra kitölti a szülőt. Balról illeszkedik a
        //  parancsok GroupBox-ának jobb széléhez.
        anchors.right: parent.right
        anchors.rightMargin: 0
        anchors.left : commandsGB.right
        anchors.top : parent.top

        // Oszlopba rendezett további elemek
        ColumnLayout {
            // Felfelé, lefelé és balra a szülő széléhez illeszkedik. Jobbra nem, mert
            //  széthúzni felesleges őket.
            anchors.top: parent.top
            anchors.bottom: parent.bottom
            anchors.left: parent.left
            // Sima szövegek (Text elemek), amiknek az értéke egy a C++ oldalon definiált currentState
            //  értékétől függ. (Ha az értéke null, akkor "?" jelenik meg.)
            // A currentState-et a MainWindowsEventHandling::historyChanged metódus regisztrálja be, hogy
            //  látható legyen a QML oldalról is. (Hivatkozás a RobotStateHistory::currentState-re.)
            Text { text: " Állapot: " + (currentState!=null ? currentState.statusName : "?") }
            Text { text: " Idő: " + (currentState!=null ? currentState.timestamp : "?") }
            Text { text: " X: " + (currentState!=null ? currentState.x.toFixed(3) : "?") }
            Text { text: " V: " + (currentState!=null ? currentState.v.toFixed(3) : "?") }
            Text { text: " A: " + (currentState!=null ? currentState.a.toFixed(3) : "?") }
            Text { text: " Lámpa: " + (currentState!=null ? currentState.light.toString() : "?") }
        }
    }

    // Delegate: this is the appearance of a list item
    // A későbbi history listának szüksége van egy delegate-re. Minden lista elem ennek a komponensnek egy
    //  példánya lesz.
    Component {
        // ID, hogy tudjuk a listánál hivatkozni erre, mint a lista delegatejére.
        id: stateDelegate
        Row {
            // Itt a model az, ami a list egyik eleme. (Bármi is legyen majd az.)
            Text { text: model.statusName }
            Text { text: " X: " + model.x.toFixed(3) }
            Text { text: " V: " + model.v.toFixed(3) }
            Text { text: " A: " + model.a.toFixed(3) }
        }
    }

    // Az állapot lista és a grafikon GroupBoxa.
    GroupBox {
        id: graphGB
        title: qsTr("Grafikon")
        // Oldalra és lefelé kitölti a szülőt.
        anchors.right: parent.right
        anchors.rightMargin: 0
        anchors.left: parent.left
        anchors.leftMargin: 0
        anchors.bottom: parent.bottom
        anchors.bottomMargin: 0
        // Felfelé a commandsGB és currentValuesGB GroupBoxok közül ahhoz igazodik, aminek lejjebb van az alja.
        anchors.top: (commandsGB.bottom > currentValuesGB.bottom ? commandsGB.bottom : currentValuesGB.bottom )
        anchors.topMargin: 0

        // Sorban egymás mellett van a lista és a grafikon
        RowLayout {
            // Kitölti a szülőt és nem hagy helyet az elemek között.
            anchors.fill: parent
            spacing: 0
            // A history lista egy scrollozható elemen belül van.
            ScrollView {
                // A scrollohzató elem igazítása a szölő RowLayouthoz.
                // Itt a ScrollViewon belül adjuk meg, hogy a RowLayoutban
                //  mik legyenek a rá (ScrollViewra) vonatkozó méret beállítások,
                //  mert ezeket a RowLayout kezeli ebben az esetben.
                Layout.fillHeight: true
                Layout.fillWidth: true
                Layout.minimumWidth: 250
                Layout.preferredWidth: 250
                Layout.maximumWidth: 300
                Layout.minimumHeight: 150

                // Itt jön a tényleges lista.
                ListView {
                    id: stateHistoryList
                    // A model az, ahonnan az adatokat vesszük.
                    // A historyModel változót a MainWindowsEventHandling::historyChanged metódus teszi
                    //  elérhetővé a QML oldalon is.
                    //  Típusa QList<QObject*>, a tárolt pointerek valójában RobotState-ekre mutatnak.
                    model: historyModel
                    // A delegate megadása, vagyis hogy egy listaelem hogyan jelenjen meg.
                    //  (Már fentebb definiáltuk.)
                    delegate: stateDelegate

                    // Eseménykezelő, az elemek darabszámának változása esetén a kijelölést
                    //  a legalsó elemre viszi. Ezzel oldja meg, hogy folyamatosan scrollozódjon
                    //  a lista és a legutoló elem mindig látható legyen.
                    onCountChanged: {
                        stateHistoryList.currentIndex = stateHistoryList.count - 1;
                    }
                }
            }

            // A HistoryGraph példányosítása, melyet külön fájlban definiáltunk.
            //  (A rendszer név alapján találja meg a fájlt.)
            HistoryGraph {
                id: historyGraph
                // Az objectName akkor jó, ha C++ oldalról kell megkeresnünk egy QML oldalon definiált
                //  objektumot a findChild metódus rekurzív hívásaival.
                objectName: "historyGraph"

                // A RowLayout erre az elemre vonatkozó elhelyezés beállításai.
                Layout.fillHeight: true
                Layout.fillWidth: true
                Layout.minimumWidth: 200
                Layout.preferredWidth: 400
                Layout.minimumHeight: 150

                // Ezek pedig a HistoryGraph tulajdonságai, amiket majd ott definiálunk,
                //  itt pedig értéket adunk nekik. Az alábbi változókat (pl. historyGraphTimestamps)
                //  szintén a MainWindowsEventHandling::historyChanged metódus teszi elérhetővé
                //  a QML oldal számára.
                // Ezek az értékek C++ oldalon folyamatosan változnak. Minden változás esetén
                //  lefut a MainWindowsEventHandling::historyChanged és ezeket újraregisztrálja a QML
                //  oldal számára, így frissülni fog a HistoryGraph tulajdonság is.
                graphTimestamps: historyGraphTimestamps
                graphVelocities: historyGraphVelocity
                graphAccelerations: historyGraphAcceleration
            }
        }
    }
}
