export function updateValidationErrors(inputValidation, customInputValidation = undefined)
{
    if (inputValidation.id)
    {
        let inputElement = document.getElementById(inputValidation.id);

        if (inputElement)
        {
            if (inputValidation.responseErrors)
            {
                let inputElementErrors = inputValidation.responseErrors[inputValidation.id];

                if (inputElementErrors)
                { 
                    inputElement.setCustomValidity("Server errors");
                    inputValidation.errorMessages = inputElementErrors;
                }
                else
                {
                    checkInputValidity(inputValidation, customInputValidation);
                }
            }
            else
            {                
                checkInputValidity(inputValidation, customInputValidation);
            }
        }
    }
}

export function checkInputValidity(inputValidation, customInputValidation = undefined)
{
    if (customInputValidation)
    {
        customInputValidation(inputValidation);
        return;
    }

    let inputElement = document.getElementById(inputValidation.id);

    inputElement.setCustomValidity("");

    inputValidation.errorMessages = [];

    if (!inputElement.validity.valid)
    {
        if (inputElement.validity.valueMissing)
        {
            inputValidation.errorMessages.push(`The ${inputValidation.label} field is required.`);
        }
        else
        {
            inputValidation.errorMessages.push(`Input errors.`);
        }
    }
}

