import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";

export const postAuth = createAsyncThunk(
    "user/postAuth",
    async (login, { rejectWithValue }) => {
        try {
            const { data } = await axios.post(
                "http://localhost:5101/api/identidade/autenticar",
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
                "http://localhost:5101/api/identidade/nova-conta",
                user
            );
            return data;
        } catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);
