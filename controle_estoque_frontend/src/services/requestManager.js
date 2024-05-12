import axios from "axios";

export const makeRequest = async (
    method,
    url,
    navigateHook,
    payload = null
) => {
    const headers = {
        Authorization: "Bearer " + localStorage.getItem("accessToken"),
    };

    if (payload) {
        try {
            const { data, status } = await axios({
                method,
                url,
                headers,
                data: payload,
            });
            return { data, status };
        } catch (error) {
            if (error.response.status === 401) {
                navigateHook("/autenticar/login");
            }
            return error.response.data;
        }
    }

    try {
        const { data, status } = await axios({ method, url, headers });
        return { data, status };
    } catch (error) {
        if (error.response.status === 401) {
            navigateHook("/autenticar/login");
        }
        return error.response.data;
    }
};
