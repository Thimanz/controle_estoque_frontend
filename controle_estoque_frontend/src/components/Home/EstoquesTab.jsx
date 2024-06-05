import { useNavigate } from "react-router-dom";
import "./EstoquesTab.css";
import { motion as m } from "framer-motion";
import { useState, useEffect, useRef } from "react";
import {
    getAllStocksListPaged,
    getStockListByName,
} from "../../services/stocksService";

const EstoquesTab = () => {
    const navigate = useNavigate();
    const loadRef = useRef();

    const [search, setSearch] = useState("");
    const [stocksList, setStocksList] = useState([]);
    const [maxPage, setMaxPage] = useState(1);

    const [currentPage, setCurrentPage] = useState(1);

    const searchAllStocks = async () => {
        const response = await getAllStocksListPaged(currentPage, 15, navigate);
        if (response.status === 200) {
            setStocksList([...stocksList, ...response.data.list]);
            setMaxPage(response.data.totalPages);
            setCurrentPage(currentPage + 1);
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
    }, [stocksList]);

    const onIntersection = (entries) => {
        const firstEntry = entries[0];
        if (firstEntry.isIntersecting && currentPage <= maxPage) {
            searchStocks();
        }
    };

    const searchStocks = async () => {
        if (!search) {
            searchAllStocks();
            return;
        }
        const response = await getStockListByName(
            search,
            currentPage,
            15,
            navigate
        );
        if (response.status === 200) {
            setStocksList([...stocksList, ...response.data.list]);
            setMaxPage(response.data.totalPages);
            setCurrentPage(currentPage + 1);
        }
    };

    return (
        <main className="main-home">
            <div className="stock-container">
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
                            setStocksList([]);
                        }}
                    >
                        <input
                            placeholder="Pesquisar produto"
                            className="input-search"
                            type="text"
                            onChange={(e) => setSearch(e.target.value)}
                        />
                        <svg
                            viewBox="0 0 24 24"
                            className="search__icon"
                            onClick={() => {
                                setCurrentPage(1);
                                setStocksList([]);
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
                        className="new-stock"
                        onClick={() => navigate("/estoques/novo-estoque")}
                    >
                        <div className="sign">+</div>
                        <div className="text">Novo Estoque</div>
                    </button>
                </m.section>
            </div>
            {stocksList.length > 0 && (
                <m.div
                    initial={{ y: 10, opacity: 0 }}
                    animate={{ y: 0, opacity: 1 }}
                    exit={{
                        y: -10,
                        opacity: 0,
                        transition: { delay: 0.2 },
                    }}
                    transition={{ duration: 0.2 }}
                    className="stocks-searched"
                >
                    {stocksList.map((stock) => {
                        return (
                            <div
                                className="stock-box"
                                key={stock.id}
                                onClick={() =>
                                    navigate(`/estoques/${stock.id}`)
                                }
                            >
                                <h4 className="stock-name">{stock.nome}</h4>
                                <div className="stock-props">
                                    <div className="stock-props-headers">
                                        <th>Espaço Total: </th>
                                        <th>Espaço Livre: </th>
                                        <th>Quantidade de Itens: </th>
                                    </div>
                                    <div className="stock-data">
                                        <td>{`${stock.espacoTotal} cm³`}</td>
                                        <td>{`${stock.espacoLivre} cm³`}</td>
                                        <td>{`${stock.quantidadeItens} cm³`}</td>
                                    </div>
                                </div>
                            </div>
                        );
                    })}
                </m.div>
            )}
            {currentPage <= maxPage && <h4 ref={loadRef}>Carregando...</h4>}
        </main>
    );
};
export default EstoquesTab;
