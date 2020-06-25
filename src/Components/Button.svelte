<script>
    import { links } from 'svelte-routing'
    export let to = ""
</script>

<style>
</style>

{#if to}
<a href={to} on:click use:links class="btn btn-secondary">
    <slot></slot>
</a>
{:else}
<button type="button" class="btn btn-secondary">
    <slot></slot>
</button>
{/if}