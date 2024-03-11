import RegisterForm from "./RegisterForm";
import "./Register.css";

import { motion as m } from "framer-motion";

const Register = ({ setLogin }) => {
    return (
        <>
            <RegisterForm></RegisterForm>
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
                className="select-login"
            >
                <m.div
                    initial={{ opacity: 0 }}
                    animate={{ opacity: 1 }}
                    transition={{ delay: 0.5 }}
                    className="select-login"
                >
                    <h1 className="already-member">
                        Já possui cadastro e deseja realizar login?
                    </h1>
                    <button onClick={setLogin}>Login</button>
                </m.div>
            </m.section>
        </>
    );
};

export default Register;
