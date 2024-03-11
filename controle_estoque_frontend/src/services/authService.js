import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";

export const postAuth = createAsyncThunk(
    "user/postAuth",
    async (login, { rejectWithValue }) => {
        try {
            const { data } = await axios.post(
                "http://localhost:3000/api/indentidade/autenticar",
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
                "http://localhost:3000/api/indentidade/nova-conta",
                user
            );
            return data;
        } catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);
