import { httpGet } from './api.js'

export class StockApi
{
    getStocks() {
        return httpGet("/stock");
    }
}
