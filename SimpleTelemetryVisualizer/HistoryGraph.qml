import QtQuick 2.0
//import jbQuick.Charts 1.0

import QtQuick.Controls 1.3 // for Button


Canvas {
    property var graphTimestamps;
    property var graphVelocities;
    property var graphAccelerations;

    onPaint: {
        // Get drawing context
        var context = getContext("2d");

        // Make canvas all gray
        context.beginPath();
        context.fillStyle = "gray"
        context.fillRect(0, 0, width, height);
        context.fill();

        drawDataset(context, graphVelocities, "rgba(220,220,220,1)", 5.0);
    } // end onPaint

    function drawDataset(context, datarow, strokeStyle, verticalScaler)
    {
        var offset = height/2;

        context.beginPath();
        context.lineWidth = 2;
        context.strokeStyle = strokeStyle;
        context.moveTo(0, offset-datarow[0]);
        for(var i=0; i<graphVelocities.length;i++)
        {
//            console.log(graphVelocities[i]);
            context.lineTo(10*i, offset - verticalScaler * datarow[i]);
        }
        context.stroke();
    }
}
