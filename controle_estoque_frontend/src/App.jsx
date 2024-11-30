import LoginRegister from "./routes/LoginRegister/LoginRegister";
import "./App.css";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import NotFound from "./routes/NotFound/NotFound";
import Home from "./routes/Home/Home";
import NewProduct from "./routes/Products/NewProduct";
import Product from "./routes/Products/Product";
import NewOrder from "./routes/Orders/NewOrder";
import Stock from "./routes/Stocks/Stock";
import NewStock from "./routes/Stocks/NewStock";
import BuyOrder from "./routes/Orders/BuyOrder";
import SellOrder from "./routes/Orders/SellOrder";
import TransferOrder from "./routes/Orders/TransferOrder";

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Navigate to="/inicio" />} />
                <Route path="/inicio" element={<Home />} />
                <Route path="/produtos/novo-produto" element={<NewProduct />} />
                <Route path="/estoques/novo-estoque" element={<NewStock />} />
                <Route path="/produtos/:id" element={<Product />} />
                <Route path="/estoques/:id" element={<Stock />} />
                <Route path="/pedidos/novo-pedido" element={<NewOrder />} />
                <Route path="/pedidos/compra/:id" element={<BuyOrder />} />
                <Route path="/pedidos/venda/:id" element={<SellOrder />} />
                <Route
                    path="/pedidos/transferencia/:id"
                    element={<TransferOrder />}
                />
                <Route
                    path="/autenticar/:authtype"
                    element={<LoginRegister />}
                />
                <Route path="*" element={<NotFound />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;
