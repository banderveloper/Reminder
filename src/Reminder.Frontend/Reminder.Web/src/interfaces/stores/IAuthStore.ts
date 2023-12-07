import {ISignInRequest} from "../requests/ISignInRequest.ts";
import {ISignUpRequest} from "../requests/ISignUpRequest.ts";

export interface IAuthStore {
    isAuthenticated: boolean;
    isLoading: boolean;
    errorCode: string | null;

    signIn: (params: ISignInRequest) => Promise<void>;
    signUp: (params: ISignUpRequest) => Promise<void>;
    refresh: () => Promise<void>;
    signOut: () => Promise<void>;
    checkAuthentication: () => Promise<void>;
}