import 'isomorphic-fetch';

import WebsocketService from './WebsocketService'

export class UsersService {
    private _userLoggedOn: any;

    constructor(socketCallback: any) {
        this._userLoggedOn = socketCallback;
        
        // Chat-Nachrichten vom Server empfangen
        WebsocketService.registerUserLoggedOn((id: number, name: string) => {
            this._userLoggedOn({
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