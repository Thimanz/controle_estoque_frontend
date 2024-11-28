import { useEffect, useState } from "react";
import { Tooltip, ResponsiveContainer, PieChart, Pie, Cell } from "recharts";
import { getBestSellersData } from "../../services/dashService";
import { useNavigate } from "react-router-dom";

const BestSellersChart = () => {
    const navigate = useNavigate();

    const [data, setData] = useState([]);

    const COLORS = [
        "#77E5A1",
        "#E8007D",
        "#5959AC",
        "#E67D77",
        "#9F9FCF",
        "#78848D",
        "#D4C900",
        "#00A9A8",
        "#CD8EB6",
        "#7AC5AB",
    ];

    useEffect(() => {
        const fetchData = async () => {
            const response = await getBestSellersData(navigate);
            if (response.status === 200) setData(response.data);
        };

        fetchData();
    }, []);

    return (
        <>
            {data.length > 0 && (
                <ResponsiveContainer width="100%" height="100%">
                    <PieChart width={400} height={400}>
                        <Pie
                            nameKey="produto"
                            dataKey="quantidadeVendida"
                            startAngle={360}
                            endAngle={0}
                            data={data}
                            cx="50%"
                            cy="50%"
                            label={(entry) => entry.name}
                        >
                            {data.map((entry, index) => (
                                <Cell fill={COLORS[index % COLORS.length]} />
                            ))}
                        </Pie>
                        <Tooltip />
                    </PieChart>
                </ResponsiveContainer>
            )}
        </>
    );
};

export default BestSellersChart;
