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
                {
                    dataField: "name",
                    width: 200
                },
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

    });

</script>

<h3>Portfolios</h3>

<div class="mb-3" id="portfoliosGridContainer">
</div>

<div class="mb-3">
    <Button to="/portfolios/addportfolio">Add Portfolio</Button>
</div>
