<script>
    import { links } from 'svelte-routing';

    /*$: {

        console.log(url);
        let navbarItems = window.$("#navbarSupportedContent li a");

         for (let navbarItem of navbarItems)
         {
            let pattern = new RegExp(navbarItem.html().toLower());

            let match = url.test(pattern);

            if (match && navbarItem.hasClass("active"))
            {
                navbarItems.removeClass("active");
                navbarItem.addClass("active");
                break;
            }
         }
    }*/
</script>

<style>
</style>
<p>{url}</p>
<nav class="navbar navbar-expand-lg navbar-dark bg-dark" use:links>
    <a class="navbar-brand" href="/"><img src="./img/stock.png" class="mr-1" alt="Stocks"/>Portfolio Manager</a>
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
    </button>
    
    <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav mr-auto">
        <li class="nav-item active">
            <a class="nav-link" href="/">Home {url}</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" href="/portfolio">Portfolio</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" href="/stocks">Stocks</a>
            </li>            
        </ul>
    </div>
</nav>
