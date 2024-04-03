import { useNavigate } from "react-router-dom";
import Product from "../Svgs/Product";
import "./ProdutosTab.css";
import { motion as m, AnimatePresence } from "framer-motion";

const ProdutosTab = () => {
    const navigate = useNavigate();

    return (
        <main className="main-home">
            <div className="main-container">
                <m.section
                    className="search"
                    initial={{ x: 10, opacity: 0 }}
                    animate={{ x: 0, opacity: 1 }}
                    exit={{ x: 10, opacity: 0, transition: { delay: 0 } }}
                    transition={{ duration: 0.2, delay: 0.2 }}
                >
                    <h2>Encontre seu produto</h2>
                    <div className="search-container">
                        <input className="input-search" type="text" />
                        <svg viewBox="0 0 24 24" className="search__icon">
                            <g>
                                <path d="M21.53 20.47l-3.66-3.66C19.195 15.24 20 13.214 20 11c0-4.97-4.03-9-9-9s-9 4.03-9 9 4.03 9 9 9c2.215 0 4.24-.804 5.808-2.13l3.66 3.66c.147.146.34.22.53.22s.385-.073.53-.22c.295-.293.295-.767.002-1.06zM3.5 11c0-4.135 3.365-7.5 7.5-7.5s7.5 3.365 7.5 7.5-3.365 7.5-7.5 7.5-7.5-3.365-7.5-7.5z"></path>
                            </g>
                        </svg>
                    </div>
                </m.section>
                <m.div
                    initial={{ y: 10, opacity: 0 }}
                    animate={{ y: 0, opacity: 1 }}
                    exit={{ y: -10, opacity: 0, transition: { delay: 0.2 } }}
                    transition={{ duration: 0.2 }}
                >
                    <Product height="200px" width="200px" />
                </m.div>
                <m.section
                    className="new"
                    initial={{ x: -10, opacity: 0 }}
                    animate={{ x: 0, opacity: 1 }}
                    exit={{ x: -10, opacity: 0, transition: { delay: 0 } }}
                    transition={{ duration: 0.2, delay: 0.2 }}
                >
                    <h2>Cadastre um novo produto</h2>
                    <button
                        className="new-product"
                        onClick={() => navigate("/produtos/novo-produto")}
                    >
                        <div className="sign">+</div>
                        <div className="text">Novo Produto</div>
                    </button>
                </m.section>
            </div>
        </main>
    );
};

export default ProdutosTab;
