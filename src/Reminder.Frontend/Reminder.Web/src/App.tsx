import './App.css'
import AuthPage from "./pages/AuthPage/AuthPage.tsx";
import {Route, Routes} from "react-router-dom";
import PromptsPage from "./pages/PromptsPage/PromptsPage.tsx";
import Navbar from "./components/Navbar/Navbar.tsx";
import SignOutPage from "./pages/SignOutPage/SignOutPage.tsx";

function App() {

    return (
        <>
            <Navbar/>
            <Routes>
                <Route path='/' element={ <PromptsPage/> }/>
                <Route path='/prompts' element={ <PromptsPage/> }/>
                <Route path='/auth' element={ <AuthPage/> }/>
                <Route path='/signout' element={ <SignOutPage/> }/>
            </Routes>
        </>
    )
}

export default App
