<script>
    export let label;
    export let id = "";
    export let appendLabel = "";
    export let prependLabel = "";
    export let value;

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
    <input {id} class="form-control" type="number" min="0" step="1" bind:value />
{#if appendLabel}
    <div class="input-group-append">
        <span class="input-group-text">{appendLabel}</span>
    </div>
{/if}
</div>