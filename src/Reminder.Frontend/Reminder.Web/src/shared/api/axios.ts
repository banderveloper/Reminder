import axios, {AxiosInstance} from 'axios';
import {API_URL} from "./endpoints.ts";
import {useAuthStore} from "../../stores/useAuthStore.ts";


const api: AxiosInstance = axios.create({
    baseURL: API_URL,
    withCredentials: true // enable cookies sharing
});


api.interceptors.request.use(request => {

    console.log('[INTERCEPTOR] REQUEST');
    console.log(request)

    return request;
});

// Response logging
api.interceptors.response.use(
    response => {

        console.log('[INTERCEPTOR] RESPONSE SUCCESS');
        console.log(response)

        return response;
    },
    async error => {

        console.error('[INTERCEPTOR] RESPONSE ERROR');
        console.error(error);

        const {response} = error;

        if(response && response.status == 401){

            const {isAuthenticated, refresh} = useAuthStore.getState();

            console.log('GOT 401, refreshing...');
            // todo refresh token existing
            await refresh();
            console.log('REFRESHED, AUTHENTICATED: ' + isAuthenticated)
        }
    });


export default api;