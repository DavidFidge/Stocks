<script>
    import { links } from 'svelte-routing'
    import Spinner from 'svelte-spinner'
    export let to = ""
    export let showSpinner = false
    export let disabled = false
</script>

<style>

.spinner {
    display: none;
}

.spinner.active {
    display: inline;
}
</style>

{#if to}
<a href={to} on:click use:links class="btn btn-secondary">
    <slot></slot>
</a>
{:else}
<button 
    on:click class="btn btn-secondary"
    disabled={disabled}
    >
    <slot></slot>
</button>
{/if}

<div class="spinner {showSpinner ? 'active' : ''}">
    <Spinner
        size="50"
        speed="1000"
        color="#6c757d"
        thickness="4"
        gap="30"
    />
</div>
