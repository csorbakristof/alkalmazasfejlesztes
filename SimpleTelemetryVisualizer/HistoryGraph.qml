import QtQuick 2.0

Canvas {
    property var graphTimestamps;
    property var graphVelocities;
    property var graphAccelerations;

    onPaint: {
        // Get drawing context
        var context = getContext("2d");

        // Make canvas all gray
        context.beginPath();
        context.fillStyle = "white"
        context.fillRect(0, 0, width, height);
        context.fill();

        // Draw horizontal grid
        drawHorizontalLine(context, 0.0, "rgba(0,0,0,1)", 5.0)
        drawHorizontalLine(context, 5.0, "rgba(100,100,100,1)", 5.0)
        drawHorizontalLine(context, -5.0, "rgba(100,100,100,1)", 5.0)
        drawHorizontalLine(context, 10.0, "rgba(0,0,0,1)", 5.0)
        drawHorizontalLine(context, -10.0, "rgba(0,0,0,1)", 5.0)

        // Draw data sets
        drawDataset(context, graphVelocities, "rgba(110,220,110,1)", 5.0);
        drawDataset(context, graphAccelerations, "rgba(220,110,110,1)", 5.0);
    } // end onPaint

    function drawHorizontalLine(context, dataValue, strokeStyle, verticalScaler)
    {
        var offset = height/2;

        context.beginPath();
        context.lineWidth = 1;
        context.strokeStyle = strokeStyle;
        context.moveTo(0, offset - verticalScaler * dataValue);
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
        for(var i=0; i<graphVelocities.length;i++)
        {
            context.lineTo(10*i, offset - verticalScaler * datarow[i]);
        }
        context.stroke();
    }
}
