import LoginForm from "./LoginForm";
import "./Login.css";

import { motion as m } from "framer-motion";

const Login = ({ setRegister }) => {
    return (
        <>
            <LoginForm></LoginForm>
            <m.section
                initial={{
                    x: "-95%",
                    style: {
                        border: "5%",
                    },
                }}
                animate={{
                    x: 0,
                    style: {
                        borderBottomRightRadius: "none",
                        borderTopRightRadius: "none",
                    },
                }}
                transition={{
                    duration: 1,
                    bounce: 0.2,
                    stiffness: 30,
                    type: "spring",
                }}
                className="select-register"
            >
                <m.div
                    initial={{ opacity: 0 }}
                    animate={{ opacity: 1 }}
                    transition={{ delay: 0.5 }}
                    className="select-register"
                >
                    <h1 className="new-member">
                        É um membro novo e ainda não possui cadastro?
                    </h1>
                    <button onClick={setRegister}>Cadastrar-se</button>
                </m.div>
            </m.section>
        </>
    );
};

export default Login;
