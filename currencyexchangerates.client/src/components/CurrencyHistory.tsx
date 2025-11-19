import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { currencyHistoryService } from "../api/currencyHistoryService";
import type { HistoricalRate } from "../models/HistoricalRate";
import "./CurrencyHistory.css";
export default function CurrencyHistory() {

    const { code } = useParams();
    const [history, setHistory] = useState<HistoricalRate[]>([]);
    const [error, setError] = useState("");

    useEffect(() => {
        if (!code) return;

        currencyHistoryService.getHistory(code)
            .then(res => setHistory(res.data))
            .catch(err => {
                console.error(err);
                setError("Błąd pobierania historii");
            });
    }, [code]);

    return (
        <div className="history-container">
            <h2>Historia waluty: {code}</h2>

            {error && <p className="error">{error}</p>}

            <div className="history-list">
                {history.map((h, i) => (
                    <div className="history-card" key={i}>
                        <div className="date">{new Date(h.effectiveDate).toLocaleDateString('pl-PL', { day: '2-digit', month: '2-digit', year: 'numeric'})}</div>
                        <div className="rate">Kurs średni (PLN): <strong>{h.rate}</strong></div>
                    </div>
                ))}
            </div>
        </div>
    );
}