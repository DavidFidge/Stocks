<script>

    import { onMount } from "svelte";
    import Button from '../Components/Button.svelte'
    import { PortfolioApi } from '../api/portfolioApi.js'

    const portfolioApi = new PortfolioApi();

    let portfolios = {};

    async function getPortfolios()
    {
        return await portfolioApi.getPortfolios();
    }

    onMount(async _ => {
        const { data } = await getPortfolios();
        portfolios = JSON.stringify(data);
    });

</script>

<h3>Portfolios</h3>

<p>{portfolios}</p>

<Button to="/portfolio/addportfolio">Add Portfolio</Button>

<h3>Holdings</h3>

<Button to="/portfolio/addholding">Add Holding</Button>
