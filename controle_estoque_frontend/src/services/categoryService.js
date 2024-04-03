import { makeRequest } from "./requestManager";

export const getCategoryList = (navigateHook) => {
    return makeRequest(
        "get",
        `http://localhost:5101/api/produto/categorias`,
        navigateHook
    );
};
