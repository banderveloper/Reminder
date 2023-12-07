import {create} from 'zustand'
import {persist, createJSONStorage} from 'zustand/middleware'
import {ISignInRequest} from "../interfaces/requests/ISignInRequest.ts";
import {ISignUpRequest} from "../interfaces/requests/ISignUpRequest.ts";
import {IAuthStore} from "../interfaces/stores/IAuthStore.ts"
import api from "../shared/api/axios.ts";
import {IServerResponsePayload} from "../interfaces/responses/IServerResponsePayload.ts";
import {ENDPOINTS} from "../shared/api/endpoints.ts";

export const useAuthStore = create<IAuthStore>()(persist((set) => ({
    isAuthenticated: false,
    isLoading: false,
    errorCode: null,

    signIn: async (params: ISignInRequest) => {
        console.log('sign in')

        set({isLoading: true});

        const response = await api.post<IServerResponsePayload<object>>(ENDPOINTS.AUTH.SIGN_IN, {
            username: params.username,
            password: params.password,
            fingerprint: params.fingerprint
        });

        set({isAuthenticated: response.data.succeed});
        set({errorCode: response.data.errorCode});

        set({isLoading: false});
    },

    signUp: async (params: ISignUpRequest) => {
        console.log('sign up')
        console.log(params)
    },

    refresh: async () => {
        console.log('refresh')

        set({isLoading: true});

        const response  = await api.post<IServerResponsePayload<object>>(ENDPOINTS.AUTH.REFRESH);

        set({isAuthenticated: response.data.succeed});
        set({errorCode: response.data.errorCode});

        set({isLoading: false});
    },

    signOut: async () => {
        console.log('sign out')
    }
}), {
    name: 'reminder-storage',
    storage: createJSONStorage(() => sessionStorage)
}));
