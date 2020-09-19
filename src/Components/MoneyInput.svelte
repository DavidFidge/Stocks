<script>
    export let label;
    export let id = "";
    export let appendLabel = "";
    export let prependLabel = "";
    export let value;
    export let required = false;
    export let min = "0.00"

    if (!id)
    {
        id = label.replace(/\W/g, '');
    }

</script>
 
<label for={id}>{label}</label>
<div class="input-group mb-3">
{#if prependLabel}
    <div class="input-group-prepend">
        <span class="input-group-text">{prependLabel}</span>
    </div>
{/if}
    <input {id} required={required} class="form-control" type="number" min={min} step="0.01" bind:value />
{#if appendLabel}
    <div class="input-group-append">
        <span class="input-group-text">{appendLabel}</span>
    </div>
{/if}
</div>