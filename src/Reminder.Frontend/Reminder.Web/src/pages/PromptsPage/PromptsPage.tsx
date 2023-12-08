import {useAuthStore} from "../../stores/useAuthStore.ts";
import {useNavigate} from "react-router-dom";
import {useEffect} from "react";
import {usePromptsStore} from "../../stores/usePromptsStore.ts";

const PromptsPage = () => {

    const {connect, temp} = usePromptsStore();
    const {isAuthenticated} = useAuthStore();
    const navigate = useNavigate();

    useEffect(() => {
        if(!isAuthenticated) navigate('/auth');
    }, []);

    useEffect(() => {
        if(!isAuthenticated) navigate('/auth');
    }, [isAuthenticated]);

    async function con() {
        await connect();
    }

    async function post(){
        temp();
    }

    return (
        <>
            <h1>Prompts page</h1>

            <button onClick={con}>connect</button>
            <button onClick={post}>post</button>
        </>
    );
};

export default PromptsPage;