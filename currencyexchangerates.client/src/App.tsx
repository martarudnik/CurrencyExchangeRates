import { Routes, Route } from "react-router-dom";
import CurrencyGrid from "./components/CurrencyGrid";
import CurrencyHistory from "./components/CurrencyHistory";

function App() {
    return (
        <Routes>
            <Route
                path="/"
                element={
                    <div>
                        <CurrencyGrid />
                    </div>
                }
            />
            <Route
                path="/history/:code"
                element={<CurrencyHistory />}
            />
        </Routes>
    );
}

export default App;
