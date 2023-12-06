import {create} from 'zustand'
import {ISignInRequest} from "../interfaces/requests/ISignInRequest.ts";
import {ISignUpRequest} from "../interfaces/requests/ISignUpRequest.ts";

interface IAuthStore {
    isAuthenticated: boolean;
    isLoading: boolean;

    signIn: (params: ISignInRequest) => Promise<void>;
    signUp: (params: ISignUpRequest) => Promise<void>;
    refresh: () => Promise<void>;
    signOut: () => Promise<void>;
}

export const useAuthStore = create<IAuthStore>(() => ({
    isAuthenticated: false,
    isLoading: false,

    signIn: async (params: ISignInRequest) => {
        console.log('sign in')
        console.log(params)
    },

    signUp: async (params: ISignUpRequest) => {
        console.log('sign up')
        console.log(params)
    },

    refresh: async () => {
        console.log('refresh')
    },

    signOut: async () => {
        console.log('sign out')
    }
}));