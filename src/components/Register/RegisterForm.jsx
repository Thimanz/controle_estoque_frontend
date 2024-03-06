import { useRef, useState, useEffect } from "react";
import {
    faCheck,
    faTimes,
    faInfoCircle,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import validator from "validator";

const RegisterForm = () => {
    const userRef = useRef();
    const errRef = useRef();

    const [validEmail, setValidEmail] = useState(false);
    const [userFocus, setUserFocus] = useState(false);

    const [pwd, setPwd] = useState("");
    const [validPwd, setValidPwd] = useState(false);
    const [pwdFocus, setPwdFocus] = useState(false);

    const [matchPwd, setMatchPwd] = useState("");
    const [validMatch, setValidMatch] = useState(false);
    const [matchFocus, setMatchFocus] = useState(false);

    const [errMsg, setErrMsg] = useState("");
    const [success, setSuccess] = useState(false);

    useEffect(() => {
        userRef.current.focus();
    }, []);

    // useEffect(() => {
    //     setValidPwd(PWD_REGEX.test(pwd));
    //     setValidMatch(pwd === matchPwd);
    // }, [pwd, matchPwd]);

    // useEffect(() => {
    //     setErrMsg("");
    // }, [pwd, matchPwd]);

    const validateEmail = (e) => {
        const email = e.target.value;
        setValidEmail(validator.isEmail(email));
    };

    return (
        <section>
            <p
                ref={errRef}
                className={errMsg ? "errMsg" : "offscreen"}
                aria-live="assertive"
            >
                {errMsg}
            </p>
            <h1>Cadastre-se</h1>
            <form>
                <label htmlFor="username">
                    Usuário:
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
                    id="username"
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
                        userFocus && !validEmail ? "instructions" : "offscreen"
                    }
                >
                    <FontAwesomeIcon icon={faInfoCircle} />
                    Escreva um email válido.
                </p>
            </form>
        </section>
    );
};

export default RegisterForm;
