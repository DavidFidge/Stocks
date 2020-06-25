<script>
    export let label;
    export let shadowLabel = "";
    export let value;
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
        <input class:vertical type="number" min="0" step="1" bind:value />
    {#if shadowLabel}
        <span>{shadowLabel}</span>
    {/if}
    </label>
</div>