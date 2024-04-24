import { makeRequest } from "./requestManager";

const BASE_URL = "http://localhost:5101";

export const getCategoryList = (navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/produto/categorias`,
        navigateHook
    );
};
