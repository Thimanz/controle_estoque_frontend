import { useEffect, useState } from "react";
import {
    XAxis,
    YAxis,
    CartesianGrid,
    Tooltip,
    ResponsiveContainer,
    BarChart,
    Bar,
    Cell,
    Text,
} from "recharts";
import { getOccupationData } from "../../services/dashService";
import { useNavigate } from "react-router-dom";

function CustomizedTick(props) {
    const { x, y, stroke, payload } = props;
    return (
        <Text
            angle={-90}
            x={x}
            y={y}
            style={{ fontSize: "10px" }}
            fill="#000000"
            textAnchor="end"
            width="10"
            verticalAnchor="middle"
        >
            {payload.value}
        </Text>
    );
}

const OccupationChart = () => {
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
            const response = await getOccupationData(navigate);
            if (response.status === 200) setData(response.data);
        };

        fetchData();
    }, []);

    return (
        <>
            {data.length > 0 && (
                <ResponsiveContainer width="100%" height="100%">
                    <BarChart
                        data={data}
                        cx="50%"
                        cy="50%"
                        margin={{
                            top: 0,
                            right: 0,
                            left: 0,
                            bottom: 30,
                        }}
                    >
                        <CartesianGrid strokeDasharray="3 3" />
                        <XAxis
                            dataKey="nome"
                            angle={-45}
                            interval={0}
                            textAnchor="end"
                            tick={<CustomizedTick />}
                        />
                        <YAxis />
                        <Tooltip cursor={{ fill: "none" }} />
                        <Bar
                            name="Quantidade em Estoque"
                            dataKey="quantidadeEstoque"
                        >
                            {data.map((entry, index) => (
                                <Cell fill={COLORS[index % COLORS.length]} />
                            ))}
                        </Bar>
                    </BarChart>
                </ResponsiveContainer>
            )}
        </>
    );
};

export default OccupationChart;
