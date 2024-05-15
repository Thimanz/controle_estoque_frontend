import { useState } from "react";
import Notifications from "./Notifications";
import { FaBars, FaUser } from "react-icons/fa";
import { FaX } from "react-icons/fa6";
import { motion as m } from "framer-motion";
import "./Header.css";
import Logo from "./Logo";

import { useSelector } from "react-redux";

import { useNavigate } from "react-router-dom";

const Header = () => {
    const navigate = useNavigate();

    const [isMobileNavOpen, setIsMobileNavOpen] = useState(false);

    const navVariants = {
        open: {
            height: "35vh",
            alignItems: "flex-start",
        },
        closed: {
            height: "30px",
        },
    };

    return (
        <m.nav
            className="header"
            key={isMobileNavOpen}
            initial={"closed"}
            animate={isMobileNavOpen ? "open" : "closed"}
            variants={navVariants}
            transition={{ duration: 0.5, ease: "easeInOut" }}
        >
            <Logo onClick={() => navigate("/inicio")} />
            <ul className="desktop-nav">
                <li className="nav-item">
                    <a href="/">Home</a>
                </li>
                <li className="nav-item">
                    <a href="/">Sobre</a>
                </li>
                <li
                    className="nav-item nav-user"
                    onClick={() => navigate("/autenticar/login")}
                >
                    <FaUser />
                    <a className="user">
                        {localStorage.getItem("userEmail")
                            ? `Olá, ${localStorage.getItem("userEmail")}`
                            : "Faça Login"}
                    </a>
                </li>
                <li className="nav-item">
                    <Notifications />
                </li>
            </ul>
            <div className="mobile-dropdown">
                <button
                    className="mobile-nav-toggle"
                    onClick={() => setIsMobileNavOpen(!isMobileNavOpen)}
                >
                    {isMobileNavOpen ? <FaX /> : <FaBars />}
                </button>
                <m.ul
                    className="mobile-nav"
                    style={{ display: isMobileNavOpen ? "flex" : "none" }}
                    initial={{ opacity: 0, x: "-70dvw" }}
                    animate={{ opacity: 1, x: "-60dvw" }}
                    transition={{ duration: 0.5, delay: 0.5 }}
                >
                    <li className="nav-item">
                        <a href="/">Home</a>
                    </li>
                    <li className="nav-item">
                        <a href="/">About</a>
                    </li>
                    <li
                        className="nav-item"
                        onClick={() => navigate("/autenticar/login")}
                    >
                        <FaUser />
                        <a className="user">
                            {localStorage.getItem("userEmail")
                                ? `Olá, ${localStorage.getItem("userEmail")}`
                                : "Faça Login"}
                        </a>
                    </li>
                    <li className="nav-item">
                        <Notifications />
                    </li>
                </m.ul>
            </div>
        </m.nav>
    );
};

export default Header;
