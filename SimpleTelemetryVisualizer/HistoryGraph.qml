import QtQuick 2.0
import jbQuick.Charts 1.0

Rectangle {
    id: historyGraph

/*    color: "red"
    border.color: "black"
    border.width: 1
    radius: 5 */

    Chart {
      id: chart_line;
      width: 400;
      height: 200;
      chartAnimated: true;
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
                      data: historyGraphDataRow1
                  },
                  {
                      label: "Data row 2",
                      fillColor: "rgba(151,187,205,0.2)",
                      strokeColor: "rgba(151,187,205,1)",
                      pointColor: "rgba(151,187,205,1)",
                      pointStrokeColor: "#fff",
                      pointHighlightFill: "#fff",
                      pointHighlightStroke: "rgba(151,187,205,1)",
                      data: historyGraphDataRow2
                  }
              ]
            } // end chartData
        }
    }

}

// https://github.com/jwintz/qchart.js
// http://www.chartjs.org/docs/#line-chart-example-usage
