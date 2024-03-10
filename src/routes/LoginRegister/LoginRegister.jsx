import { useParams } from "react-router-dom";
import Login from "../../components/Login/Login";
import Register from "../../components/Register/Register";
import "./LoginRegister.css";
import { AnimatePresence } from "framer-motion";

import { useState } from "react";
import NotFound from "../NotFound/NotFound";

const LoginRegister = () => {
    const { authtype } = useParams();

    const [option, setOption] = useState(authtype);

    if (!(option === "login" || option === "registrar")) {
        return <NotFound />;
    }

    return (
        <main className="Page">
            <section className="main-container">
                <AnimatePresence initial={false}>
                    {option === "login" ? (
                        <Login
                            setRegister={() => setOption("registrar")}
                        ></Login>
                    ) : (
                        <Register
                            setLogin={() => setOption("login")}
                        ></Register>
                    )}
                </AnimatePresence>
            </section>
        </main>
    );
};

export default LoginRegister;
