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
    import CheckboxInput from '../Components/CheckboxInput.svelte';

    const portfolioApi = new PortfolioApi();
    const stockApi = new StockApi();

    let stock = undefined;
    let portfolio = undefined;
    let paymentDate = "";
    let dividendAmount = "";
    let frankedAmount = "";
    let isDividendReinvestmentPlan = false;
    let reinvestmentNumberOfShares = "";
    let reinvestmentPrice = "";
    let portfolios = [];
    let dividend = {};
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

        let stockCode = null;
        let portfolioId = null;
       
        if (stock)
            stockCode = stock.value;

        if (portfolio)
            portfolioId = portfolio.value;
        
        dividend = {
            stockCode: stockCode,
            paymentDate,
            dividendAmount,
            frankedAmount,
            isDividendReinvestmentPlan,
            reinvestmentNumberOfShares,
            reinvestmentPrice,            
            portfolioId: portfolioId
        };    
    }

    let isSaving = false;

    async function handleSubmit(event) {

        isSaving = true;

        let dividendPost = {
            stockCode: dividend.stockCode,
            paymentDate: dividend.paymentDate,
            dividendAmount: dividend.dividendAmount,
            frankedAmount: dividend.frankedAmount,
            isDividendReinvestmentPlan: dividend.isDividendReinvestmentPlan,
            reinvestmentNumberOfShares: dividend.reinvestmentNumberOfShares || 0,
            reinvestmentPrice: dividend.reinvestmentPrice || 0,            
            portfolioId: dividend.portfolioId
        };   

        let response = await portfolioApi.addDividend(dividendPost);

        if (response.ok === true)
        {
            navigate("/dividends");
        }
        else
        {
            errors = response.response.errors;
            isSaving = false;
        }
    }

</script>

<style>
    .formContainer {
        padding: 10px 0px 10px 0px;
        max-width: 500px;
    }
</style>

<h3>Add Dividend</h3>

<div class="formContainer">
    <form novalidate on:submit|preventDefault={handleSubmit}>
        <SelectInput id="StockCode" label="Stock Code" loadOptions={stockLoadOptions} placeholder="Asx code or company name" bind:selectedValue={stock} required={true} {errors} />
        <DateInput id="PaymentDate" label="Payment Date" bind:value={paymentDate} required={true} {errors} />
        <MoneyInput id="DividendAmount" prependLabel="$" label="Dividend Amount" bind:value={dividendAmount} required={true} {errors} />
        <MoneyInput id="FrankedAmount" prependLabel="$" label="Franked Amount" bind:value={frankedAmount} required={true} {errors} />
        <SelectInput id="Portfolio" label="Portfolio" required={true} items={portfolios} bind:selectedValue={portfolio} {errors} />
        <CheckboxInput id ="IsDividendReinvestmentPlan" label="Dividend Reinvestment Plan" bind:value={isDividendReinvestmentPlan} />
        <NumberInput id="NumberOfShares" label="Number of Shares Reinvested" bind:value={reinvestmentNumberOfShares} required={isDividendReinvestmentPlan} min={1} {errors} />
        <MoneyInput id="ReinvestmentPrice" prependLabel="$" label="Total Reinvestment Price" bind:value={reinvestmentPrice} required={isDividendReinvestmentPlan} {errors} />
        <Button showSpinner={isSaving} disabled={isSaving}>Save</Button>
    </form>
</div>
<ServerError></ServerError>
