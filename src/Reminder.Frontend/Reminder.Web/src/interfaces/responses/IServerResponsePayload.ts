export interface IServerResponsePayload<T> {
    errorCode: string | null;
    data: T;
    succeed: boolean;
}
