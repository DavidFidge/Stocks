<script>
    export let endOfDayData;

    $:{
        if (endOfDayData && endOfDayData.length > 0)
        {
            let lows = endOfDayData.map(curVal => curVal.low);
            let highs = endOfDayData.map(curVal => curVal.high);
            
            let min = Math.min.apply(null, lows);
            let max = Math.max.apply(null, highs);

            let yAxisTickInterval = (max - min) / 10.0;

            let yAxisPrecision = 0;

            if (min < 100)
                yAxisPrecision = 2;
            if (min < 1)
                yAxisPrecision = 4;
            
            window.$("#chart-container").dxChart(
            {
                title: `${endOfDayData[0].stockCode} Stock Price`,
                dataSource: endOfDayData,
                commonSeriesSettings: {
                    argumentField: "date",
                    type: "candlestick"
                },
                legend: {
                    itemTextPosition: 'left'
                },
                series: [
                    { 
                        name: `${endOfDayData[0].stockCode}`,
                        openValueField: "open", 
                        highValueField: "high", 
                        lowValueField: "low", 
                        closeValueField: "close", 
                        reduction: {
                            color: "red"
                        }
                    }
                ],    
                valueAxis: {
                    tickInterval: yAxisTickInterval,
                    label: {
                        format: {
                            type: "currency",
                            precision: yAxisPrecision
                        }
                    }
                },
                argumentAxis: {
                    workdaysOnly: true,
                    label: {
                        customizeText: function(axisDate) {
                            let date = new Date(axisDate.value);
                            return date.toLocaleDateString("en-GB");
                        }
                    }
                },
                "export": {
                    enabled: true
                },
                tooltip: {
                    enabled: true,
                    location: "edge",
                    customizeTooltip: function (arg) {
                        return {
                            text: "Open: $" + arg.openValue + "<br/>" +
                                "High: $" + arg.highValue + "<br/>" +
                                "Low: $" + arg.lowValue + "<br/>" +
                                "Close: $" + arg.closeValue + "<br/>"
                        };
                    }
                }
            });
        }
    }
   
</script>

<div id="chart-container" style="width: 100%; height: 400px; background-color: white;"></div>