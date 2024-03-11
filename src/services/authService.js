import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";

export const postAuth = createAsyncThunk(
    "user/postAuth",
    async (user, { rejectWithValue }) => {
        try {
            const { data } = await axios.post(
                "http://localhost:3000/api/indentidade/autenticar",
                user
            );
            return data;
        } catch (error) {
            return rejectWithValue(error.response.data);
        }
    }
);
