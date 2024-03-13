import LoginRegister from "./routes/LoginRegister/LoginRegister";
import "./App.css";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import NotFound from "./routes/NotFound/NotFound";
import Home from "./routes/Home/Home";

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/inicio" element={<Home />} />
                <Route path="/" element={<Navigate to="/autenticar/login" />} />
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
