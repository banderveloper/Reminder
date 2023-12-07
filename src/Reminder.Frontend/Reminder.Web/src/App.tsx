import './App.css'
import {useAuthStore} from "./stores/useAuthStore.ts";
import {useEffect} from "react";

function App() {

    const {isAuthenticated, signIn, errorCode} = useAuthStore();

    useEffect(() => {
        signIn({
            username: 'admin',
            fingerprint: 'admin',
            password: 'admin'
        })
        // api.get('http://localhost:5000/temp/protected');

    }, []);

    return (
        <>
            <h1>Is authenticated: {isAuthenticated ? "Yes" : "No"}</h1>
            <h2>Error code: {errorCode ? errorCode : "NULL"}</h2>
        </>
    )
}

export default App
