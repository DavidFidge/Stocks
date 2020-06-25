<script>
    
    import TextInput from "../Components/TextInput.svelte";
    import MoneyInput from "../Components/MoneyInput.svelte";
    import DateInput from "../Components/DateInput.svelte";
    import NumberInput from "../Components/NumberInput.svelte";
    import Button from "../Components/Button.svelte";

    let companyCode = "";
    let purchaseDate = "";
    let numberOfShares = "";
    let purchasePrice = "";
    let pricePerShare = "Price Per Share: 0.00";
    let brokerage = "";

    $: { 
        if (window.$.isNumeric(purchasePrice) && window.$.isNumeric(numberOfShares))
        {
            pricePerShare = "Price Per Share: " + (purchasePrice / numberOfShares).toFixed(4).replace(/([0-9]+\.[0-9][0-9][1-9]?)(0+)/,'$1');
        }
        else
        {
            pricePerShare = "Price Per Share: 0.00";
        }
    }

    async function handleSubmit(event) {
    };

</script>

<style>
    .formContainer {
        padding: 10px 0px 10px 0px;
    }
</style>

<h3>Add Holding</h3>

<div class="formContainer">
    <form on:submit|preventDefault={handleSubmit}>
        <TextInput label="Company Code" bind:value={companyCode} />
        <DateInput label="Purchase Date" bind:value={purchaseDate} />
        <NumberInput label="Number of Shares" bind:value={numberOfShares} />
        <MoneyInput label="Total Purchase Price" bind:value={purchasePrice} shadowLabel={pricePerShare} />
        <MoneyInput label="Brokerage" bind:value={brokerage} />
        <div>
            <Button>Save</Button>
        </div>
    </form>
</div>