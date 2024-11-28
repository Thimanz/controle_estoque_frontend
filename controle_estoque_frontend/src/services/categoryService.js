import { makeRequest } from "./requestManager";

const BASE_URL = "https://gde-produtos-api.fly.dev";

export const getCategoryList = (navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/produto/categorias`,
        navigateHook
    );
};
