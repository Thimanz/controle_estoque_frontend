import { makeRequest } from "./requestManager";

const BASE_URL = "https://localhost:44341";

export const getStocksListByProductId = (id, navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/estoque/obter-lista-por-produto-id/${id}`,
        navigateHook
    );
};

export const getAllStocksList = (navigateHook) => {
    return makeRequest("get", `${BASE_URL}/api/estoque`, navigateHook);
};
