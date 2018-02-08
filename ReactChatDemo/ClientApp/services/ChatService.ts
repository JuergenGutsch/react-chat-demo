import * as SignalR from '@aspnet/signalr-client';
import 'isomorphic-fetch';


export class ChatService {
    private socketCallback: any;

    constructor(socketCallback: any) {

        this.socketCallback = socketCallback;

        var transport = SignalR.TransportType.WebSockets;
        let logger = new SignalR.ConsoleLogger(SignalR.LogLevel.Information);

        // Connection erzeugen
        var connection = new SignalR.HubConnection(`http://${document.location.host}/chat`,
            { transport: transport, logging: logger });

        // Chat-Nachrichten vom Server empfangen
        connection.on('NewMessage', (message: ChatMessage) => {
            socketCallback(message);
        });
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