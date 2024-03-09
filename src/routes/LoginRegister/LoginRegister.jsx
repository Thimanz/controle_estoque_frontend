import Login from "../../components/Login/Login";
import Register from "../../components/Register/Register";
import "./LoginRegister.css";
import { AnimatePresence } from "framer-motion";

import { useState } from "react";

const LoginRegister = () => {
    const [option, setOption] = useState("Login");

    return (
        <main className="Page">
            <section className="main-container">
                <AnimatePresence initial={false}>
                    {option === "Login" ? (
                        <Login
                            setRegister={() => setOption("Register")}
                        ></Login>
                    ) : (
                        <Register
                            setLogin={() => setOption("Login")}
                        ></Register>
                    )}
                </AnimatePresence>
            </section>
        </main>
    );
};

export default LoginRegister;
