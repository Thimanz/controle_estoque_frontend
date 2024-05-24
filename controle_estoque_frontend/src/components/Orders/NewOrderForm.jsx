import { useEffect, useRef, useState } from "react";
import "./NewOrderForm.css";
import { FaCaretDown, FaCaretUp } from "react-icons/fa";
import { useNavigate } from "react-router-dom";
import ProductsModal from "../Products/ProductsModal";
import {
    getAllStocksList,
    getStocksListByProductId,
} from "../../services/stocksService";
import OrderItemStockDropdown from "./OrderItemStockDropdown";
import {
    postBuyOrder,
    postSellOrder,
    postTransferOrder,
} from "../../services/orderService";
import { ToastContainer, toast, Bounce } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const NewOrderForm = () => {
    const typeRef = useRef();
    const navigate = useNavigate();
    const stockRef = useRef();

    const orderTypes = {
        ENTRADA: "Entrada",
        SAIDA: "Saída",
        TRANSFERENCIA: "Transferência",
    };

    const [stockDropdownActive, setStockDropdownActive] = useState(false);
    const [selectedStock, setSelectedStock] = useState("");
    const [selectedStockId, setSelectedStockId] = useState("");

    const [orderItems, setOrderItems] = useState([]);
    const [amounts, setAmounts] = useState([]);

    const [totalValue, setTotalValue] = useState(0);

    const [typeDropdownActive, setTypeDropdownActive] = useState(false);

    const [type, setType] = useState("");
    const [typeKey, setTypeKey] = useState("");
    const [stocksFrom, setStocksFrom] = useState([]);
    const [stocksTo, setStocksTo] = useState([]);

    const [name, setName] = useState("");

    const [showProductModal, setShowProductModal] = useState(false);

    const [stocksListFrom, setStocksListFrom] = useState([]);
    const [stocksListTo, setStocksListTo] = useState([]);

    //carregar todos os estoques de destino
    useEffect(() => {
        const fetchAllStocks = async () => {
            const stocksData = await getAllStocksList(navigate);
            setStocksListTo(stocksData.data);
        };
        fetchAllStocks();
    }, []);

    //atualizar quantidades e estoques de retirada
    useEffect(() => {
        if (orderItems.length > 0) {
            setAmounts([...amounts, 1]);
            const fetchProductStocks = async () => {
                const stocksData = await getStocksListByProductId(
                    orderItems[orderItems.length - 1].id,
                    navigate
                );
                setStocksListFrom([...stocksListFrom, stocksData.data]);
            };
            fetchProductStocks();
        }
    }, [orderItems.length]);

    //atualizar preço total
    useEffect(() => {
        const totalItemValues =
            orderItems.length > 0 &&
            orderItems
                .map(
                    (item, index) =>
                        (typeKey === "ENTRADA"
                            ? item.precoCusto
                            : item.precoVenda) * amounts[index]
                )
                .filter((item) => item);

        const newTotalValue =
            totalItemValues.length > 0
                ? totalItemValues.reduce((sum, valor) => sum + valor)
                : 0;
        setTotalValue(newTotalValue);
    }, [JSON.stringify(orderItems), amounts, typeKey]);

    const sendOrder = async () => {
        let response;
        switch (typeKey) {
            case "ENTRADA":
                response = await postBuyOrder(
                    {
                        nomeFornecedor: name,
                        pedidoItens: orderItems.map((item, index) => {
                            return {
                                produtoId: item.id,
                                quantidade: amounts[index],
                                precoUnitario: item.precoCusto,
                                localId: stocksTo[index].id,
                            };
                        }),
                    },
                    navigate
                );
                break;
            case "SAIDA":
                response = await postSellOrder(
                    {
                        nomeCliente: name,
                        pedidoItens: orderItems.map((item, index) => {
                            return {
                                produtoId: item.id,
                                quantidade: amounts[index],
                                precoUnitario: item.precoVenda,
                                localId: stocksFrom[index].id,
                            };
                        }),
                    },
                    navigate
                );
                break;
            case "TRANSFERENCIA":
                response = await postTransferOrder(
                    {
                        idLocalDestino: selectedStockId,
                        pedidoItens: orderItems.map((item, index) => {
                            return {
                                produtoId: item.id,
                                quantidade: amounts[index],
                                localId: stocksFrom[index].id,
                            };
                        }),
                    },
                    navigate
                );
                break;
        }
        if (response.status === 204) {
            navigate("/inicio", {
                state: {
                    successMsg: "Pedido Cadastrado com Sucesso",
                    tab: "Pedidos",
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
        <main className="main-order">
            <ToastContainer />
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
                                <label htmlFor="type">Tipo de Transação</label>
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
                    {typeKey && (
                        <section className="right-order-form">
                            {typeKey !== "TRANSFERENCIA" && (
                                <div className="input-group">
                                    <input
                                        type="text"
                                        onBlur={(e) => {
                                            setName(e.target.value);
                                        }}
                                        required
                                        autoComplete="off"
                                    />
                                    <label htmlFor="nome">
                                        {typeKey === "ENTRADA"
                                            ? "Nome do Fornecedor"
                                            : "Nome do Cliente"}
                                    </label>
                                </div>
                            )}
                            {typeKey === "TRANSFERENCIA" && (
                                <div className="input-group dropdown">
                                    <div
                                        onClick={(e) => {
                                            setStockDropdownActive(
                                                !stockDropdownActive
                                            );
                                            stockRef.current.focus();
                                        }}
                                    >
                                        <input
                                            ref={stockRef}
                                            type="button"
                                            value={selectedStock}
                                            required
                                            className="dropdown-btn"
                                        />
                                        <label htmlFor="categoria">
                                            {"Estoque de destino"}
                                        </label>
                                        {stockDropdownActive ? (
                                            <FaCaretUp className="arrow-icon" />
                                        ) : (
                                            <FaCaretDown className="arrow-icon" />
                                        )}
                                    </div>
                                    <div
                                        className="dropdown-content item-dropdown-content"
                                        style={{
                                            display: stockDropdownActive
                                                ? "block"
                                                : "none",
                                        }}
                                    >
                                        {stocksListTo &&
                                            stocksListTo.map((stock) => {
                                                return (
                                                    <div
                                                        key={stock.id}
                                                        onClick={(e) => {
                                                            setSelectedStock(
                                                                e.target
                                                                    .textContent
                                                            );
                                                            setSelectedStockId(
                                                                stock.id
                                                            );
                                                            setStockDropdownActive(
                                                                !stockDropdownActive
                                                            );
                                                        }}
                                                        className="item"
                                                    >
                                                        {stock.nome}
                                                    </div>
                                                );
                                            })}
                                    </div>
                                </div>
                            )}
                        </section>
                    )}
                </div>
                {orderItems.map((item, index) => (
                    <div className="order-items" key={index}>
                        <img
                            src={item.imagem}
                            alt="imagem do produto"
                            className="product-image"
                        />
                        <div className="item-name-inputs">
                            <h4 className="item-name">{item.nome}</h4>
                            <div className="item-inputs">
                                {typeKey !== "" &&
                                    typeKey !== "TRANSFERENCIA" && (
                                        <div className="input-group item-input">
                                            <input
                                                type="number"
                                                pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                                step="any"
                                                value={
                                                    typeKey === "ENTRADA"
                                                        ? item.precoCusto
                                                        : item.precoVenda
                                                }
                                                onChange={(e) => {
                                                    const newOrderItems = [
                                                        ...orderItems,
                                                    ];
                                                    typeKey === "ENTRADA"
                                                        ? (newOrderItems[
                                                              index
                                                          ].precoCusto =
                                                              parseFloat(
                                                                  e.target.value
                                                              ))
                                                        : (newOrderItems[
                                                              index
                                                          ].precoVenda =
                                                              parseFloat(
                                                                  e.target.value
                                                              ));
                                                    setOrderItems(
                                                        newOrderItems
                                                    );
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
                                <div className="product-input-group item-input">
                                    <input
                                        type="number"
                                        pattern="[-+]?[0-9]*[.,]?[0-9]+"
                                        step="any"
                                        value={String(amounts[index])}
                                        onChange={(e) => {
                                            const newAmounts = [...amounts];
                                            newAmounts[index] = parseInt(
                                                e.target.value
                                            );
                                            setAmounts(newAmounts);
                                        }}
                                        required
                                        autoComplete="off"
                                    />
                                    <label htmlFor="quantidade">
                                        Quantidade
                                    </label>
                                </div>
                                {(typeKey === "SAIDA" ||
                                    typeKey === "TRANSFERENCIA") && (
                                    <OrderItemStockDropdown
                                        stocksList={stocksListFrom[index]}
                                        selectedStocks={stocksFrom}
                                        setSelectedStocks={setStocksFrom}
                                        itemIndex={index}
                                        prompt={"Local de Retirada"}
                                    />
                                )}
                                {typeKey === "ENTRADA" && (
                                    <OrderItemStockDropdown
                                        stocksList={stocksListTo}
                                        selectedStocks={stocksTo}
                                        setSelectedStocks={setStocksTo}
                                        itemIndex={index}
                                        prompt={"Local de Destino"}
                                    />
                                )}
                            </div>
                        </div>
                    </div>
                ))}
                {typeKey && (
                    <>
                        <button
                            className="new-product"
                            onClick={() => setShowProductModal(true)}
                        >
                            <div className="sign">+</div>
                            <div className="text">Adicionar Item</div>
                        </button>
                        {typeKey === "ENTRADA" && (
                            <h2 className="total-price">
                                Custo total: R$
                                {totalValue}
                            </h2>
                        )}
                        {typeKey === "SAIDA" && (
                            <h2 className="total-price">
                                Valor total: R$
                                {totalValue}
                            </h2>
                        )}
                        {showProductModal && (
                            <ProductsModal
                                onClose={() => setShowProductModal(false)}
                                selectedProducts={orderItems}
                                setSelectedProducts={setOrderItems}
                            />
                        )}
                        <button className="confirm-order" onClick={sendOrder}>
                            Confirmar Pedido
                        </button>
                    </>
                )}
            </section>
        </main>
    );
};

export default NewOrderForm;
