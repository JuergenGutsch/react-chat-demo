import 'isomorphic-fetch';

import WebsocketService from './WebsocketService'

export class ChatService {
    private _messageAdded: any;

    constructor(messageAdded: any) {
        this._messageAdded = messageAdded;

        // Chat-Nachrichten vom Server empfangen
        WebsocketService.registerMessageAdded((message: ChatMessage) => {
            this._messageAdded(message);
        });
    }

    public addMessage(message: string) {
        WebsocketService.sendMessage(message);
    }

    public fetchInitialMessages(fetchInitialMessagesCallback: any) {
        fetch('api/Chat/InitialMessages')
            .then(response => response.json() as Promise<ChatMessage[]>)
            .then(data => {
                fetchInitialMessagesCallback(data);
            });
    }
}

export interface ChatMessage {
    id: number;
    date: Date;
    message: string;
    sender: string;
}