import * as SignalR from '@aspnet/signalr-client';
import 'isomorphic-fetch';


export class UsersService {
    private socketCallback: any;

    constructor(socketCallback: any) {

        this.socketCallback = socketCallback;

        var transport = SignalR.TransportType.WebSockets;
        let logger = new SignalR.ConsoleLogger(SignalR.LogLevel.Information);

        // Connection erzeugen
        var connection = new SignalR.HubConnection(`http://${document.location.host}/chat`,
            { transport: transport, logging: logger });

        // Chat-Nachrichten vom Server empfangen
        connection.on('UserLoggedOn', (id, name) => {
            socketCallback({
                id: id,
                name: name
            })
        });
    }

    public fetchLogedOnUsers(fetchUsersCallback: any) {

        fetch('api/Chat/LoggedOnUsers')
            .then(response => response.json() as Promise<User[]>)
            .then(data => {
                fetchUsersCallback(data);
            });
    }

}

export interface User {
    id: number;
    name: string;
}