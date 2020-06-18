import { httpGet } from './api.js'

export class StockApi
{
    getStocks(filterText) {
        return httpGet(`/api/Stocks?$filter=indexof(Code, '${filterText.toUpperCase()}') gt -1`);
    }
}
