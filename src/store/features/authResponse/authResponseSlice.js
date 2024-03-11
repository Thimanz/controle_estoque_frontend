import { createSlice } from "@reduxjs/toolkit";
import { postAuth, postNewUserAccount } from "../../../services/authService";

const authResponseSlice = createSlice({
    name: "user",
    initialState: {
        data: {},
        isSuccess: false,
        error: {},
    },
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(postAuth.rejected, (state, { payload }) => {
            state.isSuccess = false;
            state.data = {};
            state.error = payload;
        });
        builder.addCase(postAuth.fulfilled, (state, { payload }) => {
            state.isSuccess = true;
            state.error = {};
            state.data = payload;
        });
        builder.addCase(postNewUserAccount.rejected, (state, { payload }) => {
            state.isSuccess = false;
            state.data = {};
            state.error = payload;
        });
        builder.addCase(postNewUserAccount.fulfilled, (state, { payload }) => {
            state.isSuccess = true;
            state.error = {};
            state.data = payload;
        });
    },
});

export default authResponseSlice;
