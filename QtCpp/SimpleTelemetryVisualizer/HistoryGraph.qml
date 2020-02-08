import QtQuick 2.0

// A HistoryGraph alapvetően egy Canvas, mivel arra lehet rajzolni.
Canvas {
    // Vannak neki további tulajdonságai, amiket kintről kap
    property var graphTimestamps;
    property var graphVelocities;
    property var graphAccelerations;

    // A Canvas Paint signalja esetén újra kell rajzolni a grafikont.
    onPaint: {
        // A grafikon rajzolát itt, a QML oldalon, JavaScripben megírva történik.
        // Kell egy "drawing context", amire rajzolni tudunk.
        var context = getContext("2d");

        // Kitöltés fehérre
        context.fillStyle = "white"
        context.fillRect(0, 0, width, height);

        // Vízszintes vonalak berajzolása a függőleges pozíció, a szín és a függőleges
        //  skálázás megadásával. (Erre van külön függvényünk.)
        drawHorizontalLine(context, 0.0, "rgba(0,0,0,1)", 5.0)
        drawHorizontalLine(context, 5.0, "rgba(100,100,100,1)", 5.0)
        drawHorizontalLine(context, -5.0, "rgba(100,100,100,1)", 5.0)
        drawHorizontalLine(context, 10.0, "rgba(0,0,0,1)", 5.0)
        drawHorizontalLine(context, -10.0, "rgba(0,0,0,1)", 5.0)

        // Az adatsorok megrajzolása (a graphTimestamps értékét jelenleg nem használjuk).
        //  Ez is külön függvénybe került.
        drawDataset(context, graphVelocities, "rgba(110,220,110,1)", 5.0);
        drawDataset(context, graphAccelerations, "rgba(220,110,110,1)", 5.0);
    } // onPaint vége

    // Vízszintes vonal berajzolása.
    function drawHorizontalLine(context, dataValue, strokeStyle, verticalScaler)
    {
        var offset = height/2;

        context.beginPath();
        context.lineWidth = 1;
        context.strokeStyle = strokeStyle;
        // Mozgás a vonal elejére.
        context.moveTo(0, offset - verticalScaler * dataValue);
        // Vonal végére mozgás.
        context.lineTo(width, offset - verticalScaler * dataValue);
        context.stroke();
    }

    function drawDataset(context, datarow, strokeStyle, verticalScaler)
    {
        var offset = height/2;

        context.beginPath();
        context.lineWidth = 3;
        context.strokeStyle = strokeStyle;
        context.moveTo(0, offset-datarow[0]);
        // A vektoron végigmenve behúzzuk a grafikon szakaszait.
        for(var i=0; i<graphVelocities.length;i++)
        {
            context.lineTo(10*i, offset - verticalScaler * datarow[i]);
        }
        context.stroke();
    }
}
