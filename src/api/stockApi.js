import { httpGet } from './api.js'

export class StockApi
{
    getStocks(filterText) {
        return httpGet(`/stock?$filter=indexof(Code, '${filterText.toUpperCase()}') gt -1`);
    }
}
