import Header from "../../components/Header/Header";
import "./Home.css";
import HomeComponent from "../../components/Home/ProdutosTab";
import HomeMenu from "../../components/HomeMenu/HomeMenu";
import { useState } from "react";

import Product from "../../components/Svgs/Product";
import Stock from "../../components/Svgs/Stock";
import Truck from "../../components/Svgs/Truck";
import Access from "../../components/Svgs/Access";

const Home = () => {
    const options = [
        { description: "Produtos", image: <Product /> },
        { description: "Estoques", image: <Stock /> },
        { description: "Pedidos", image: <Truck /> },
        { description: "Acessos", image: <Access /> },
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
            <HomeComponent />
        </>
    );
};

export default Home;
