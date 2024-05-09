import { makeRequest } from "./requestManager";

const BASE_URL = "http://localhost:44372";

export const postBuyOrder = (order, navigateHook) => {
    return makeRequest(
        "post",
        `${BASE_URL}/api/movimentacao/pedido/compra`,
        navigateHook,
        order
    );
};

export const postSellOrder = (order, navigateHook) => {
    return makeRequest(
        "post",
        `${BASE_URL}/api/movimentacao/pedido/venda`,
        navigateHook,
        order
    );
};

export const postTransferOrder = (order, navigateHook) => {
    return makeRequest(
        "post",
        `${BASE_URL}/api/movimentacao/pedido/transferencia`,
        navigateHook,
        order
    );
};
