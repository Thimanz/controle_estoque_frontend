import { motion as m } from "framer-motion";
import { useEffect, useRef, useState } from "react";
import "./PedidosTab.css";
import { useNavigate } from "react-router-dom";
import { getAllOrders, getOrderListByDate } from "../../services/orderService";

const PedidosTab = () => {
    const navigate = useNavigate();
    const loadRef = useRef();

    const [search, setSearch] = useState("");
    const [ordersList, setOrdersList] = useState([]);
    const [maxPage, setMaxPage] = useState(1);
    const [emptyList, setEmptyList] = useState(false);

    const [currentPage, setCurrentPage] = useState(1);

    const searchAllOrders = async () => {
        const response = await getAllOrders(currentPage, 15, navigate);
        if (response.status === 200) {
            setOrdersList([...ordersList, ...response.data.list]);
            setMaxPage(response.data.totalPages);
            setCurrentPage(currentPage + 1);
        } else {
            setEmptyList(true);
        }
    };

    useEffect(() => {
        const observer = new IntersectionObserver(onIntersection);
        if (observer && loadRef.current) {
            observer.observe(loadRef.current);
        }

        return () => {
            if (observer) observer.disconnect();
        };
    }, [ordersList]);

    const onIntersection = (entries) => {
        const firstEntry = entries[0];
        if (firstEntry.isIntersecting && currentPage <= maxPage) {
            searchOrders();
        }
    };

    const searchOrders = async () => {
        if (!search) {
            searchAllOrders();
            return;
        }
        const response = await getOrderListByDate(
            search,
            currentPage,
            15,
            navigate
        );
        if (response.status === 200) {
            setOrdersList([...ordersList, ...response.data.list]);
            setMaxPage(response.data.totalPages);
            setCurrentPage(currentPage + 1);
        } else {
            setEmptyList(true);
        }
    };

    return (
        <main className="main-home">
            <div className="order-container">
                <m.section
                    className="search"
                    initial={{ x: 10, opacity: 0 }}
                    animate={{ x: 0, opacity: 1 }}
                    exit={{ x: 10, opacity: 0, transition: { delay: 0 } }}
                    transition={{ duration: 0.2, delay: 0.2 }}
                >
                    <form
                        className="search-container"
                        onSubmit={(e) => {
                            e.preventDefault();
                            setCurrentPage(1);
                            setMaxPage(1);
                            setOrdersList([]);
                        }}
                    >
                        <input
                            placeholder="Pesquisar Pedido"
                            className="input-search"
                            type="date"
                            onChange={(e) => setSearch(e.target.value)}
                        />
                        <svg
                            viewBox="0 0 24 24"
                            className="search__icon"
                            onClick={() => {
                                setCurrentPage(1);
                                setMaxPage(1);
                                setOrdersList([]);
                            }}
                        >
                            <g>
                                <path d="M21.53 20.47l-3.66-3.66C19.195 15.24 20 13.214 20 11c0-4.97-4.03-9-9-9s-9 4.03-9 9 4.03 9 9 9c2.215 0 4.24-.804 5.808-2.13l3.66 3.66c.147.146.34.22.53.22s.385-.073.53-.22c.295-.293.295-.767.002-1.06zM3.5 11c0-4.135 3.365-7.5 7.5-7.5s7.5 3.365 7.5 7.5-3.365 7.5-7.5 7.5-7.5-3.365-7.5-7.5z"></path>
                            </g>
                        </svg>
                    </form>
                </m.section>
                <m.section
                    className="new"
                    initial={{ x: -10, opacity: 0 }}
                    animate={{ x: 0, opacity: 1 }}
                    exit={{ x: -10, opacity: 0, transition: { delay: 0 } }}
                    transition={{ duration: 0.2, delay: 0.2 }}
                >
                    <button
                        className="new-order"
                        onClick={() => navigate("/pedidos/novo-pedido")}
                    >
                        <div className="sign">+</div>
                        <div className="text">Novo Pedido</div>
                    </button>
                </m.section>
            </div>
            {ordersList.length > 0 && (
                <m.div
                    initial={{ y: 10, opacity: 0 }}
                    animate={{ y: 0, opacity: 1 }}
                    exit={{
                        y: -10,
                        opacity: 0,
                        transition: { delay: 0.2 },
                    }}
                    transition={{ duration: 0.2 }}
                    className="orders-searched"
                >
                    {ordersList.map((order) => {
                        return (
                            <div
                                className="order-box"
                                key={order.id}
                                onClick={() => {
                                    switch (order.tipo) {
                                        case 0:
                                            navigate(
                                                `/pedidos/compra/${order.id}`
                                            );
                                            break;
                                        case 1:
                                            navigate(
                                                `/pedidos/venda/${order.id}`
                                            );
                                            break;
                                        case 2:
                                            navigate(
                                                `/pedidos/transferencia/${order.id}`
                                            );
                                    }
                                }}
                            >
                                <h4 className="order-name">
                                    {"Pedido " + order.numero}
                                </h4>
                                <div className="order-props">
                                    <div className="order-props-headers">
                                        <th>Tipo: </th>
                                        <th>Data: </th>
                                    </div>
                                    <div className="order-data">
                                        <td>
                                            {order.tipo === 0
                                                ? "Compra"
                                                : order.tipo === 1
                                                ? "Venda"
                                                : "Transferência"}
                                        </td>
                                        <td>{order.data}</td>
                                    </div>
                                </div>
                            </div>
                        );
                    })}
                </m.div>
            )}
            {currentPage <= maxPage && (
                <h4 ref={loadRef}>
                    {emptyList ? "Não há nada para ver aqui" : "Carregando..."}
                </h4>
            )}
        </main>
    );
};

export default PedidosTab;
