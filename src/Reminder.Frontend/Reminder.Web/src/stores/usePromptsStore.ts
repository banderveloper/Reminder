import {IPromptsStore} from "../interfaces/stores/IPromptsStore.ts";
import {create} from "zustand"
import {createJSONStorage, persist} from 'zustand/middleware'
import {HubConnectionBuilder} from "@microsoft/signalr";
import {ENDPOINTS} from "../shared/signal/endpoints.ts";


export const usePromptsStore = create<IPromptsStore>()(persist((set, get) => ({

    connection: null,
    errorCode: null,
    isConnected: false,
    disposablePrompts: [],

    connect: async () => {

        const store = get();

        const tempConnectionInstance = new HubConnectionBuilder()
            .withUrl(ENDPOINTS.PROMPTS_HUB)
            // @ts-expect-error(microsoft is shit)
            .configureLogging(logEntry => {
                const transportOptions = logEntry.data.transportOptions;
                transportOptions.headers = {
                    Cookie: document.cookie
                };
            })
            .build();

        tempConnectionInstance.on(store.GetAllDisposablePrompts.name, store.GetAllDisposablePrompts);

        tempConnectionInstance.on(store.GetCreateDisposablePromptSuccess.name, store.GetCreateDisposablePromptSuccess);
        tempConnectionInstance.on(store.GetCreateDisposablePromptError.name, store.GetCreateDisposablePromptError);

        tempConnectionInstance.on(store.GetDeleteDisposablePromptSuccess.name, store.GetCreateDisposablePromptSuccess);
        tempConnectionInstance.on(store.GetDeleteDisposablePromptError.name, store.GetDeleteDisposablePromptError);

        tempConnectionInstance.start().then(() => {
            console.log('ok')
        }).catch(error => {
            if (error.message && error.message.includes(`Status code '401'`)) {
                console.log('if')
            } else {
                console.log('else')
            }
        });


        set({connection: tempConnectionInstance});
    },

    temp: () => {
      const {connection} = get();

      connection!.invoke('CreateDisposablePromptAsync', {});
    },

    GetAllDisposablePrompts: (response) => {
        console.log('Hello from server');
        console.log(response)
    },

    GetCreateDisposablePromptSuccess: (response) => {
        console.log('success')
        console.log(response)
    },
    GetCreateDisposablePromptError: (response) => {
        console.log('error')
        console.log(response)
    },

    GetDeleteDisposablePromptSuccess: () => {
    },
    GetDeleteDisposablePromptError: () => {
    },

    GetUpdateDisposablePromptSuccess: () => {
    },
    GetUpdateDisposablePromptError: () => {
    }
}), {
    name: 'reminder-storage',
    storage: createJSONStorage(() => sessionStorage)
}));