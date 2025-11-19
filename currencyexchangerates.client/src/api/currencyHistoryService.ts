import api from "./http";
import type { HistoricalRate } from "../models/HistoricalRate";

export const currencyHistoryService = {
    getHistory: (code: string) => api.get<HistoricalRate[]>(`/api/Currencies/${code}/history`),
};