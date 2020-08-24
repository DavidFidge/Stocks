<script>
    import { Router, Route } from 'svelte-routing'
    import Navbar from './Navbar/Navbar.svelte'
    import Home from './Home/Home.svelte'
    import Stock from './Stock/Stock.svelte'
    import Portfolio from './Portfolio/Portfolio.svelte'
    import AddHolding from './Portfolio/AddHolding.svelte'
    import AddPortfolio from './Portfolio/AddPortfolio.svelte'
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
            <Route path="/portfolio">
                <Portfolio />
            </Route>
            <Route path="/portfolio/addholding">
                <AddHolding />
            </Route>
            <Route path="/portfolio/addportfolio">
                <AddPortfolio />
            </Route>           
        </div>
    </main>
</Router>
