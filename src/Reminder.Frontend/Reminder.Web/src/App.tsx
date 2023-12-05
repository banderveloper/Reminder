import './App.css'
import {useEffect, useState} from "react";

function App() {

    const [theme, setTheme] = useState("light");

    useEffect(() => {
        if (theme === "dark") {
            document.documentElement.classList.add("dark");
        } else {
            document.documentElement.classList.remove("dark")
        }
    }, [theme]);

    const handleThemeSwitch = () => {
        setTheme(theme === "dark" ? "light" : "dark");
    };

    return (
        <>
            <div className="h-screen bg-white dark:bg-black flex justify-center items-center">
                <button className="bg-green-200 dark:bg-red-200 p-4 rounded-3xl" onClick={handleThemeSwitch}>
                    Dark Mode
                </button>
            </div>

        </>
    );
}

export default App
