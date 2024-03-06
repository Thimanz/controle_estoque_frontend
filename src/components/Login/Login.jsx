import LoginForm from "./LoginForm";
import "./Login.css";

const Login = ({ setRegister }) => {
    return (
        <>
            <LoginForm></LoginForm>
            <section className="select-register">
                <h1>É um membro novo e ainda não possui cadastro?</h1>
                <button onClick={setRegister}>Cadastrar-se</button>
            </section>
        </>
    );
};

export default Login;
