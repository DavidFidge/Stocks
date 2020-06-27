<script>
    export let label;
    export let id = "";
    export let appendLabel = "";
    export let prependLabel = "";
    export let value;
    export let multiline = false;

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
{#if multiline}
    <textarea {id} class="form-control" bind:value />
{:else}
    <input {id} class="form-control" type="text" bind:value />
{/if}
{#if appendLabel}
    <div class="input-group-append">
        <span class="input-group-text">{appendLabel}</span>
    </div>
{/if}
</div>
