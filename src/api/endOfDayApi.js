import { httpGet } from './api.js'

export class EndOfDayApi
{
    getEndOfDay() {
        return httpGet("/endOfDay");
    }
}
