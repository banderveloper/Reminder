export const API_URL: string = 'http://localhost:5000'

export const ENDPOINTS = {
    AUTH: {
        SIGN_IN: `${API_URL}/auth/sign-in`,
        REFRESH: `${API_URL}/auth/refresh`,
        SIGN_OUT: `${API_URL}/auth/sign-out`,
        SIGN_UP: `${API_URL}/auth/sign-up`
    }
}