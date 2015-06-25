import QtQuick 2.0
import jbQuick.Charts 1.0

import QtQuick.Controls 1.3 // for Button


Rectangle {
/*    color: "red"
    border.color: "black"
    border.width: 1
    radius: 5 */

    function redraw()
    {
        // Should be called when data changes
        console.log("HistoryGraph.redraw started");
        historyChart.repaintWithDataUpdate();
        historyChart.requestPaint();
        console.log("HistoryGraph.redraw finished");
    }

    Button {
        id: graphRepaintButton
        width: 100
        height: 20
        text: qsTr("Redraw")
        onClicked: {
            parent.redraw();
        }
    }


    Chart {
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
    }
}

// https://github.com/jwintz/qchart.js
// http://www.chartjs.org/docs/#line-chart-example-usage
