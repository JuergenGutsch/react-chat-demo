import 'isomorphic-fetch';

import WebsocketService from './WebsocketService'
import { User } from '../services/Models/User';

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

    public fetchLogedOnUsers(fetchUsersCallback: (msg: User[]) => void) {
        fetch('api/Chat/LoggedOnUsers')
            .then(response => response.json() as Promise<User[]>)
            .then(data => {
                fetchUsersCallback(data);
            });
    }
}