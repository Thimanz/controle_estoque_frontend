import { FaX } from "react-icons/fa6";
import { motion as m } from "framer-motion";
import { useNavigate } from "react-router-dom";
import { useEffect, useState, useRef } from "react";
import {
    getProductListByName,
    getAllProductList,
} from "../../services/productsService";
import "./ProductModal.css";

const ProductsModal = ({ onClose, selectedProducts, setSelectedProducts }) => {
    const navigate = useNavigate();
    const loadRef = useRef();

    const [search, setSearch] = useState("");
    const [productsList, setProductsList] = useState([]);
    const [maxPage, setMaxPage] = useState(1);
    const [currentPage, setCurrentPage] = useState(1);
    const [emptyList, setEmptyList] = useState(false);

    const searchAllProducts = async () => {
        const response = await getAllProductList(currentPage, 6, navigate);
        if (response.status === 200) {
            setProductsList([...productsList, ...response.data.list]);
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
    }, [productsList]);

    const onIntersection = (entries) => {
        const firstEntry = entries[0];
        if (firstEntry.isIntersecting && currentPage <= maxPage) {
            searchProducts();
        }
    };

    const searchProducts = async () => {
        if (!search) {
            searchAllProducts();
            return;
        }
        const response = await getProductListByName(
            search,
            currentPage,
            6,
            navigate
        );
        if (response.status === 200) {
            setProductsList([...productsList, ...response.data.list]);
            setMaxPage(response.data.totalPages);
            setCurrentPage(currentPage + 1);
        } else {
            setEmptyList(true);
        }
    };

    return (
        <section className="main-modal">
            <m.div
                initial={{ y: 50 }}
                animate={{ y: 0 }}
                transition={{ duration: 0.4, ease: "easeOut" }}
                className="modal"
            >
                <button onClick={onClose} className="modal-x">
                    <FaX size={30} />
                </button>
                <div className="product-modal">
                    <section>
                        <form
                            className="search-container"
                            onSubmit={(e) => {
                                e.preventDefault();
                                setCurrentPage(1);
                                setMaxPage(1);
                                setProductsList([]);
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
                                    setMaxPage(1);
                                    setProductsList([]);
                                }}
                            >
                                <g>
                                    <path d="M21.53 20.47l-3.66-3.66C19.195 15.24 20 13.214 20 11c0-4.97-4.03-9-9-9s-9 4.03-9 9 4.03 9 9 9c2.215 0 4.24-.804 5.808-2.13l3.66 3.66c.147.146.34.22.53.22s.385-.073.53-.22c.295-.293.295-.767.002-1.06zM3.5 11c0-4.135 3.365-7.5 7.5-7.5s7.5 3.365 7.5 7.5-3.365 7.5-7.5 7.5-7.5-3.365-7.5-7.5z"></path>
                                </g>
                            </svg>
                        </form>
                    </section>
                    <div className="products-scroller">
                        {productsList.length > 0 && (
                            <div className="paged-products">
                                {productsList.map((product) => {
                                    return (
                                        <div
                                            className="product-box item-product-box"
                                            key={product.id}
                                            onClick={() => {
                                                setSelectedProducts(
                                                    selectedProducts.concat([
                                                        product,
                                                    ])
                                                );
                                                onClose();
                                            }}
                                        >
                                            <img
                                                src={product.imagem}
                                                alt="imagem do produto"
                                                className="product-image"
                                            />
                                            <h4 className="product-name">
                                                {product.nome}
                                            </h4>
                                        </div>
                                    );
                                })}
                            </div>
                        )}
                        {currentPage <= maxPage && (
                            <h4 ref={loadRef}>
                                {emptyList
                                    ? "Não há nada para ver aqui"
                                    : "Carregando..."}
                            </h4>
                        )}
                    </div>
                </div>
            </m.div>
        </section>
    );
};

export default ProductsModal;
