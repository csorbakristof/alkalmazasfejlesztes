import QtQuick 2.0
//import jbQuick.Charts 1.0

import QtQuick.Controls 1.3 // for Button


Canvas {
/*    color: "red"
    border.color: "black"
    border.width: 1
    radius: 5 */

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

        // Draw a line
        context.beginPath();
        context.lineWidth = 2;
        context.strokeStyle = "rgba(220,220,220,1)"
        context.moveTo(0, graphVelocities[0]);
        for(var i=0; i<graphVelocities.length;i++)
        {
            console.log(graphVelocities[i]);
            context.lineTo(10*i, 50+graphVelocities[i]);
        }
        context.stroke();

    } // end onPaint


/*    Chart {
      id: historyChart;
      width: 400;
      height: 200;
      chartAnimated: false;
      chartAnimationEasing: Easing.Linear;
      chartAnimationDuration: 2000;
      chartType: Charts.ChartType.LINE;
      Component.onCompleted: {
          chartData = {
              labels: historyGraphTimestamps,
              datasets: [
                  {
                      label: "Data row 1",
                      fillColor: "rgba(220,220,220,0.2)",
                      strokeColor: "rgba(220,220,220,1)",
                      pointColor: "rgba(220,220,220,1)",
                      pointStrokeColor: "#fff",
                      pointHighlightFill: "#fff",
                      pointHighlightStroke: "rgba(220,220,220,1)",
                      data: historyGraphVelocity
                  },
                  {
                      label: "Data row 2",
                      fillColor: "rgba(151,187,205,0.2)",
                      strokeColor: "rgba(151,187,205,1)",
                      pointColor: "rgba(151,187,205,1)",
                      pointStrokeColor: "#fff",
                      pointHighlightFill: "#fff",
                      pointHighlightStroke: "rgba(151,187,205,1)",
                      data: historyGraphAcceleration
                  }
              ]
            } // end chartData
        }
    } */
}

// https://github.com/jwintz/qchart.js
// http://www.chartjs.org/docs/#line-chart-example-usage
