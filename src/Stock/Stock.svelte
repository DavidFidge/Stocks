<script>
    import Select from 'svelte-select';
    import { StockApi } from '../api/stockApi.js'

    const stockApi = new StockApi();
   
    const loadOptions = async function(filteredText) {
        const { data } = await stockApi.getStocks(filteredText);
        
        let mappedData = window.$.map(data, function (v) {        
            return {
            value: v.code,
            label: v.code.concat(' - ', v.description)
            }
        });

        return mappedData;
    };
  
</script>

<div>    
    <h3>Search for a stock: </h3>

    <form class="form-inline my-2 my-lg-0">
        <div style="width: 500px">
            <Select {loadOptions} placeholder="Asx code or company name" listPlacement="bottom"></Select>
        </div>
        <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
    </form>
</div>