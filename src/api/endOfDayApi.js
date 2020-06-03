import { httpGet } from './api.js'

export class EndOfDayApi
{
    getEndOfDay(filterText) {
        return httpGet(`/endofday?$filter=Stock/Code eq '${filterText}'`);
    }
}
