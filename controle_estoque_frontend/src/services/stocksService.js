import { makeRequest } from "./requestManager";

const BASE_URL = "https://gde-estoque-api.fly.dev";

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

export const getStockListByName = (name, page, pageSize, navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/estoque/lista-por-nome?nome=${name}&pageSize=${pageSize}&pageIndex=${page}`,
        navigateHook
    );
};

export const getAllStocksListPaged = (page, pageSize, navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/estoque/listar-todos?pageSize=${pageSize}&pageIndex=${page}`,
        navigateHook
    );
};

export const postStock = (stock, navigateHook) => {
    return makeRequest("post", `${BASE_URL}/api/estoque/`, navigateHook, stock);
};

export const deleteStock = (id, navigateHook) => {
    return makeRequest("delete", `${BASE_URL}/api/estoque/${id}`, navigateHook);
};

export const updateStock = (id, stock, navigateHook) => {
    return makeRequest(
        "put",
        `${BASE_URL}/api/estoque/${id}`,
        navigateHook,
        stock
    );
};

const BASE_URL_BFF = "https://gde-bff-movimentacaoestoque.fly.dev";

export const getStock = (id, navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL_BFF}/api/estoque/${id}`,
        navigateHook
    );
};
