import {create} from 'zustand'
import {persist, createJSONStorage} from 'zustand/middleware'
import {ISignInRequest} from "../interfaces/requests/ISignInRequest.ts";
import {ISignUpRequest} from "../interfaces/requests/ISignUpRequest.ts";
import {IAuthStore} from "../interfaces/stores/IAuthStore.ts"

export const useAuthStore = create<IAuthStore>()(persist((set, get) => ({
    isAuthenticated: false,
    isLoading: false,

    signIn: async (params: ISignInRequest) => {
        console.log(set, get);
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
}), {
    name: 'reminder-storage',
    storage: createJSONStorage(() => sessionStorage)
}));
