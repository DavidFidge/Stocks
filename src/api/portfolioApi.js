import { httpGet, httpPost } from './api.js'

export class PortfolioApi
{
    addPortfolio(portfolio) {
        return httpPost(`/api/Portfolio`, portfolio);
    }

    addHolding(holding) {
        return httpPost(`/api/Holding`, holding);
    }

    getPortfolios() {
        return httpGet(`/api/Portfolio/Portfolios`);
    }

    getHoldings() {
        return httpGet(`/api/Holding/Holdings`);
    }
}
