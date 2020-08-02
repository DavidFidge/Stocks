<script>

    import { PortfolioApi } from '../api/portfolioApi.js'
    import TextInput from "../Components/TextInput.svelte";
    import Button from "../Components/Button.svelte";

    const portfolioApi = new PortfolioApi();

    let name = "";

    let portfolio = {};

    $: { portfolio = { name } }

    async function handleSubmit(event) {
        const { ok } = await portfolioApi.addPortfolio(portfolio);
        
        if (ok) {
            navigate("/portfolio");
        }
    }

</script>

<style>
    .formContainer {
        padding: 10px 0px 10px 0px;
        max-width: 500px;
    }
</style>

<h3>Add Portfolio</h3>

<div class="formContainer">
    <form on:submit|preventDefault={handleSubmit}>
        <TextInput label="Name" bind:value={name} />
        <div>
            <Button>Save</Button>
        </div>
    </form>
</div>