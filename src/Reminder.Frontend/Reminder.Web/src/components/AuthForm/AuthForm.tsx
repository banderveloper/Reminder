import {useState} from "react";
import {useAuthStore} from "../../stores/useAuthStore.ts";

const AuthForm = () => {

    const {isAuthenticated, signIn, errorCode} = useAuthStore();

    const [username, setUsername] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const fingerprint: string = 'web';

    const handleSubmit = async () => {

        await signIn({
            username: username,
            password: password,
            fingerprint: fingerprint
        });
    }

    return (
        <div>
            <h1>Auth form</h1>
            <input type="text" name="username" onChange={e => setUsername(e.target.value)}/>
            <input type="text" name="password" onChange={e => setPassword(e.target.value)}/>
            <button onClick={handleSubmit}>Go</button>

            <h3>IsAuthenticated: {isAuthenticated ? "Yes" : "No"}</h3>
            <h3>Error code: {errorCode ? errorCode : "NONE"}</h3>
        </div>
    );
};

export default AuthForm;