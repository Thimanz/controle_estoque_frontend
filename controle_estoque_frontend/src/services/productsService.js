import { makeRequest } from "./requestManager";

const BASE_URL = "https://localhost:44323";

export const getProductListByName = (name, page, pageSize, navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/produto/lista-por-nome?nome=${name}&pageSize=${pageSize}&pageIndex=${page}`,
        navigateHook
    );
};

export const getAllProductList = (page, pageSize, navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/produto/listar-todos?pageSize=${pageSize}&pageIndex=${page}`,
        navigateHook
    );
};

export const getProduct = (id, navigateHook) => {
    return makeRequest("get", `${BASE_URL}/api/produto/${id}`, navigateHook);
};

export const postProduct = (product, navigateHook) => {
    return makeRequest(
        "post",
        `${BASE_URL}/api/produto/`,
        navigateHook,
        product
    );
};

export const deleteProduct = (id, navigateHook) => {
    return makeRequest("delete", `${BASE_URL}/api/produto/${id}`, navigateHook);
};

export const updateProduct = (id, product, navigateHook) => {
    return makeRequest(
        "put",
        `${BASE_URL}/api/produto/${id}`,
        navigateHook,
        product
    );
};
