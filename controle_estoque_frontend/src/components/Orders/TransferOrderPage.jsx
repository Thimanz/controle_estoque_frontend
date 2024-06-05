import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import "./OrderPage.css";
import { getTransferOrder } from "../../services/orderService";

const TransferOrderPage = () => {
    const orderId = useParams().id;
    const navigate = useNavigate();

    const [orderNumber, setOrderNumber] = useState(NaN);
    const [stockTo, setStockTo] = useState({});
    const [orderDate, setOrderDate] = useState("");
    const [items, setItems] = useState([]);

    useEffect(() => {
        const fetchOrder = async () => {
            const response = await getTransferOrder(orderId, navigate);
            if (response.status === 200) {
                setOrderNumber(response.data.numero);
                setStockTo(response.data.localDestino);
                setOrderDate(response.data.dataCriacao);
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
                                Estoque de Destino:
                            </h3>
                            <p>{`${stockTo.nome}`}</p>
                        </div>
                        <div className="order-info">
                            <h3 className="order-info-text">
                                Data da Transferência:
                            </h3>
                            <p>{`${orderDate}`}</p>
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
                                            <h5>Local de Retirada: </h5>
                                            <p>{item.localRetirada.nome}</p>
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

export default TransferOrderPage;
