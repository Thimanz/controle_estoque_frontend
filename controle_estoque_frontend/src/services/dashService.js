import { makeRequest } from "./requestManager";

const BASE_URL = "https://gde-bff-movimentacaoestoque.fly.dev";

export const getProfitData = (navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/relatorio/vendas-custos/5`,
        navigateHook
    );
};

export const getOccupationData = (navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/relatorio/produtos-maior-quantidade`,
        navigateHook
    );
};

export const getBestSellersData = (navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/relatorio/top10-produtos`,
        navigateHook
    );
};

export const getMostOccupiedData = (navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/relatorio/ocupacao-estoques`,
        navigateHook
    );
};
