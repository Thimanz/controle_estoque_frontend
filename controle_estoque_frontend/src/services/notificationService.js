import { makeRequest } from "./requestManager";

const BASE_URL = "https://gde-bff-movimentacaoestoque.fly.dev";

export const getNotifications = (navigateHook) => {
    return makeRequest("get", `${BASE_URL}/api/notificacoes`, navigateHook);
};
