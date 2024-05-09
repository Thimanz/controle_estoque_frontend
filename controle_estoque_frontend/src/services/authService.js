import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";

const BASE_URL = "https://localhost:44396";

export const postAuth = createAsyncThunk(
    "user/postAuth",
    async (login, { rejectWithValue }) => {
        try {
            const { data } = await axios.post(
                `${BASE_URL}/api/identidade/autenticar`,
                login
            );
            return data;
        } catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);

export const postNewUserAccount = createAsyncThunk(
    "user/postNewAccount",
    async (user, { rejectWithValue }) => {
        try {
            const { data } = await axios.post(
                `${BASE_URL}/api/identidade/nova-conta`,
                user
            );
            return data;
        } catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);
