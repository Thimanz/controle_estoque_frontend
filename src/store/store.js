import { configureStore } from "@reduxjs/toolkit";
import loginResponseSlice from "./features/loginResponse/loginResponseSlice";

const store = configureStore({
    reducer: {
        user: loginResponseSlice.reducer,
    },
});

export default store;
