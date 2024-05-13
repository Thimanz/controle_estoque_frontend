import { useNavigate } from "react-router-dom";
import "./ProdutosTab.css";
import { motion as m } from "framer-motion";
import { useState, useEffect } from "react";
import { getProductListPaged } from "../../services/productsService";
import { FaChevronRight, FaChevronLeft } from "react-icons/fa6";

const ProdutosTab = () => {
    const navigate = useNavigate();

    const [search, setSearch] = useState("");
    const [productsList, setProductsList] = useState([]);
    const [maxPage, setmaxPage] = useState();

    const [currentPage, setCurrentPage] = useState(1);

    useEffect(() => {
        searchProducts();
    }, [currentPage]);

    const searchProducts = async () => {
        if (!search) return;
        const response = await getProductListPaged(
            search,
            currentPage,
            12,
            navigate
        );
        if (response.status === 200) {
            setProductsList(response.data.list);
            setmaxPage(response.data.TotalPages);
        }
    };

    return (
        <main className="main-home">
            <div className="product-container">
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
                            searchProducts();
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
                            onClick={searchProducts}
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
                        className="new-product"
                        onClick={() => navigate("/produtos/novo-produto")}
                    >
                        <div className="sign">+</div>
                        <div className="text">Novo Produto</div>
                    </button>
                </m.section>
            </div>
            <m.div
                initial={{ y: 10, opacity: 0 }}
                animate={{ y: 0, opacity: 1 }}
                exit={{
                    y: -10,
                    opacity: 0,
                    transition: { delay: 0.2 },
                }}
                transition={{ duration: 0.2 }}
                className="products-searched"
            >
                {productsList.length > 0 && (
                    <>
                        {productsList.map((produto) => {
                            return (
                                <div
                                    className="product-box"
                                    key={produto.id}
                                    onClick={() =>
                                        navigate(`/produtos/${produto.id}`)
                                    }
                                >
                                    <img
                                        src={"\\" + produto.imagem}
                                        alt="imagem do produto"
                                        className="product-image"
                                    />
                                    <h4 className="product-name">
                                        {produto.nome}
                                    </h4>
                                </div>
                            );
                        })}
                        <div className="pagination-buttons">
                            <button
                                className="button-last bg-color-main-blue"
                                onClick={() => {
                                    setCurrentPage(
                                        currentPage === 1
                                            ? currentPage
                                            : currentPage - 1
                                    );
                                }}
                            >
                                <FaChevronLeft size={40} />
                            </button>
                            <h4 className="current-page current-page-black">
                                {currentPage}
                            </h4>
                            <button
                                className="button-next bg-color-main-blue"
                                onClick={() => {
                                    setCurrentPage(
                                        currentPage === maxPage
                                            ? currentPage
                                            : currentPage + 1
                                    );
                                }}
                            >
                                <FaChevronRight size={40} />
                            </button>
                        </div>
                    </>
                )}
            </m.div>
        </main>
    );
};

export default ProdutosTab;
