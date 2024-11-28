import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import "./OrderPage.css";
import { getBuyOrder } from "../../services/orderService";

const BuyOrderPage = () => {
    const orderId = useParams().id;
    const navigate = useNavigate();

    const [orderNumber, setOrderNumber] = useState("");
    const [name, setName] = useState("");
    const [orderDate, setOrderDate] = useState("");
    const [totalPrice, setTotalPrice] = useState("");
    const [items, setItems] = useState([]);

    useEffect(() => {
        const fetchOrder = async () => {
            const response = await getBuyOrder(orderId, navigate);
            if (response.status === 200) {
                setOrderNumber(response.data.numero);
                setName(response.data.nomeFornecedor);
                setOrderDate(response.data.dataCriacao);
                setTotalPrice(response.data.precoTotal);
                setItems(response.data.pedidoItens);
            } else {
                navigate("/not-found");
            }
        };

        fetchOrder();
    }, [orderId]);

    return (
        <main className="main-order-page">
            <div className="order-info-products">
                <section className="order-info">
                    <div>
                        <div className="order-info">
                            <h3 className="order-info-text">Numero: </h3>
                            <p>{`${orderNumber}`}</p>
                        </div>
                        <div className="order-info">
                            <h3 className="order-info-text">
                                Nome do Fornecedor:
                            </h3>
                            <p>{`${name}`}</p>
                        </div>
                        <div className="order-info">
                            <h3 className="order-info-text">Data da Compra:</h3>
                            <p>{`${orderDate}`}</p>
                        </div>
                        <div className="order-info">
                            <h3 className="order-info-text">Preço Total:</h3>
                            <p>{`R$ ${totalPrice}`}</p>
                        </div>
                    </div>
                </section>
                <section className="products-order-container">
                    {items.map((item, index) => {
                        return (
                            <div className="order-products" key={index}>
                                <img
                                    src={item.imagem}
                                    alt="imagem do produto"
                                    className="product-image"
                                />
                                <div className="item-name-inputs">
                                    <h4 className="order-product-name">
                                        {item.nome}
                                    </h4>
                                    <div className="order-products-info">
                                        <div className="order-product-info">
                                            <h5>Quantidade: </h5>
                                            <p>{item.quantidade}</p>
                                        </div>
                                        <div className="order-product-info">
                                            <h5>Data de Validade: </h5>
                                            <p>{item.dataValidade}</p>
                                        </div>
                                    </div>
                                    <div className="order-products-info">
                                        <div className="order-product-info">
                                            <h5>Preço Unitario: </h5>
                                            <p>{`R$ ${item.precoUnitario}`}</p>
                                        </div>
                                        <div className="order-product-info">
                                            <h5>Local de Destino: </h5>
                                            <p>{item.local.nome}</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        );
                    })}
                </section>
            </div>
        </main>
    );
};

export default BuyOrderPage;
