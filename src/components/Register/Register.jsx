import RegisterForm from "./RegisterForm";

const Register = ({ setLogin }) => {
    return (
        <>
            <RegisterForm></RegisterForm>
            <section className="select-login">
                <h1>Já possui cadastro e deseja realizar login?</h1>
                <button onClick={setLogin}>Login</button>
            </section>
        </>
    );
};

export default Register;
