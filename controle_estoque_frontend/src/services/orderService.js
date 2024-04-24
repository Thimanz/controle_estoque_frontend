import { makeRequest } from "./requestManager";

const BASE_URL = "http://localhost:5101";

export const postOrder = (order, navigateHook) => {
    return makeRequest("post", `${BASE_URL}/api/pedido`, navigateHook, order);
};
