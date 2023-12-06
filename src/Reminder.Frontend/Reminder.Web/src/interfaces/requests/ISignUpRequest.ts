export interface ISignUpRequest {
    username: string;
    password: string;
    name: string | null;
    fingerprint: string;
}