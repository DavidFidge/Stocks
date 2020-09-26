<script>
    import { updateValidationErrors, checkInputValidity } from '../api/validation.js'

    export let label;
    export let id = "";
    export let appendLabel = "";
    export let prependLabel = "";
    export let value;
    export let required = false;
    export let min = "0"
    export let errors = {};

    let errorMessages = [];

    let checkValidity = function()
    {
        let inputValidation = {
            id: id,
            label: label,
            responseErrors: errors,
            errorMessages: errorMessages
        };

        checkInputValidity(inputValidation);

        errorMessages = inputValidation.errorMessages;
    }

$: {
    let inputValidation = {
            id: id,
            label: label,
            responseErrors: errors,
            errorMessages: errorMessages
        };

    updateValidationErrors(inputValidation);

    errorMessages = inputValidation.errorMessages;
    errors = [];
}

</script>

<style>

.errorMessage {
    padding: 0px;
}

.errorMessageItem {
    padding: 0px;
    color:  rgb(150, 0, 0);
}

.errorIndicator {
    color:  rgb(150, 0, 0);
    display: none;
}

.errorIndicator.active {
    display: inline;
}

</style>

<label for={id}>{label}</label>
<div class="input-group mb-3">
    <span class="errorIndicator {errorMessages.length > 0 ? 'active': ''}">*</span>
{#if prependLabel}
    <div class="input-group-prepend">
        <span class="input-group-text">{prependLabel}</span>
    </div>
{/if}
    <input
        {id}
        required={required}
        class="form-control"
        type="number"
        min={min}
        step="1"
        bind:value
        on:change={checkValidity}
	    on:input={checkValidity}
    />
{#if appendLabel}
    <div class="input-group-append">
        <span class="input-group-text">{appendLabel}</span>
    </div>
{/if}
</div>
<div class="errorMessage">
    {#each errorMessages as errorMessage}
    <div class="errorMessageItem">
        {errorMessage}
    </div>
    {/each}
</div>
