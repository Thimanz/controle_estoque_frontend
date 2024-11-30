import { useLocation } from "react-router-dom";
import Header from "../../components/Header/Header";
import NewOrderForm from "../../components/Orders/NewOrderForm";

const NewOrder = () => {
    const { state } = useLocation();

    try {
        return (
            <>
                <Header />
                <NewOrderForm
                    orderTypeKey={state.orderType}
                    itemId={state.itemId}
                />
            </>
        );
    } catch (e) {
        return (
            <>
                <Header />
                <NewOrderForm />
            </>
        );
    }
};

export default NewOrder;
