import { useRef, useState, useEffect } from "react";
import {
    faCheck,
    faTimes,
    faInfoCircle,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import validator from "validator";

import { motion as m } from "framer-motion";

import { postAuth } from "../../services/authService";
import { useDispatch } from "react-redux";

const LoginForm = () => {
    const userRef = useRef();
    const errRef = useRef();

    const [email, setEmail] = useState("");
    const [validEmail, setValidEmail] = useState(false);
    const [userFocus, setUserFocus] = useState(false);

    const [pwd, setPwd] = useState("");
    const [validPwd, setValidPwd] = useState(false);
    const [pwdFocus, setPwdFocus] = useState(false);

    const [errorMsgs, setErrorMsgs] = useState([]);

    useEffect(() => {
        userRef.current.focus();
    }, []);

    const validateEmail = (email) => {
        setValidEmail(validator.isEmail(email));
    };

    const validatePwd = (password) => {
        setValidPwd(validator.isStrongPassword(password));
    };

    const dispatch = useDispatch();

    const sendLogin = async () => {
        if (!(validEmail && validPwd)) {
            setErrorMsgs(["Há campos a serem corrigidos"]);
            return;
        }
        try {
            const loginResponse = await dispatch(
                postAuth({
                    email,
                    senha: pwd,
                })
            ).unwrap();
            localStorage.setItem("accessToken", loginResponse.accessToken);
            localStorage.setItem("refreshToken", loginResponse.refreshToken);
            setErrorMsgs([]);
        } catch (error) {
            setErrorMsgs(error.erros.mensagens);
        }
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
                        onChange={(e) => validateEmail(e.target.value)}
                        required
                        aria-invalid={validEmail ? "false" : "true"}
                        aria-describedby="uidnote"
                        onFocus={() => setUserFocus(true)}
                        onBlur={(e) => {
                            setUserFocus(false);
                            setEmail(e.target.value);
                        }}
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
                        onChange={(e) => validatePwd(e.target.value)}
                        required
                        aria-invalid={validEmail ? "false" : "true"}
                        aria-describedby="uidnote"
                        onFocus={() => setPwdFocus(true)}
                        onBlur={(e) => {
                            setPwdFocus(false);
                            setPwd(e.target.value);
                        }}
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
                    <button className="login-btn" onClick={sendLogin}>
                        Login
                    </button>
                    {errorMsgs.map((msg, index) => (
                        <p key={index} className="errMsg" aria-live="assertive">
                            {msg}
                        </p>
                    ))}
                </div>
            </m.div>
        </m.section>
    );
};

export default LoginForm;
