<script>

    import { onMount } from "svelte";
    import Button from '../Components/Button.svelte'
    import { PortfolioApi } from '../api/portfolioApi.js'

    const portfolioApi = new PortfolioApi();

    onMount(async _ => {

        let portfolioUrl = portfolioApi.getPortfoliosUrl();

        window.$("#portfoliosGridContainer").dxDataGrid({
            dataSource: portfolioUrl,
            columns: [
                "name",
                {
                    dataField: "holderIdentificationNumber",
                    caption: "HIN"
                }
            ],
            showBorders: true,
            allowColumnResizing: true,
            columnResizingMode: "nextColumn",
            columnMinWidth: 50,
            columnAutoWidth: true
        });

        let holdingsUrl = portfolioApi.getHoldingsUrl();

        window.$("#holdingsGridContainer").dxDataGrid({
            dataSource: holdingsUrl,
            columns: [
                "stockCode",
                {
                    dataField: "purchaseDate",
                    dataType: "datetime",
                    format: "dd/MM/y"
                },
                {
                    dataField: "numberOfShares",
                    caption: "No. of Shares"
                },
                {
                    dataField: "purchasePrice",
                    format: "#0.00##"
                },
                {
                    dataField: "brokerage",
                    format: "#0.00"
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

<h3>Portfolios</h3>

<div class="mb-3" id="portfoliosGridContainer">
</div>

<div class="mb-3">
    <Button to="/portfolio/addportfolio">Add Portfolio</Button>
</div>

<h3>Holdings</h3>

<div class="mb-3" id="holdingsGridContainer">
</div>

<div class="mb-3">
    <Button to="/portfolio/addholding">Add Holding</Button>
</div>