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
        let inputElement = document.getElementById(id);

        inputElement.setCustomValidity("");
        errorMessages = [];

        if (!inputElement.validity.valid)
        {
            if (inputElement.validity.valueMissing)
            {
                errorMessages.push(`The ${label} field is required.`);
            }
            else
            {
                errorMessages.push(`Input errors.`);
            }
        }
    }

$: {
    if (id)
    {
        let inputElement = document.getElementById(id);

        if (inputElement)
        {
            if (errors)
            {
                let errorMessagesForElement = errors[id];

                if (errorMessagesForElement)
                { 
                    inputElement.setCustomValidity("Server errors");
                    errorMessages = errorMessagesForElement;
                }
                else
                {
                    checkValidity(inputElement);
                }
            }
            else
            {                
                checkValidity(inputElement);
            }
        }
    }
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
{#if multiline}
    <textarea
        {id}
        required={required}
        class="form-control"
        bind:value
        on:change={checkValidity}
	    on:input={checkValidity}
    />
{:else}
    <input 
        {id} 
        required={required}
        class="form-control"
        type="text"
        bind:value 
        on:change={checkValidity}
	    on:input={checkValidity}
    />
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

