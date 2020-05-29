<script>

    import Select from 'svelte-select';

    const stocks = {
        stocks:
        [ 
            {
                Code: 'ASX',
                Description: 'Australian Securities Exchange',
                Open: 1.0,
                High: 1.5,
                Low: 0.9,
                Close: 1.1,
                Volume: 10000
            },
            {
                Code: 'CSL',
                Description: 'CSL Ltd',
                Open: 100.0,
                High: 100.5,
                Low: 100.9,
                Close: 100.1,
                Volume: 20000
            }
        ]
    };

    const items = window.$.map(stocks.stocks, function (v) {
        return {
            value: v.Code,
            label: v.Code.concat(' - ', v.Description)
        };
    });
    
</script>
<style>

</style>

<div>
     
    <h3>Search for a stock: </h3>

    <form class="form-inline my-2 my-lg-0">
        <div style="width: 500px">
            <Select {items} placeholder="Asx code or company name"></Select>
        </div>
        <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
    </form>

</div>