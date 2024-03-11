import { configureStore } from "@reduxjs/toolkit";
import authResponseSlice from "./features/authResponse/authResponseSlice";

const store = configureStore({
    reducer: {
        authResponse: authResponseSlice.reducer,
    },
});

export default store;
