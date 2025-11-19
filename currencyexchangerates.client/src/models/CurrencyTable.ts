import type { CurrencyRate } from "./CurrencyRate";

export interface CurrencyTable {
    table: string;
    tableNumber: string;
    effectiveDate: string;
    rates: CurrencyRate[];
}
