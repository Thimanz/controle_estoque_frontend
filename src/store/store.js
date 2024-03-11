import { configureStore } from "@reduxjs/toolkit";
import loginResponseSlice from "./features/loginResponse/loginResponseSlice";

const store = configureStore({
    reducer: {
        loginResponse: loginResponseSlice.reducer,
    },
});

export default store;
