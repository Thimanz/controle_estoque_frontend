import { useEffect, useRef, useState } from "react";
import { FaBell, FaCircleExclamation, FaPlus } from "react-icons/fa6";
import "./Notifications.css";
import { getNotifications } from "../../services/notificationService";
import { useNavigate } from "react-router-dom";

const Notifications = () => {
    const bellRef = useRef();
    const dropdownRef = useRef();
    const navigate = useNavigate();

    const [notifications, setNotifications] = useState([]);

    useEffect(() => {
        const fetchNotifications = async () => {
            const response = await getNotifications(navigate);
            if (response.status === 200) setNotifications(response.data);
        };

        fetchNotifications();
        const interval = setInterval(() => {
            fetchNotifications();
        }, 5000);

        let closer = (e) => {
            if (
                dropdownRef.current &&
                !dropdownRef.current.contains(e.target) &&
                bellRef.current &&
                !bellRef.current.contains(e.target)
            ) {
                setIsDropdownActive(false);
            }
        };

        document.addEventListener("mousedown", closer);

        return () => {
            clearInterval(interval);
            document.removeEventListener("mousedown", closer);
        };
    }, []);

    const closeNotification = (productId) => {
        navigate("/pedidos/novo-pedido", {
            state: { orderType: "ENTRADA", itemId: productId },
        });
    };

    const [isDropdownActive, setIsDropdownActive] = useState(false);

    return (
        <>
            <a
                ref={bellRef}
                onClick={() => setIsDropdownActive(!isDropdownActive)}
            >
                <FaBell
                    color={isDropdownActive ? "#04d9ff" : "white"}
                    size={25}
                />
                {notifications.length > 0 && (
                    <p className="notification-ammount">
                        {notifications.length}
                    </p>
                )}
            </a>
            {isDropdownActive && notifications.length > 0 && (
                <ul ref={dropdownRef} className="notifications-menu">
                    {notifications.map((notification, index) => {
                        switch (notification.tipo) {
                            case 1:
                                return (
                                    <li key={index} className="notification">
                                        <FaCircleExclamation
                                            size={30}
                                            color="rgb(240, 175, 175)"
                                            onClick={() =>
                                                navigate(
                                                    `/produtos/${notification.id}`
                                                )
                                            }
                                        />
                                        <p
                                            onClick={() =>
                                                navigate(
                                                    `/produtos/${notification.id}`
                                                )
                                            }
                                        >
                                            {notification.mensagem}
                                        </p>
                                        <div
                                            onClick={() =>
                                                closeNotification(
                                                    notification.id
                                                )
                                            }
                                            className="notification-plus"
                                        >
                                            <FaPlus size={20} color="white" />
                                        </div>
                                    </li>
                                );
                            case 2:
                                return (
                                    <li key={index} className="notification">
                                        <FaCircleExclamation
                                            size={30}
                                            color="rgb(240, 175, 175)"
                                            onClick={() =>
                                                navigate(
                                                    `/estoques/${notification.id}`
                                                )
                                            }
                                        />
                                        <p
                                            onClick={() =>
                                                navigate(
                                                    `/estoques/${notification.id}`
                                                )
                                            }
                                        >
                                            {notification.mensagem}
                                        </p>
                                    </li>
                                );
                        }
                    })}
                </ul>
            )}
        </>
    );
};

export default Notifications;
