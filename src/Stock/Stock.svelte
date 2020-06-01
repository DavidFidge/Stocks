<script>
    import { onMount } from "svelte";
    import Select from 'svelte-select';
    import { StockApi } from '../api/stockApi.js'

    let stockApi = new StockApi();
    
    let items = [];

    onMount(async function() {
        const { data } = await stockApi.getStocks();
        
        let mappedData = window.$.map(data, function (v) {        
            return {
            value: v.code,
            label: v.code.concat(' - ', v.description)
            }
        });

        items = mappedData;
    });
  
</script>

<div>
     
    <h3>Search for a stock: </h3>

    <form class="form-inline my-2 my-lg-0">
        <div style="width: 500px">
            <Select {items} placeholder="Asx code or company name" listPlacement="bottom"></Select>
        </div>
        <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
    </form>

</div>