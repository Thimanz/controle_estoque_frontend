import { makeRequest } from "./requestManager";

const BASE_URL = "http://localhost:5101";

export const getStocksListByProductId = (id, navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/estoque/obter-lista-por-produto-id/${id}`,
        navigateHook
    );
};

export const getAllStocksList = (navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/estoque/listar-todos`,
        navigateHook
    );
};
