import Login from "../../components/Login/Login";
import Register from "../../components/Register/Register";
import "./LoginRegister.css";

import { useState } from "react";

const LoginRegister = () => {
    const [option, setOption] = useState("Login");

    return (
        <main className="Page">
            <section className="main-container">
                {option === "Login" ? (
                    <Login setRegister={() => setOption("Register")}></Login>
                ) : (
                    <Register setLogin={() => setOption("Login")}></Register>
                )}
            </section>
        </main>
    );
};

export default LoginRegister;
