<script>
    export let label;
    export let shadowLabel = "";
    export let value;
    export let multiline = false;
    export let vertical = true;
</script>
 
<style>
    .vertical {
        width: 100%;
    }
</style>

<div>
    <label>
        <span>{label}</span>
    {#if multiline}
        <textarea class:vertical bind:value />
    {:else}
        <input class:vertical type="text" bind:value />
    {/if}
    {#if shadowLabel}
        <span>{shadowLabel}</span>
    {/if}
    </label>
</div>