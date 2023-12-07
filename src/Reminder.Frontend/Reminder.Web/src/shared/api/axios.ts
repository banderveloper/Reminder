import axios from 'axios';
import {API_URL} from "./endpoints.ts";

const api = axios.create({
    baseURL: API_URL,
    // withCredentials: true
});

// Add interceptor for 401 responses
api.interceptors.response.use(
    (response) => response,
    async (error) => {

        const {response} = error;

        if (response && response.status === 401) {
            console.log(401)
        }

        // Rethrow error for other status codes
        return Promise.reject(error);
    }
);

export default api;