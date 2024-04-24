import LoginRegister from "./routes/LoginRegister/LoginRegister";
import "./App.css";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import NotFound from "./routes/NotFound/NotFound";
import Home from "./routes/Home/Home";
import NewProduct from "./routes/Products/NewProduct";
import Product from "./routes/Products/Product";
import NewOrder from "./routes/Orders/NewOrder";

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Navigate to="/inicio" />} />
                <Route path="/inicio" element={<Home />} />
                <Route path="/produtos/novo-produto" element={<NewProduct />} />
                <Route path="/produtos/:id" element={<Product />} />
                <Route path="/pedidos/novo-pedido" element={<NewOrder />} />
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
