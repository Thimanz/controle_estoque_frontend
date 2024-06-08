import Header from "../../components/Header/Header";
import "./Home.css";
import ProdutosTab from "../../components/Home/ProdutosTab";
import HomeMenu from "../../components/HomeMenu/HomeMenu";
import { useEffect, useState } from "react";
import { ToastContainer, toast, Bounce } from "react-toastify";

import Product from "../../components/Svgs/Product";
import Stock from "../../components/Svgs/Stock";
import Truck from "../../components/Svgs/Truck";
import { AnimatePresence } from "framer-motion";
import PedidosTab from "../../components/Home/PedidosTab";
import { useLocation } from "react-router-dom";
import EstoquesTab from "../../components/Home/EstoquesTab";

const Home = () => {
    const { state } = useLocation();

    const options = [
        {
            description: "Produtos",
            image: <Product height="25px" width="25px" />,
            tab: <ProdutosTab />,
        },
        {
            description: "Estoques",
            image: <Stock height="25px" width="25px" />,
            tab: <EstoquesTab />,
        },
        {
            description: "Pedidos",
            image: <Truck height="25px" width="25px" />,
            tab: <PedidosTab />,
        },
    ];

    const [selectedTab, setSelectedTab] = useState(options[0]);

    useEffect(() => {
        try {
            setSelectedTab(options.find((e) => e.description === state.tab));
            toast.success(state.successMsg, {
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
        } catch {}
    }, []);

    return (
        <>
            <ToastContainer />
            <Header />
            <HomeMenu
                selectedTab={selectedTab}
                setSelectedTab={setSelectedTab}
                options={options}
            />
            <AnimatePresence mode="wait">{selectedTab.tab}</AnimatePresence>
        </>
    );
};

export default Home;
