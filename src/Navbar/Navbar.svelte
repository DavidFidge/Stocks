<script>
    import { links } from 'svelte-routing';

    import { onMount } from 'svelte';

    let updateActiveNavbarItem = function()
    {
        let pathname = window.location.pathname;

        if (pathname === '/')
            pathname = '/home';

        let navbarItems = window.$("#navbarSupportedContent li a");

        for (let navbarItem of navbarItems)
        {
            let pattern = new RegExp(window.$(navbarItem).text().toLowerCase());

            let match = pattern.test(pathname);

            if (match && !window.$(navbarItem).hasClass("active"))
            {
                window.$(navbarItems).removeClass("active");
                window.$(navbarItem).addClass("active");
                window.$(navbarItems).blur();

                break;
            }
        }
    
        setTimeout(updateActiveNavbarItem, 100);
    };

    onMount(() => 
    {
        // The only way to detect changes to window.location seems to be to poll.
        // An alternative way would be to plumb it in the routing system or a link component
        // as well as handling browser back and forward.  At the moment polling is a lot simpler.
        // https://stackoverflow.com/questions/3522090/event-when-window-location-href-changes/33668370
        setTimeout(updateActiveNavbarItem, 100);
    });

</script>

<nav class="navbar navbar-expand-lg navbar-dark bg-dark" use:links>
    <a class="navbar-brand" href="/"><img src="/img/stock.png" class="mr-1" alt="Stocks"/>Portfolio Manager</a>
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
    </button>
    
    <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav mr-auto">
        <li class="nav-item">
            <a class="nav-link" href="/">Home</a>
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
