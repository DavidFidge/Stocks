<script>
    import { Router, Route } from 'svelte-routing'
    import Navbar from './Navbar/Navbar.svelte'
    import Home from './Home/Home.svelte'
    import Stock from './Stock/Stock.svelte'

    import Portfolio from './Portfolio/Portfolio.svelte'
    import AddPortfolio from './Portfolio/AddPortfolio.svelte'

    import Holding from './Holding/Holding.svelte'
    import AddHolding from './Holding/AddHolding.svelte'

    import Dividend from './Dividend/Dividend.svelte'
    import AddDividend from './Dividend/AddDividend.svelte'

    export let url = ""
</script>

<Router {url}>
    <main>
        <Navbar />
        <div id="content" class="container-fluid pt-3 pb-3">
            <Route path="/">
                <Home />
            </Route>
            <Route path="/stocks">
                <Stock />
            </Route>
            <Route path="/portfolios">
                <Portfolio />
            </Route>
            <Route path="/holdings">
                <Holding />
            </Route>
            <Route path="/dividends">
                <Dividend />
            </Route>
            <Route path="/portfolios/addportfolio">
                <AddPortfolio />
            </Route>
            <Route path="/holdings/addholding">
                <AddHolding />
            </Route>
            <Route path="/dividends/adddividend">
                <AddDividend />
            </Route>         
        </div>
    </main>
</Router>
