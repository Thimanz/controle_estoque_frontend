import { makeRequest } from "./requestManager";

const BASE_URL = "https://localhost:44323";

export const getNotifications = (navigateHook) => {
    return makeRequest("get", `${BASE_URL}/api/notificacoes`, navigateHook);
};
