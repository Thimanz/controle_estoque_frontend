import { useEffect, useRef, useState } from "react";
import "./NewOrderForm.css";
import { FaCaretDown, FaCaretUp } from "react-icons/fa";
import { useNavigate } from "react-router-dom";
import ProductsModal from "../Products/ProductsModal";

const NewOrderForm = () => {
    const typeRef = useRef();
    const navigate = useNavigate();

    const orderTypes = {
        ENTRADA: "Entrada",
        SAIDA: "Saída",
        TRANSFERENCIA: "Transferência",
    };

    const [orderItems, setOrderItems] = useState([]);
    const [orderItemsPrices, setOrderItemsPrices] = useState([]);

    useEffect(() => {
        console.log(orderItems);
    }, [orderItems]);

    const [typeDropdownActive, setTypeDropdownActive] = useState(false);

    const [type, setType] = useState("");
    const [typeKey, setTypeKey] = useState("");

    const [totalValue, setTotalValue] = useState(0);

    const [showProductModal, setShowProductModal] = useState(false);

    return (
        <main className="main-order">
            <section className="centered-block">
                <div className="order-forms">
                    <section className="left-order-form">
                        <div className="input-group dropdown">
                            <div
                                onClick={(e) => {
                                    setTypeDropdownActive(!typeDropdownActive);
                                    typeRef.current.focus();
                                }}
                            >
                                <input
                                    ref={typeRef}
                                    type="button"
                                    value={type}
                                    required
                                    className="dropdown-btn"
                                />
                                <label htmlFor="categoria">
                                    Tipo de Transação
                                </label>
                                {typeDropdownActive ? (
                                    <FaCaretUp className="arrow-icon" />
                                ) : (
                                    <FaCaretDown className="arrow-icon" />
                                )}
                            </div>
                            <div
                                className="dropdown-content"
                                style={{
                                    display: typeDropdownActive
                                        ? "block"
                                        : "none",
                                }}
                            >
                                {Object.keys(orderTypes).map((type) => {
                                    return (
                                        <div
                                            key={type}
                                            onClick={(e) => {
                                                setType(e.target.textContent);
                                                setTypeDropdownActive(
                                                    !typeDropdownActive
                                                );
                                                setTypeKey(type);
                                            }}
                                            className="item"
                                        >
                                            {orderTypes[type]}
                                        </div>
                                    );
                                })}
                            </div>
                        </div>
                    </section>
                    <section className="right-order-form"></section>
                </div>
                {orderItems.map((item, index) => (
                    <div className="order-items" key={index}>
                        <img
                            src={"\\" + item.imagem}
                            alt="imagem do produto"
                            className="product-image"
                        />
                        <div>
                            <h4 className="product-name">{item.nome}</h4>
                            {typeKey !== "" && typeKey !== "TRANSFERENCIA" && (
                                <div className="input-group item-inputs">
                                    <input
                                        type="number"
                                        onBlur={(e) => {
                                            const newOrderItems = orderItems;
                                            typeKey === "ENTRADA"
                                                ? (newOrderItems[
                                                      index
                                                  ].precoCusto = parseFloat(
                                                      e.target.value
                                                  ))
                                                : (newOrderItems[
                                                      index
                                                  ].precoVenda = parseFloat(
                                                      e.target.value
                                                  ));
                                            console.log(newOrderItems);
                                            setOrderItems(newOrderItems);
                                        }}
                                        required
                                        autoComplete="off"
                                    />
                                    {typeKey === "ENTRADA" && (
                                        <label htmlFor="precoCusto">
                                            Preço de custo
                                        </label>
                                    )}
                                    {typeKey === "SAIDA" && (
                                        <label htmlFor="precoVenda">
                                            Preço de venda
                                        </label>
                                    )}
                                </div>
                            )}
                        </div>
                    </div>
                ))}
                <button
                    className="new-product"
                    onClick={() => setShowProductModal(true)}
                >
                    <div className="sign">+</div>
                    <div className="text">Adicionar Item</div>
                </button>
                <h2 className="total-price">
                    Valor total: {totalValue.toFixed(2)}
                </h2>
                {showProductModal && (
                    <ProductsModal
                        onClose={() => setShowProductModal(false)}
                        selectedProducts={orderItems}
                        setSelectedProducts={setOrderItems}
                    />
                )}
            </section>
        </main>
    );
};

export default NewOrderForm;
