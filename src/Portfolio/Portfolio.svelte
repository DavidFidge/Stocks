<script>

    import { onMount } from "svelte";
    import Button from '../Components/Button.svelte'
    import { PortfolioApi } from '../api/portfolioApi.js'

    const portfolioApi = new PortfolioApi();

    let portfolios = {};
    let holdings = {};

    async function getPortfolios()
    {
        return await portfolioApi.getPortfolios();
    }

    async function getHoldings()
    {
        return await portfolioApi.getHoldings();
    }

    onMount(async _ => {
        let data = await getPortfolios();
        portfolios = JSON.stringify(data.data);

        data = await getHoldings();
        holdings = JSON.stringify(data.data);
    });

</script>

<h3>Portfolios</h3>

<p>{portfolios}</p>

<Button to="/portfolio/addportfolio">Add Portfolio</Button>

<h3>Holdings</h3>

<p>{holdings}</p>

<Button to="/portfolio/addholding">Add Holding</Button>
