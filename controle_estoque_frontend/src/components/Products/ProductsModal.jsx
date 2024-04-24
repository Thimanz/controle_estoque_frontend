import { FaX, FaChevronRight, FaChevronLeft } from "react-icons/fa6";
import { motion as m } from "framer-motion";
import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { getProductListPaged } from "../../services/productsService";
import "./ProductModal.css";

const ProductsModal = ({ onClose, selectedProducts, setSelectedProducts }) => {
    const navigate = useNavigate();

    const [search, setSearch] = useState("");
    const [productsList, setProductsList] = useState([]);

    const [currentPage, setCurrentPage] = useState(0);

    useEffect(() => {
        searchProducts();
    }, [currentPage]);

    const searchProducts = async () => {
        if (!search) return;
        const response = await getProductListPaged(
            search,
            currentPage,
            navigate
        );
        if (response.status === 200) {
            setProductsList(response.data);
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
                                onClick={() => {
                                    setCurrentPage(1);
                                }}
                            >
                                <g>
                                    <path d="M21.53 20.47l-3.66-3.66C19.195 15.24 20 13.214 20 11c0-4.97-4.03-9-9-9s-9 4.03-9 9 4.03 9 9 9c2.215 0 4.24-.804 5.808-2.13l3.66 3.66c.147.146.34.22.53.22s.385-.073.53-.22c.295-.293.295-.767.002-1.06zM3.5 11c0-4.135 3.365-7.5 7.5-7.5s7.5 3.365 7.5 7.5-3.365 7.5-7.5 7.5-7.5-3.365-7.5-7.5z"></path>
                                </g>
                            </svg>
                        </form>
                    </section>
                    <div className="paged-products">
                        {Object.keys(productsList).length !== 0 &&
                            productsList.List.map((produto) => {
                                return (
                                    <m.div
                                        initial={{ opacity: 0.5 }}
                                        animate={{ opacity: 1 }}
                                        transition={{ duration: 0.2 }}
                                        className="product-box item-product-box"
                                        key={produto.id}
                                        onClick={() => {
                                            setSelectedProducts(
                                                selectedProducts.concat([
                                                    produto,
                                                ])
                                            );
                                            onClose();
                                        }}
                                    >
                                        <img
                                            src={"\\" + produto.imagem}
                                            alt="imagem do produto"
                                            className="product-image"
                                        />
                                        <h4 className="product-name">
                                            {produto.nome}
                                        </h4>
                                    </m.div>
                                );
                            })}
                    </div>
                    {Object.keys(productsList).length !== 0 &&
                        productsList.List.length > 0 && (
                            <div className="pagination-buttons">
                                <button
                                    className="button-last"
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
                                <h4 className="current-page">{currentPage}</h4>
                                <button
                                    className="button-next"
                                    onClick={() => {
                                        setCurrentPage(
                                            currentPage ===
                                                productsList.TotalPages
                                                ? currentPage
                                                : currentPage + 1
                                        );
                                    }}
                                >
                                    <FaChevronRight size={40} />
                                </button>
                            </div>
                        )}
                </div>
            </m.div>
        </section>
    );
};

export default ProductsModal;
