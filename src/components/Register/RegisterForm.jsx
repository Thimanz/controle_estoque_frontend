import { useRef, useState, useEffect } from "react";
import {
    faCheck,
    faTimes,
    faInfoCircle,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import validator from "validator";
import isValidCpf from "../../utils/isValidCpf";

import InputMask from "react-input-mask";

import { motion as m } from "framer-motion";

const RegisterForm = () => {
    const userRef = useRef();
    const errRef = useRef();

    const [validEmail, setValidEmail] = useState(false);
    const [emailFocus, setEmailFocus] = useState(false);

    const [validCpf, setValidCpf] = useState(false);
    const [cpfFocus, setCpfFocus] = useState(false);

    const [pwd, setPwd] = useState("");
    const [validPwd, setValidPwd] = useState(false);
    const [pwdFocus, setPwdFocus] = useState(false);

    const [matchPwd, setMatchPwd] = useState("");
    const [validMatch, setValidMatch] = useState(false);
    const [matchFocus, setMatchFocus] = useState(false);

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
        setValidMatch(matchPwd === pwd && validator.isStrongPassword(matchPwd));
    };

    const validateMatch = (e) => {
        const matchPwd = e.target.value;
        setValidMatch(matchPwd === pwd && validator.isStrongPassword(matchPwd));
    };

    const validateCPF = (e) => {
        const cpfStr = e.target.value;
        const cpf = cpfStr.replace(/[^0-9]/g, "");
        setValidCpf(isValidCpf(cpf));
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
            className="register-area"
        >
            <m.div
                initial={{ opacity: 0 }}
                animate={{ opacity: 1 }}
                transition={{ delay: 0.5 }}
                className="register-area"
            >
                <h1>Cadastre-se</h1>
                <form>
                    <label htmlFor="name">Nome:</label>
                    <input
                        type="text"
                        id="name"
                        ref={userRef}
                        autoComplete="off"
                        required
                    />
                </form>
                <div className="sectioned-forms">
                    <div className="left-form">
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
                                autoComplete="off"
                                onChange={validateEmail}
                                required
                                onFocus={() => setEmailFocus(true)}
                                onBlur={() => setEmailFocus(false)}
                            />
                            <p
                                className={
                                    emailFocus && !validEmail
                                        ? "instructions"
                                        : "offscreen"
                                }
                            >
                                <FontAwesomeIcon icon={faInfoCircle} />
                                Escreva um e-mail válido.
                            </p>
                        </form>
                        <form>
                            <label htmlFor="cpf">
                                CPF:&nbsp;
                                <FontAwesomeIcon
                                    icon={faCheck}
                                    className={validCpf ? "valid" : "hide"}
                                />
                                <FontAwesomeIcon
                                    icon={faTimes}
                                    className={validCpf ? "hide" : "invalid"}
                                />
                            </label>
                            <InputMask
                                mask="999.999.999-99"
                                type="text"
                                id="cpf"
                                autoComplete="off"
                                onChange={validateCPF}
                                required
                                onFocus={() => setCpfFocus(true)}
                                onBlur={() => setCpfFocus(false)}
                            />
                            <p
                                className={
                                    cpfFocus && !validCpf
                                        ? "instructions"
                                        : "offscreen"
                                }
                            >
                                <FontAwesomeIcon icon={faInfoCircle} />
                                Escreva um CPF válido.
                            </p>
                        </form>
                    </div>
                    <div className="right-form">
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
                                onFocus={() => setPwdFocus(true)}
                                onBlur={(e) => {
                                    setPwdFocus(false);
                                    setPwd(e.target.value);
                                }}
                            />
                            <p
                                className={
                                    pwdFocus && !validPwd
                                        ? "instructions"
                                        : "offscreen"
                                }
                            >
                                <FontAwesomeIcon icon={faInfoCircle} />A senha
                                deve ter no mínimo 8 caracteres, <br /> caracter
                                especial e letra maiúscula
                            </p>
                        </form>
                        <form>
                            <label htmlFor="matchpwd">
                                Confirmar Senha:&nbsp;
                                <FontAwesomeIcon
                                    icon={faCheck}
                                    className={validMatch ? "valid" : "hide"}
                                />
                                <FontAwesomeIcon
                                    icon={faTimes}
                                    className={validMatch ? "hide" : "invalid"}
                                />
                            </label>
                            <input
                                type="password"
                                id="matchPwd"
                                autoComplete="off"
                                onChange={validateMatch}
                                required
                                onFocus={() => setMatchFocus(true)}
                                onBlur={(e) => {
                                    setMatchFocus(false);
                                    setMatchPwd(e.target.value);
                                }}
                            />
                            <p
                                className={
                                    matchFocus && !validMatch
                                        ? "instructions"
                                        : "offscreen"
                                }
                            >
                                <FontAwesomeIcon icon={faInfoCircle} />
                                As senhas não são iguais
                            </p>
                        </form>
                    </div>
                </div>
                <div className="lower-form">
                    <button className="login-btn">Cadastrar-se</button>
                    <p
                        ref={errRef}
                        className={
                            validEmail && validPwd ? "offscreen" : "errMsg"
                        }
                        aria-live="assertive"
                    >
                        Há campos a serem corrigidos
                    </p>
                </div>
            </m.div>
        </m.section>
    );
};

export default RegisterForm;
