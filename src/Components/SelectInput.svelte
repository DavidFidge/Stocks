<script>
    import { updateValidationErrors, checkInputValidity } from '../api/validation.js'
    import Select from 'svelte-select';

    export let label;
    export let id;
    export let appendLabel = "";
    export let prependLabel = "";
    export let required = false;
    export let placeholder = "";
    export let loadOptions = null;
    export let items = null;
    export let selectedValue = null;
    export let errors = [];

    let errorMessages = [];
    let inputAttributes = { id: id };
    let isSelected = selectedValue;

    let selectInputValidity = function(inputValidation)
    {
        let inputElement = document.getElementById(inputValidation.id);

        inputElement.setCustomValidity("");

        inputValidation.errorMessages = [];

        if (inputValidation.required && !inputValidation.isSelected)
        {
            inputElement.setCustomValidity("valueMissing");
            inputValidation.errorMessages.push(`The ${inputValidation.label} field is required.`);
        }
    }

    let checkValidity = function()
    {
        let inputValidation = {
            id: id,
            label: label,
            responseErrors: errors,
            errorMessages: errorMessages,
            required: required,
            isSelected: isSelected
        };

        checkInputValidity(inputValidation, selectInputValidity);

        errorMessages = inputValidation.errorMessages;
    }

    let validitySelect = function()
    {
        isSelected = true;
        checkValidity();
    }

    let validityClear = function()
    {
        isSelected = false;
        checkValidity();
    }

$: {
    let inputValidation = {
            id: id,
            label: label,
            responseErrors: errors,
            errorMessages: errorMessages,
            required: required,
            isSelected: isSelected
        };

    updateValidationErrors(inputValidation, selectInputValidity);

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
    display: block;
}

.nowrap {
    flex-wrap: nowrap !important;
}

</style>

<label for={id}>{label}</label>
<div class="input-group nowrap mb-3">
    <div class="input-group-prepend errorIndicator {errorMessages.length > 0 ? 'active': ''}">*</div>
{#if prependLabel}
    <div class="input-group-prepend">
        <span class="input-group-text">{prependLabel}</span>
    </div>
{/if}
{#if loadOptions}
    <Select
        inputAttributes={inputAttributes}
        containerClasses="input-group test"
        {loadOptions}
        {placeholder}
        listPlacement="bottom"
        bind:selectedValue={selectedValue}
        on:select={validitySelect}
        on:clear={validityClear}
    ></Select>
{:else}
    <Select
        inputAttributes={inputAttributes}
        containerClasses="input-group"
        {items}
        {placeholder}
        listPlacement="bottom"
        bind:selectedValue={selectedValue}
        on:select={validitySelect}
        on:clear={validityClear}
    ></Select>   
{/if}
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

