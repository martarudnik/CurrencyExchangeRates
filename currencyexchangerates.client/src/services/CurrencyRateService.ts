//import { useEffect, useState } from "react";
//import { CurrencyRateService } from "../api/currencyService";
//import type { CurrencyRate } from "../models/CurrencyRate";

//export function useCurrencyRates() {
//    const [currencies, setCurrencies] = useState<CurrencyRate[]>([]);
//    const [error, setError] = useState("");

//    useEffect(() => {
//        CurrencyRateService.getAll()
//            .then(res => setCurrencies(res.data))
//            .catch(() => setError("Error"));
//    }, []);

//    return { currencies, error };
//}
