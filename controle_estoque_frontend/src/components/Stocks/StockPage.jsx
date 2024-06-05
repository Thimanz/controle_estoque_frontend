import { useEffect, useState, useRef } from "react";
import { FaTrash } from "react-icons/fa";
import { useNavigate, useParams } from "react-router-dom";
import { deleteProduct, updateProduct } from "../../services/productsService";
import { ToastContainer, toast, Bounce } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "./StockPage.css";
import {
    deleteStock,
    getStock,
    updateStock,
} from "../../services/stocksService";

const StockPage = () => {
    const stockId = useParams().id;
    const navigate = useNavigate();

    const [name, setName] = useState("");
    const [address, setAddress] = useState("");
    const [products, setProducts] = useState([]);
    const [totalSpace, setTotalSpace] = useState("");
    const [freeSpace, setFreeSpace] = useState("");
    const [length, setLength] = useState("");
    const [width, setWidth] = useState("");
    const [height, setHeight] = useState("");

    useEffect(() => {
        const fetchStock = async () => {
            const response = await getStock(stockId, navigate);
            if (response.status === 200) {
                setName(response.data.nome);
                setAddress(response.data.endereco);
                setProducts(response.data.localItens);
                setFreeSpace(response.data.espacoLivreCalculado);
                setTotalSpace(response.data.espacoTotal);
                setLength(response.data.dimensoes.comprimento);
                setWidth(response.data.dimensoes.largura);
                setHeight(response.data.dimensoes.altura);
            } else {
                navigate("/not-found");
            }
        };

        fetchStock();
    }, [stockId]);

    useEffect(() => {
        setTotalSpace(
            parseFloat(width) * parseFloat(height) * parseFloat(length)
        );
    }, [width, height, length]);

    const updateButton = async () => {
        const response = await updateStock(
            stockId,
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
                    successMsg: "Estoque Atualizado com Sucesso",
                    tab: "Estoques",
                },
            });
        } else {
            response.errors.mensagens.forEach((mensagem) => {
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
    };

    const deleteButton = async () => {
        const response = await deleteStock(stockId, navigate);
        if (response.status === 204) {
            navigate("/inicio", {
                state: {
                    successMsg: "Estoque Deletado com Sucesso",
                    tab: "Estoques",
                },
            });
        } else {
            response.errors.mensagens.forEach((mensagem) => {
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
    };

    return (
        <main className="main-stock-page">
            <ToastContainer />
            <div className="stock-info-products">
                <section className="stock-info">
                    <div>
                        <div className="stock-input-group">
                            <input
                                type="text"
                                onChange={(e) => {
                                    setName(e.target.value);
                                }}
                                required
                                value={name}
                                autoComplete="off"
                            />
                            <label htmlFor="nome">Nome</label>
                        </div>
                        <div className="stock-input-group">
                            <input
                                type="text"
                                onChange={(e) => {
                                    setAddress(e.target.value);
                                }}
                                required
                                value={address}
                                autoComplete="off"
                            />
                            <label htmlFor="endereco">Endereço</label>
                        </div>
                        <div className="stock-input-group">
                            <input
                                type="number"
                                pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                step="any"
                                onChange={(e) => {
                                    setLength(e.target.value);
                                }}
                                required
                                value={length}
                                autoComplete="off"
                            />
                            <label htmlFor="comprimento">
                                Comprimento (cm)
                            </label>
                        </div>
                        <div className="stock-input-group">
                            <input
                                type="number"
                                pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                step="any"
                                onChange={(e) => {
                                    setWidth(e.target.value);
                                }}
                                required
                                value={width}
                                autoComplete="off"
                            />
                            <label htmlFor="largura">Largura (cm)</label>
                        </div>
                        <div className="stock-input-group">
                            <input
                                type="number"
                                pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                step="any"
                                onChange={(e) => {
                                    setHeight(e.target.value);
                                }}
                                required
                                value={height}
                                autoComplete="off"
                            />
                            <label htmlFor="altura">Altura (cm)</label>
                        </div>
                        <div className="stock-info">
                            <h3 className="stock-info-text">Espaço Total:</h3>
                            <p>{`${totalSpace} cm³`}</p>
                        </div>
                        <div className="stock-info">
                            <h3 className="stock-info-text">Espaço Livre:</h3>
                            <p>{`${freeSpace} cm³`}</p>
                        </div>
                    </div>
                </section>
                <section className="products-stock-container">
                    {products.map((product, index) => {
                        return (
                            <div className="stock-products" key={index}>
                                <img
                                    src={product.imagem}
                                    alt="imagem do produto"
                                    className="product-image"
                                />
                                <div className="item-name-inputs">
                                    <h4 className="stock-product-name">
                                        {product.nome}
                                    </h4>
                                    <div className="stock-products-info">
                                        <div className="stock-product-info">
                                            <h5>Quantidade: </h5>
                                            <p>{product.quantidade}</p>
                                        </div>
                                        <div className="stock-product-info">
                                            <h5>Data de Validade: </h5>
                                            <p>{product.dataValidade}</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        );
                    })}
                </section>
            </div>
            <div className="stock-buttons">
                <button className="delete-stock" onClick={deleteButton}>
                    Deletar
                </button>
                <button className="update-stock" onClick={updateButton}>
                    Atualizar
                </button>
            </div>
        </main>
    );
};

export default StockPage;
