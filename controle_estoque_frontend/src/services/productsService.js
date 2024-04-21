import { makeRequest } from "./requestManager";

export const getProductList = (name, navigateHook) => {
    return makeRequest(
        "get",
        `http://localhost:5101/api/produto/lista-por-nome/${name}`,
        navigateHook
    );
};

export const getProduct = (id, navigateHook) => {
    return makeRequest(
        "get",
        `http://localhost:5101/api/produto/${id}`,
        navigateHook
    );
};

export const postProduct = (product, navigateHook) => {
    return makeRequest(
        "post",
        "http://localhost:5101/api/produto/",
        navigateHook,
        product
    );
};

export const deleteProduct = (id, navigateHook) => {
    return makeRequest(
        "delete",
        `http://localhost:5101/api/produto/${id}`,
        navigateHook
    );
};

export const updateProduct = (id, product, navigateHook) => {
    return makeRequest(
        "put",
        `http://localhost:5101/api/produto/${id}`,
        navigateHook,
        product
    );
};
