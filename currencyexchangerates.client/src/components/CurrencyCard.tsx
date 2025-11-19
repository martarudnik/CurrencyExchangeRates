import { Link } from "react-router-dom";
import "./CurrencyCard.css";
import type { CurrencyRate } from "../models/CurrencyRate";

export default function CurrencyCard({ currency, code, rate }: CurrencyRate) {
    return (
        <Link to={`/history/${code}`} className="currency-card">
            <div className="header">
                <span className="currency">{currency}/{code}</span>
            </div>
            <div className="row">
                <span>Kurs średni (PLN)</span>
                <span className="value">{rate}</span>
            </div>
        </Link>
    );
}