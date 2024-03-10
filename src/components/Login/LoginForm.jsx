import { useRef, useState, useEffect } from "react";
import {
    faCheck,
    faTimes,
    faInfoCircle,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import validator from "validator";

import { motion as m } from "framer-motion";

const LoginForm = () => {
    const userRef = useRef();
    const errRef = useRef();

    const [validEmail, setValidEmail] = useState(false);
    const [userFocus, setUserFocus] = useState(false);

    const [validPwd, setValidPwd] = useState(false);
    const [pwdFocus, setPwdFocus] = useState(false);

    useEffect(() => {
        userRef.current.focus();
    }, []);

    const validateEmail = (e) => {
        const email = e.target.value;
        setValidEmail(validator.isEmail(email));
    };

    const validatePwd = (e) => {
        const pwd = e.target.value;
        setValidPwd(validator.isStrongPassword(pwd));
    };

    return (
        <m.section
            initial={{
                x: "95%",
                style: {
                    border: "5%",
                },
            }}
            animate={{
                x: 0,
                style: {
                    borderBottomLeftRadius: "none",
                    borderTopLeftRadius: "none",
                },
            }}
            transition={{
                duration: 1,
                bounce: 0.2,
                stiffness: 30,
                type: "spring",
            }}
            className="login-area"
        >
            <m.div
                initial={{ opacity: 0 }}
                animate={{ opacity: 1 }}
                transition={{ delay: 0.5 }}
                className="login-area"
            >
                <h1>Faça login com as suas credenciais</h1>
                <form>
                    <label htmlFor="email">
                        E-mail:&nbsp;
                        <FontAwesomeIcon
                            icon={faCheck}
                            className={validEmail ? "valid" : "hide"}
                        />
                        <FontAwesomeIcon
                            icon={faTimes}
                            className={validEmail ? "hide" : "invalid"}
                        />
                    </label>
                    <input
                        type="text"
                        id="email"
                        ref={userRef}
                        autoComplete="off"
                        onChange={validateEmail}
                        required
                        aria-invalid={validEmail ? "false" : "true"}
                        aria-describedby="uidnote"
                        onFocus={() => setUserFocus(true)}
                        onBlur={() => setUserFocus(false)}
                    />
                    <p
                        id="uidnote"
                        className={
                            userFocus && !validEmail
                                ? "instructions"
                                : "offscreen"
                        }
                    >
                        <FontAwesomeIcon icon={faInfoCircle} />
                        Escreva um e-mail válido.
                    </p>
                </form>
                <form>
                    <label htmlFor="pwd">
                        Senha:&nbsp;
                        <FontAwesomeIcon
                            icon={faCheck}
                            className={validPwd ? "valid" : "hide"}
                        />
                        <FontAwesomeIcon
                            icon={faTimes}
                            className={validPwd ? "hide" : "invalid"}
                        />
                    </label>
                    <input
                        type="password"
                        id="password"
                        autoComplete="off"
                        onChange={validatePwd}
                        required
                        aria-invalid={validEmail ? "false" : "true"}
                        aria-describedby="uidnote"
                        onFocus={() => setPwdFocus(true)}
                        onBlur={() => setPwdFocus(false)}
                    />
                    <p
                        id="uidnote"
                        className={
                            pwdFocus && !validPwd ? "instructions" : "offscreen"
                        }
                    >
                        <FontAwesomeIcon icon={faInfoCircle} />A senha deve ter
                        no mínimo 8 caracteres, <br /> caracter especial e letra
                        maiúscula
                    </p>
                </form>
                <div className="lower-form">
                    <button className="login-btn">Login</button>
                    <p
                        ref={errRef}
                        className={
                            validEmail && validPwd ? "offscreen" : "errMsg"
                        }
                        aria-live="assertive"
                    >
                        Login incorreto
                    </p>
                </div>
            </m.div>
        </m.section>
    );
};

export default LoginForm;
