import api from "./http";
import type { CurrencyTable } from "../models/CurrencyTable";

interface ApiError {
    response?: {
        status: number;
    };
}

export async function getCurrencyTable(): Promise<CurrencyTable | null> {
    try {
        const response = await api.get<CurrencyTable>("/api/Currencies");

        return response.data; // OK
    }
    catch (error) {
        const err = error as ApiError;  // 🍀 typujemy błąd!

        // 404 → brak danych, nie błąd sieci
        if (err.response?.status === 404) {
            return null;
        }

        // inne błędy → rzucamy wyżej
        throw new Error("Błąd połączenia z serwerem.");
    }
}
