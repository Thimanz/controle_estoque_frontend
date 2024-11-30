import { motion } from "framer-motion";
import "./HomeMenu.css";

const HomeMenu = ({ selectedTab, setSelectedTab, options }) => {
    return (
        <nav className="menu">
            <ul className="menu-list">
                {options.map((item) => (
                    <li
                        key={item.description}
                        className={
                            item.description === selectedTab.description
                                ? "selected"
                                : ""
                        }
                        onClick={() => setSelectedTab(item)}
                    >
                        <div className="item">
                            {item.image}
                            <p className="item-description">{`${item.description}`}</p>
                        </div>
                        {item.description === selectedTab.description ? (
                            <motion.div
                                className="underline"
                                layoutId="underline"
                            />
                        ) : null}
                    </li>
                ))}
            </ul>
        </nav>
    );
};

export default HomeMenu;
