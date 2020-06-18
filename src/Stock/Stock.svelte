<script>
    import Select from 'svelte-select';
    import FusionChart from './FusionChart.svelte'
    import { StockApi } from '../api/stockApi.js'
    import { EndOfDayApi } from '../api/endOfDayApi.js'

    let selectedStock = undefined;
    let endOfDayData = undefined;
    const stockApi = new StockApi();
    const endOfDayApi = new EndOfDayApi();
   
    const loadOptions = async function(filteredText) {
        let stockData = (await stockApi.getStocks(filteredText)).data;
        
        let mappedData = window.$.map(stockData, function (v) {
            return {
            value: v.code,
            label: v.code.concat(' - ', v.companyName)
            }
        });

        return mappedData;
    };

    $: {
        if (selectedStock)
        {
            getEndOfDayData(selectedStock);
        }
    }

    async function getEndOfDayData(stock)
    {
        endOfDayData = (await endOfDayApi.getEndOfDay(stock.value)).data;
    }
  
</script>

<div>    
    <h3>Search for a stock: </h3>

    <div class="my-4" style="width: 500px">
        <Select {loadOptions} placeholder="Asx code or company name" listPlacement="bottom" bind:selectedValue={selectedStock}></Select>
    </div>

    <FusionChart endOfDayData={endOfDayData} />
</div>