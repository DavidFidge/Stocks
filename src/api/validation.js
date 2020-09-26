export function updateValidationErrors(inputValidation)
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
                    checkInputValidity(inputValidation);
                }
            }
            else
            {                
                checkInputValidity(inputValidation);
            }
        }
    }
}

export function checkInputValidity(inputValidation)
{
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

