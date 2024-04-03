import Header from "../../components/Header/Header";
import "./Home.css";
import ProdutosTab from "../../components/Home/ProdutosTab";
import HomeMenu from "../../components/HomeMenu/HomeMenu";
import { useState } from "react";

import Product from "../../components/Svgs/Product";
import Stock from "../../components/Svgs/Stock";
import Truck from "../../components/Svgs/Truck";
import Access from "../../components/Svgs/Access";
import { AnimatePresence } from "framer-motion";

const Home = () => {
    const options = [
        {
            description: "Produtos",
            image: <Product height="25px" width="25px" />,
            tab: <ProdutosTab />,
        },
        {
            description: "Estoques",
            image: <Stock height="25px" width="25px" />,
        },
        { description: "Pedidos", image: <Truck height="25px" width="25px" /> },
        {
            description: "Acessos",
            image: <Access height="25px" width="25px" />,
        },
    ];

    const [selectedTab, setSelectedTab] = useState(options[0]);

    return (
        <>
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
