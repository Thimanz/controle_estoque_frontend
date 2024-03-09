import RegisterForm from "./RegisterForm";
import "./Register.css";

import { motion as m } from "framer-motion";

const Register = ({ setLogin }) => {
    return (
        <>
            <RegisterForm></RegisterForm>
            <m.section
                initial={{
                    x: -300,
                    style: {
                        borderBottomRightRadius: "5%",
                        borderTopRightRadius: "5%",
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
                <h1 className="already-member">
                    Já possui cadastro e deseja realizar login?
                </h1>
                <button onClick={setLogin}>Login</button>
            </m.section>
        </>
    );
};

export default Register;
