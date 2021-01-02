<script>
    import { navigate } from "svelte-routing";
    import { PortfolioApi } from '../api/portfolioApi.js'
    import { StockApi } from '../api/stockApi.js'
    import { onMount } from "svelte";
    import TextInput from "../Components/TextInput.svelte";
    import MoneyInput from "../Components/MoneyInput.svelte";
    import DateInput from "../Components/DateInput.svelte";
    import NumberInput from "../Components/NumberInput.svelte";
    import Button from "../Components/Button.svelte";
    import ServerError from "../Components/ServerError.svelte";
    import SelectInput from '../Components/SelectInput.svelte';

    const portfolioApi = new PortfolioApi();
    const stockApi = new StockApi();

    let stock = undefined;
    let portfolio = undefined;
    let purchaseDate = "";
    let numberOfShares = "";
    let purchasePrice = "";
    let pricePerShare = "Price Per Share: 0.00";
    let brokerage = "";
    let portfolios = [];
    let holding = {};
    let errors = [];

    async function getPortfolios()
    {
        return await portfolioApi.getPortfolios();
    }

    const stockLoadOptions = async function(filteredText) {
        let stockData = (await stockApi.getStocks(filteredText)).data;
        
        let mappedData = window.$.map(stockData, function (v) {
            return {
            value: v.code,
            label: v.code.concat(' - ', v.companyName)
            }
        });

        return mappedData;
    };

    onMount(async _ => {
        const { data } = await getPortfolios();

        portfolios = window.$.map(data, function (v) {
            return {
            value: v.id,
            label: v.name
            }
        });
    });

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

    $: { 

        let stockCode = null;
        let portfolioId = null;
       
        if (stock)
            stockCode = stock.value;

        if (portfolio)
            portfolioId = portfolio.value;
        
        holding = { stockCode: stockCode, purchaseDate, numberOfShares, purchasePrice, brokerage, portfolioId: portfolioId }    
    }

    async function handleSubmit(event) {

        let response = await portfolioApi.addHolding(holding);

        if (response.ok === true)
        {
            navigate("/holdings");
        }
        else
        {
            errors = response.response.errors;
        }
    }

</script>

<style>
    .formContainer {
        padding: 10px 0px 10px 0px;
        max-width: 500px;
    }
</style>

<h3>Add Holding</h3>

<div class="formContainer">
    <form novalidate on:submit|preventDefault={handleSubmit}>
        <SelectInput id="StockCode" label="Stock Code" loadOptions={stockLoadOptions} placeholder="Asx code or company name" bind:selectedValue={stock} required={true} {errors} />
        <DateInput id="PurchaseDate" label="Purchase Date" bind:value={purchaseDate} required={true} {errors} />
        <NumberInput id="NumberOfShares" label="Number of Shares" bind:value={numberOfShares} required={true} min={1} {errors} />
        <MoneyInput id="PurchasePrice" prependLabel="$" label="Total Purchase Price" bind:value={purchasePrice} appendLabel={pricePerShare} required={true} {errors} />
        <MoneyInput id="Brokerage" prependLabel="$" label="Brokerage" bind:value={brokerage} required={true} {errors} />
        <SelectInput id="Portfolio" label="Portfolio" required={true} items={portfolios} bind:selectedValue={portfolio} {errors} />
        <Button>Save</Button>
    </form>
</div>
<ServerError></ServerError>
