import "./NewStockForm.css";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { ToastContainer, toast, Bounce } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { postStock } from "../../services/stocksService";

const NewStockForm = () => {
    const navigate = useNavigate();

    const [name, setName] = useState("");
    const [address, setAddress] = useState("");
    const [length, setLength] = useState("");
    const [width, setWidth] = useState("");
    const [height, setHeight] = useState("");

    const sendErrorMsg = (message) => {
        toast.error(message, {
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
    };

    const validateEmptyFields = () => {
        let hasError;
        if (!name) {
            sendErrorMsg('Campo "Nome" não pode ser vazio');
            hasError = true;
        }
        if (!address) {
            sendErrorMsg('Campo "Endereço" não pode ser vazio');
            hasError = true;
        }
        if (!length) {
            sendErrorMsg('Campo "Comprimento" não pode ser vazio');
            hasError = true;
        }
        if (!width) {
            sendErrorMsg('Campo "Largura" não pode ser vazio');
            hasError = true;
        }
        if (!height) {
            sendErrorMsg('Campo "Altura" não pode ser vazio');
            hasError = true;
        }
        return hasError;
    };

    const sendStock = async () => {
        toast.dismiss();
        if (validateEmptyFields()) {
            return;
        }
        const response = await postStock(
            {
                nome: name,
                endereco: address,
                comprimento: parseFloat(length),
                largura: parseFloat(width),
                altura: parseFloat(height),
            },
            navigate
        );
        if (response.status === 204) {
            navigate("/inicio", {
                state: {
                    successMsg: "Estoque Cadastrado com Sucesso",
                    tab: "Estoques",
                },
            });
        } else {
            response.errors.mensagens.forEach(sendErrorMsg);
        }
    };

    return (
        <main className="main-stock">
            <ToastContainer />
            <section className="centered-block">
                <div className="stock-forms">
                    <section className="left-stock-form">
                        <div className="input-group">
                            <input
                                type="text"
                                onBlur={(e) => {
                                    setName(e.target.value);
                                }}
                                required
                                autoComplete="off"
                            />
                            <label htmlFor="nome">Nome</label>
                        </div>
                        <div className="input-group">
                            <input
                                type="number"
                                pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                step="any"
                                onBlur={(e) => {
                                    setWidth(e.target.value);
                                }}
                                required
                                autoComplete="off"
                            />
                            <label htmlFor="largura">Largura (cm)</label>
                        </div>
                    </section>
                    <section className="right-stock-form">
                        <div className="input-group">
                            <input
                                type="number"
                                pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                step="any"
                                onBlur={(e) => {
                                    setLength(e.target.value);
                                }}
                                required
                                autoComplete="off"
                            />
                            <label htmlFor="comprimento">
                                Comprimento (cm)
                            </label>
                        </div>
                        <div className="input-group">
                            <input
                                type="number"
                                pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                step="any"
                                onBlur={(e) => {
                                    setHeight(e.target.value);
                                }}
                                required
                                autoComplete="off"
                            />
                            <label htmlFor="altura">Altura (cm)</label>
                        </div>
                    </section>
                </div>
                <div className="input-group">
                    <input
                        type="text"
                        onBlur={(e) => {
                            setAddress(e.target.value);
                        }}
                        required
                        autoComplete="off"
                    />
                    <label htmlFor="descricao">Endereço</label>
                </div>
                <button className="confirm-stock" onClick={sendStock}>
                    Cadastrar Estoque
                </button>
            </section>
        </main>
    );
};

export default NewStockForm;
