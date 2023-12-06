import './App.css'
import Navbar from "./components/Navbar/Navbar.tsx";

function App() {

    return (
        <>
            <Navbar navbarItems={
                [
                    {path: '/first', pathName: 'First'},
                    {path: '/second', pathName: 'Second'},
                ]
            }/>
            <p>Hello world</p>
        </>
    )
}

export default App
