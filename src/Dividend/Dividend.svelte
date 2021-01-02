<script>

    import { onMount } from "svelte";
    import Button from '../Components/Button.svelte'
    import { PortfolioApi } from '../api/portfolioApi.js'

    const portfolioApi = new PortfolioApi();

    onMount(async _ => {

        let dividendsUrl = portfolioApi.getDividendsUrl();

        window.$("#dividendsGridContainer").dxDataGrid({
            dataSource: dividendsUrl,
            columns: [
                "stockCode",
                "portfolioName",
                {
                    dataField: "paymentDate",
                    dataType: "datetime",
                    format: "dd/MM/y"
                },
                {
                    dataField: "dividendAmount",
                    format: "#0.00##"
                },
                {
                    dataField: "frankedAmount",
                    format: "#0.00##"
                },                
                {
                    dataField: "isDividendReinvestmentPlan",
                    caption: "DRP",
                    dataType: "bool"
                },
                {
                    dataField: "reinvestmentNumberOfShares",
                    caption: "DRP Shares"
                },
                {
                    dataField: "reinvestmentPrice",
                    format: "#0.00##"
                },             
                {
                    dataField: "portfolioId",
                    visible: false
                },
                {
                    dataField: "Id",
                    visible: false
                }
            ],
            showBorders: true,
            allowColumnResizing: true,
            columnResizingMode: "nextColumn",
            columnMinWidth: 50,
            columnAutoWidth: true
        });
    });

</script>

<h3>Dividends</h3>

<div class="mb-3" id="dividendsGridContainer">
</div>

<div class="mb-3">
    <Button to="/dividends/adddividend">Add Dividend</Button>
</div>