import {HubConnection} from '@microsoft/signalr'
import {IDisposablePrompt} from "../IDisposablePrompt.ts";

export interface IPromptsStore {
    connection: HubConnection | null,
    isConnected: boolean,
    disposablePrompts: IDisposablePrompt[],
    errorCode: string | null;
    connect: () => Promise<void>;

    // Server side methods

    GetAllDisposablePrompts: (response: object) => void;

    GetCreateDisposablePromptSuccess: (response: object) => void;
    GetCreateDisposablePromptError: (response: object) => void;

    GetDeleteDisposablePromptSuccess: () => void;
    GetDeleteDisposablePromptError: () => void;

    GetUpdateDisposablePromptSuccess: () => void;
    GetUpdateDisposablePromptError: () => void;

    temp: () => void;
}