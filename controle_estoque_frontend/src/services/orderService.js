import { makeRequest } from "./requestManager";

const BASE_URL_BFF = "https://localhost:44372";

export const postBuyOrder = (order, navigateHook) => {
    return makeRequest(
        "post",
        `${BASE_URL_BFF}/api/movimentacao/pedido/compra`,
        navigateHook,
        order
    );
};

export const postSellOrder = (order, navigateHook) => {
    return makeRequest(
        "post",
        `${BASE_URL_BFF}/api/movimentacao/pedido/venda`,
        navigateHook,
        order
    );
};

export const postTransferOrder = (order, navigateHook) => {
    return makeRequest(
        "post",
        `${BASE_URL_BFF}/api/movimentacao/pedido/transferencia`,
        navigateHook,
        order
    );
};

const BASE_URL = "https://localhost:44339";

export const getAllOrders = (page, pageSize, navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/pedido?pageSize=${pageSize}&pageIndex=${page}`,
        navigateHook
    );
};

export const getOrderListByDate = (date, page, pageSize, navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/pedido?data=${date}&pageSize=${pageSize}&pageIndex=${page}`,
        navigateHook
    );
};

export const getBuyOrder = (id, navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/pedido/compra/${id}`,
        navigateHook
    );
};

export const getSellOrder = (id, navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/pedido/venda/${id}`,
        navigateHook
    );
};

export const getTransferOrder = (id, navigateHook) => {
    return makeRequest(
        "get",
        `${BASE_URL}/api/pedido/transferencia/${id}`,
        navigateHook
    );
};
