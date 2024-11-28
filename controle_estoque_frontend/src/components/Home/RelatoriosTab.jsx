import BestSellersChart from "../Dashboards/BestSellersChart";
import MostOccupiedChart from "../Dashboards/MostOccupiedChart";
import OccupationChart from "../Dashboards/OccupationChart";
import ProfitChart from "../Dashboards/ProfitChart";
import "./RelatoriosTab.css";

const RelatoriosTab = () => {
    return (
        <main className="main-home main-dashboards">
            <div className="dashs">
                <section className="dash1">
                    <h1 className="dash-title">
                        Custos X Vendas (Últimos 6 meses)
                    </h1>
                    <ProfitChart />
                </section>
                <section className="dash2">
                    <h1 className="dash-title">Top 10 Mais Vendidos</h1>
                    <BestSellersChart />
                </section>
                <section className="dash3">
                    <h1 className="dash-title">
                        Percentual de Ocupação por Estoque
                    </h1>
                    <MostOccupiedChart />
                </section>
                <section className="dash4">
                    <h1 className="dash-title">
                        Produtos com Maior Quantidade em Estoque
                    </h1>
                    <OccupationChart />
                </section>
            </div>
        </main>
    );
};

export default RelatoriosTab;
