import { useEffect, useState } from "react";
import "./CurrencyGrid.css";
import CurrencyCard from "./CurrencyCard";
import { type CurrencyTable } from "../models/CurrencyTable";
import { getCurrencyTable } from "../api/currencyService";

export default function CurrencyGrid() {
    const [table, setTable] = useState<CurrencyTable | null>(null);
    const [error, setError] = useState("");

    useEffect(() => {
        getCurrencyTable()
            .then(result => {
                if (result === null) {
                    setError("Nie znaleziono danych na temat kursów.");
                } else {
                    setTable(result);
                }
            })
            .catch(() => setError("Błąd pobierania danych"));
    }, []);

    if (error) return <p style={{ color: "red" }}>{error}</p>;
    if (!table)
        return (
            <div className="center-box empty-box">
                Brak danych do wyświetlenia.
            </div>
        );

    return (
        <>
            <h1>Tabela {table.table} kursów średnich walut obcych</h1>

            <h2>Dane z dnia: {new Date(table.effectiveDate).toLocaleDateString('pl-PL', {day: '2-digit', month: '2-digit', year: 'numeric'})}</h2>
            <h3>Tabela numer: {table.tableNumber}</h3>
            <div className="currency-grid">
                {table.rates.map(r => (
                    <CurrencyCard
                        key={r.code}
                        currency={r.currency}
                        code={r.code}
                        rate={r.rate}
                    />
                ))}
            </div>

        </>
    );
}