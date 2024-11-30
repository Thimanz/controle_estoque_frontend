import { useEffect, useState } from "react";
import {
    Tooltip,
    ResponsiveContainer,
    BarChart,
    XAxis,
    Bar,
    CartesianGrid,
    YAxis,
    Cell,
    Text,
} from "recharts";
import { getMostOccupiedData } from "../../services/dashService";
import { useNavigate } from "react-router-dom";

function CustomizedTick(props) {
    const { x, y, stroke, payload } = props;
    return (
        <Text
            x={x}
            y={y}
            style={{ fontSize: "12px" }}
            fill="#000000"
            textAnchor="middle"
            width={70}
            verticalAnchor="start"
        >
            {payload.value}
        </Text>
    );
}

const MostOccupiedChart = () => {
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
            const response = await getMostOccupiedData(navigate);
            if (response.status === 200) setData(response.data);
        };

        fetchData();
    }, []);

    return (
        <>
            {data.length > 0 && (
                <ResponsiveContainer width="100%" height="100%">
                    <BarChart
                        dataKey="porcentagemOcupacao"
                        data={data}
                        cx="50%"
                        cy="50%"
                    >
                        <CartesianGrid strokeDasharray="3 3" />
                        <XAxis
                            dataKey="nome"
                            interval={0}
                            tick={<CustomizedTick />}
                        />
                        <YAxis
                            domain={[0, 100]}
                            tickFormatter={(tick) => {
                                return `${tick}%`;
                            }}
                        />
                        <Tooltip
                            cursor={{ fill: "none" }}
                            formatter={function (percentage) {
                                return `${percentage}%`;
                            }}
                        />
                        <Bar
                            name="Porcentagem de Ocupação"
                            dataKey="porcentagemOcupacao"
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

export default MostOccupiedChart;
