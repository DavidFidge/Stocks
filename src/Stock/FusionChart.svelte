<script>
    export let endOfDayData;

    $:{
        if (endOfDayData && endOfDayData.length > 0)
        {
            window.$.each(endOfDayData, function(i, item) {
                item.x = i + 1;
            });

            let chartConfig = {
                type: "candlestick",
                renderAt: "chart-container",
                width: "100%",
                height: "400",
                dataFormat: "json",
                dataSource: {
                    chart: {
                        theme: "fusion",
                        caption: `${endOfDayData[0].stockCode} Chart`,
                        subCaption: "Last 2 months",
                        numberprefix: "$",
                        vNumberPrefix: " ",
                        pyaxisname: "Price",
                        vyaxisname: "Volume (In Millions)",
                        showVolumeChart: "1",
                        toolTipColor: "#ffffff",
                        toolTipBorderThickness: "0",
                        toolTipBgColor: "#000000",
                        toolTipBgAlpha: "80",
                        toolTipBorderRadius: "2",
                        toolTipPadding: "5"
                    },
                    categories: [{
                        category: [{
                            label: "2 months ago",
                            x: "1"
                        },
                        {
                            label: "1 month ago",
                            x: "31"
                        },
                        {
                            label: "Today",
                            x: "60"
                        }]
                    }],
                    dataset: [{
                        data: endOfDayData
                    }]
                }
            };

            var fusioncharts = new FusionCharts(chartConfig);
            fusioncharts.render();
        }
    }
   
</script>

<div id="chart-container" style="width: 100%; height: 400px; background-color: white;"></div>