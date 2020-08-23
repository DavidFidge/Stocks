import { httpGet } from './api.js'

export class EndOfDayApi
{
    getEndOfDay(filterText) {
        return httpGet(`/api/EndOfDay?$filter=StockCode eq '${filterText}'&$orderby=date`);
    }
}
