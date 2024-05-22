import { useEffect, useState } from "react";
import { FaBell, FaCircleExclamation, FaPlus } from "react-icons/fa6";
import "./Notifications.css";
import { getNotifications } from "../../services/notificationService";
import { useNavigate } from "react-router-dom";

const Notifications = () => {
    const navigate = useNavigate();

    const [notifications, setNotifications] = useState([]);

    useEffect(() => {
        const fetchNotifications = async () => {
            const response = await getNotifications(navigate);
            setNotifications(response.data);
        };

        fetchNotifications();
        const interval = setInterval(() => {
            fetchNotifications();
        }, 5000);

        return () => clearInterval(interval);
    }, []);

    const closeNotification = (index) => {
        navigate("/pedidos/novo-pedido");
    };

    const [isDropdownActive, setIsDropdownActive] = useState(false);

    return (
        <>
            <a onClick={() => setIsDropdownActive(!isDropdownActive)}>
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
                <ul className="notifications-menu">
                    {notifications.map((notification, index) => (
                        <li key={notification.id} className="notification">
                            <FaCircleExclamation
                                size={30}
                                color="rgb(240, 175, 175)"
                                onClick={() =>
                                    navigate(`/produtos/${notification.id}`)
                                }
                            />
                            <p
                                onClick={() =>
                                    navigate(`/produtos/${notification.id}`)
                                }
                            >
                                {notification.mensagem}
                            </p>
                            <div
                                onClick={() => closeNotification(index)}
                                className="notification-plus"
                            >
                                <FaPlus size={20} color="white" />
                            </div>
                        </li>
                    ))}
                </ul>
            )}
        </>
    );
};

export default Notifications;
