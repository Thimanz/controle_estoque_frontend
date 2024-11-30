import { useRef, useState, useEffect } from "react";
import {
    faCheck,
    faTimes,
    faInfoCircle,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import validator from "validator";

import { useNavigate } from "react-router-dom";

import { motion as m } from "framer-motion";

import { postAuth } from "../../services/authService";
import { useDispatch } from "react-redux";

import { ToastContainer, toast, Bounce } from "react-toastify";

import { DotLoader } from "react-spinners";

const LoginForm = () => {
    const navigate = useNavigate();

    const userRef = useRef();

    const [loading, setLoading] = useState(false);

    const [email, setEmail] = useState("");
    const [validEmail, setValidEmail] = useState(false);
    const [userFocus, setUserFocus] = useState(false);

    const [pwd, setPwd] = useState("");
    const [validPwd, setValidPwd] = useState(false);
    const [pwdFocus, setPwdFocus] = useState(false);

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
        setLoading(true);
        toast.dismiss();
        if (!(validEmail && validPwd)) {
            toast.error("Há campos a serem corrigidos", {
                position: "top-center",
                autoClose: 5000,
                hideProgressBar: false,
                closeOnClick: false,
                pauseOnHover: false,
                draggable: true,
                progress: undefined,
                theme: "colored",
                transition: Bounce,
            });
            setLoading(false);
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
            localStorage.setItem("userEmail", loginResponse.usuarioToken.email);
            navigate("/inicio", { replace: true });
        } catch (error) {
            error.errors.mensagens.forEach((mensagem) => {
                toast.error(mensagem, {
                    position: "top-center",
                    autoClose: 5000,
                    hideProgressBar: false,
                    closeOnClick: false,
                    pauseOnHover: false,
                    draggable: true,
                    progress: undefined,
                    theme: "colored",
                    transition: Bounce,
                });
            });
        }
        setLoading(false);
    };

    return (
        <>
            <DotLoader
                loading={loading}
                cssOverride={{
                    position: "fixed",
                    left: "50%",
                    top: "50%",
                    marginLeft: -50,
                    marginTop: -50,
                    zIndex: 1000,
                }}
                size={100}
                color="#252525"
            />
            <ToastContainer />
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
                    <h1 className="login-h1">
                        Faça login com as suas credenciais
                    </h1>
                    <form
                        className="email-form"
                        onSubmit={(e) => {
                            e.preventDefault();
                            sendLogin();
                        }}
                    >
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
                            className="email-input"
                            type="text"
                            id="email"
                            ref={userRef}
                            autoComplete="off"
                            onChange={(e) => {
                                validateEmail(e.target.value);
                                setEmail(e.target.value);
                            }}
                            required
                            aria-invalid={validEmail ? "false" : "true"}
                            aria-describedby="uidnote"
                            onFocus={() => setUserFocus(true)}
                            onBlur={(e) => {
                                setUserFocus(false);
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
                    <form
                        className="password-form"
                        onSubmit={(e) => {
                            e.preventDefault();
                            sendLogin();
                        }}
                    >
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
                            className="password-input"
                            type="password"
                            id="password"
                            autoComplete="off"
                            onChange={(e) => {
                                validatePwd(e.target.value);
                                setPwd(e.target.value);
                            }}
                            required
                            aria-invalid={validEmail ? "false" : "true"}
                            aria-describedby="uidnote"
                            onFocus={() => setPwdFocus(true)}
                            onBlur={(e) => {
                                setPwdFocus(false);
                            }}
                        />
                        <p
                            id="uidnote"
                            className={
                                pwdFocus && !validPwd
                                    ? "instructions"
                                    : "offscreen"
                            }
                        >
                            <FontAwesomeIcon icon={faInfoCircle} />A senha deve
                            ter no mínimo 8 caracteres, <br /> caracter especial
                            e letra maiúscula
                        </p>
                    </form>
                    <div className="lower-form">
                        <button
                            style={loading ? { pointerEvents: "none" } : {}}
                            className="login-btn"
                            onClick={sendLogin}
                        >
                            Login
                        </button>
                    </div>
                </m.div>
            </m.section>
        </>
    );
};

export default LoginForm;
