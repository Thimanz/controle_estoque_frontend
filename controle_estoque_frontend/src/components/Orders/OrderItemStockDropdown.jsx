import { useEffect, useRef, useState } from "react";
import { FaCaretDown, FaCaretUp } from "react-icons/fa";

const OrderItemStockDropdown = ({
    stocksList,
    selectedStocks,
    setSelectedStocks,
    itemIndex,
    prompt,
}) => {
    const stockRef = useRef();

    const [stockDropdownActive, setStockDropdownActive] = useState(false);
    const [selectedStock, setSelectedStock] = useState("");

    useEffect(() => {}, [selectedStock]);

    return (
        <div className="input-group dropdown item-input">
            <div
                onClick={(e) => {
                    setStockDropdownActive(!stockDropdownActive);
                    stockRef.current.focus();
                }}
            >
                <input
                    ref={stockRef}
                    type="button"
                    value={selectedStock}
                    required
                    className="dropdown-btn"
                />
                <label htmlFor="categoria">{prompt}</label>
                {stockDropdownActive ? (
                    <FaCaretUp className="arrow-icon" />
                ) : (
                    <FaCaretDown className="arrow-icon" />
                )}
            </div>
            <div
                className="dropdown-content item-dropdown-content"
                style={{
                    display: stockDropdownActive ? "block" : "none",
                }}
            >
                {stocksList &&
                    stocksList.map((stock) => {
                        return (
                            <div
                                key={stock.id}
                                onClick={(e) => {
                                    setSelectedStock(e.target.textContent);
                                    setStockDropdownActive(
                                        !stockDropdownActive
                                    );

                                    const newSelectedStock = [
                                        ...selectedStocks,
                                    ];
                                    newSelectedStock[itemIndex] = stock;
                                    setSelectedStocks(newSelectedStock);
                                }}
                                className="item"
                            >
                                {stock.nome}
                            </div>
                        );
                    })}
            </div>
        </div>
    );
};

export default OrderItemStockDropdown;
