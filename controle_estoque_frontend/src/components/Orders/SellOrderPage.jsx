import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import "./OrderPage.css";
import { getSellOrder } from "../../services/orderService";

const SellOrderPage = () => {
    const orderId = useParams().id;
    const navigate = useNavigate();

    const [orderNumber, setOrderNumber] = useState(NaN);
    const [name, setName] = useState("");
    const [orderDate, setOrderDate] = useState("");
    const [items, setItems] = useState([]);

    useEffect(() => {
        const fetchOrder = async () => {
            const response = await getSellOrder(orderId, navigate);
            if (response.status === 200) {
                setOrderNumber(response.data.numero);
                setName(response.data.nomeCliente);
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
                                Nome do Cliente:
                            </h3>
                            <p>{`${name}`}</p>
                        </div>
                        <div className="order-info">
                            <h3 className="order-info-text">Data da Venda:</h3>
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
                                            <h5>Preço Unitario: </h5>
                                            <p>{item.precoUnitario}</p>
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

export default SellOrderPage;