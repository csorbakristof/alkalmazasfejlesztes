import QtQuick 2.0
//import jbQuick.Charts 1.0

Rectangle {
    id: historyGraph

    color: "red"
    border.color: "black"
    border.width: 5
    radius: 10

    Text { text: "HELOOOO HISTORYGRAPH" }

/*    QChart {
      id: chart_line;
      width: 400;
      height: 400;
      chartAnimated: true;
      chartAnimationEasing: Easing.InOutElastic;
      chartAnimationDuration: 2000;
      chartType: Charts.ChartType.LINE;
      Component.onCompleted: {
          chartData = {
              labels: ["January", "February", "March", "April", "May", "June", "July"],
              datasets: [
                  {
                      label: "My First dataset",
                      fillColor: "rgba(220,220,220,0.2)",
                      strokeColor: "rgba(220,220,220,1)",
                      pointColor: "rgba(220,220,220,1)",
                      pointStrokeColor: "#fff",
                      pointHighlightFill: "#fff",
                      pointHighlightStroke: "rgba(220,220,220,1)",
                      data: [65, 59, 80, 81, 56, 55, 40]
                  },
                  {
                      label: "My Second dataset",
                      fillColor: "rgba(151,187,205,0.2)",
                      strokeColor: "rgba(151,187,205,1)",
                      pointColor: "rgba(151,187,205,1)",
                      pointStrokeColor: "#fff",
                      pointHighlightFill: "#fff",
                      pointHighlightStroke: "rgba(151,187,205,1)",
                      data: [28, 48, 40, 19, 86, 27, 90]
                  }
              ]
            } // end chartData
        }
    } */

}

// https://github.com/jwintz/qchart.js
// http://www.chartjs.org/docs/#line-chart-example-usage
