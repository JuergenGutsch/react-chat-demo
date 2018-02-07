import * as React from 'react';

interface UsersState {
    users: User[];
}
interface User {
    id: number;
    name: string;
}

export class Users extends React.Component<{}, UsersState> {
    constructor() {
        super();
        this.state = {
            users: [
                { id: 1, name: 'juergen' },
                { id: 3, name: 'marion' },
                { id: 2, name: 'peter' },
                { id: 4, name: 'mo' }]
        };
    }

    public render() {
        return <div className='panel panel-default'>
            <div className='panel-body'>
                <h3>Users online:</h3>
                <ul className='chat-users'>

                    {this.state.users.map(user =>
                        <li key={user.id}>{user.name}</li>
                    )}
                </ul>
            </div>
        </div>;
    }
}
