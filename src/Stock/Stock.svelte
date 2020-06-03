<script>
    import Select from 'svelte-select';
    import StockChart from './StockChart.svelte'
    import { StockApi } from '../api/stockApi.js'
    import { EndOfDayApi } from '../api/endOfDayApi.js'

    let selectedStock = undefined;
    const stockApi = new StockApi();
    const endOfDayApi = new EndOfDayApi();
   
    const loadOptions = async function(filteredText) {
        let stockData = (await stockApi.getStocks(filteredText)).data;
        
        let mappedData = window.$.map(stockData, function (v) {
            return {
            value: v.code,
            label: v.code.concat(' - ', v.description)
            }
        });

        return mappedData;
    };

    async function handleClick()
    {
        if (selectedStock)
        {
            let endOfDayData = (await endOfDayApi.getEndOfDay(selectedStock.value)).data;
        }
    }
  
</script>

<div>    
    <h3>Search for a stock: </h3>

    <div class="form-inline my-2 my-lg-0">
        <div style="width: 500px">
            <Select {loadOptions} placeholder="Asx code or company name" listPlacement="bottom" bind:selectedValue={selectedStock}></Select>
        </div>
        <button class="btn btn-outline-success my-2 my-sm-0" on:click={handleClick}>Search</button>
    </div>

    <StockChart />
</div>