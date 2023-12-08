import {useAuthStore} from "../../stores/useAuthStore.ts";
import {useNavigate} from "react-router-dom";
import {useEffect} from "react";

const SignOutPage = () => {

    const {signOut, isAuthenticated} = useAuthStore();
    const navigate = useNavigate();

    useEffect(() => {

        if(isAuthenticated) signOut();
        else navigate('/auth');

    }, []);

    useEffect(() => {

        if (!isAuthenticated) navigate('/auth');

    }, [isAuthenticated]);

    return <></>
};

export default SignOutPage;