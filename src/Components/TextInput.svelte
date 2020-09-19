<script>
    export let label;
    export let id;
    export let appendLabel = "";
    export let prependLabel = "";
    export let value;
    export let multiline = false;
    export let required = false;
    export let errors = {};

    let errorMessages = [];

    let checkValidity = function()
    {
        inputElement.setCustomValidity("");

        if (!inputElement.validity.valid)
        {
            if (inputElement.validity.valueMissing)
            {
                errorMessages.push(`${label} is required`);
            }
            else
            {
                errorMessages.push(`Input errors`);
            }
        }
    }

$: {
    if (id)
    {
        let inputElement = document.getElementById(id);

        if (inputElement)
        {
            let errorMessagesForElement = errors[id];

            if (errorMessagesForElement)
            { 
                inputElement.setCustomValidity("Server errors");
                errorMessages = errorMessagesForElement;
            }
            else
            {
                checkValidity();
            }
        }
    }
}

</script>

<style>

.errorMessage {
    border: 0px;
    padding: 0px;
}

.errorMessage.active {
    background-color:  red;
}

</style>

<label for={id}>{label}</label>
<div class="input-group mb-3">
{#if prependLabel}
    <div class="input-group-prepend">
        <span class="input-group-text">{prependLabel}</span>
    </div>
{/if}
{#if multiline}
    <textarea {id} required={required} class="form-control" bind:value />
{:else}
    <input {id} required={required} class="form-control" type="text" bind:value />
{/if}
{#if appendLabel}
    <div class="input-group-append">
        <span class="input-group-text">{appendLabel}</span>
    </div>
{/if}
</div>
<div id="{id}ErrorMessage" class="errorMessage {errorMessages.length > 0 ? 'active': ''}">
    {#each errorMessages as errorMessage}
    <div>
        {errorMessage}
    </div>
    {/each}
</div>

