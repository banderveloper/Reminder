import {useAuthStore} from "../../stores/useAuthStore.ts";
import {useNavigate} from "react-router-dom";
import {useEffect} from "react";

const PromptsPage = () => {

    const {isAuthenticated} = useAuthStore();
    const navigate = useNavigate();

    useEffect(() => {
        if(!isAuthenticated) navigate('/auth');
    }, []);

    useEffect(() => {
        if(!isAuthenticated) navigate('/auth');
    }, [isAuthenticated]);


    return (
        <>
            <h1>Prompts page</h1>
        </>
    );
};

export default PromptsPage;