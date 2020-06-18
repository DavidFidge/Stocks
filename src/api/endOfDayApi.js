import { httpGet } from './api.js'

export class EndOfDayApi
{
    getEndOfDay(filterText) {
        return httpGet(`/api/EndOfDay?$filter=Stock/Code eq '${filterText}'&$orderby=date`);
    }
}
