import { useEffect, useState } from "react";
import {
    LineChart,
    Line,
    XAxis,
    YAxis,
    CartesianGrid,
    Tooltip,
    Legend,
    ResponsiveContainer,
} from "recharts";
import { getProfitData } from "../../services/dashService";
import { useNavigate } from "react-router-dom";

const ProfitChart = () => {
    const navigate = useNavigate();

    const [data, setData] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const response = await getProfitData(navigate);
            if (response.status === 200) setData(response.data);
        };

        fetchData();
    }, []);

    return (
        <>
            {data.length > 0 && (
                <ResponsiveContainer width="100%" height="100%">
                    <LineChart
                        width={500}
                        height={300}
                        data={data}
                        margin={{
                            top: 5,
                            right: 30,
                            left: 20,
                            bottom: 5,
                        }}
                    >
                        <CartesianGrid strokeDasharray="3 3" />
                        <XAxis dataKey="mesAno" />
                        <YAxis tickFormatter={(value) => "R$" + value} />
                        <Tooltip formatter={(value) => "R$" + value} />
                        <Legend />
                        <Line
                            strokeWidth={3}
                            type="monotone"
                            name="Total em Vendas"
                            dataKey="totalVenda"
                            stroke="#82ca9d"
                            activeDot={{ r: 8 }}
                        />
                        <Line
                            strokeWidth={3}
                            type="monotone"
                            name="Total em Custos"
                            dataKey="totalCompra"
                            stroke="#8884d8"
                            activeDot={{ r: 8 }}
                        />
                    </LineChart>
                </ResponsiveContainer>
            )}
        </>
    );
};

export default ProfitChart;
