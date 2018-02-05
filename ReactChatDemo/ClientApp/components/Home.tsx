import * as React from 'react';
import { RouteComponentProps } from 'react-router';

interface ChatsState {
    messages: ChatMessage[];
    currentMessage: string;
}
interface ChatMessage {
    id: number;
    date: Date;
    message: string;
}

export class Home extends React.Component<RouteComponentProps<{}>, ChatsState> {
    msg: any = {};

    constructor() {
        super();
        this.state = { messages: [], currentMessage: '' };

        this.handleMessageRef = this.handleMessageRef.bind(this);
        this.handleMessageChange = this.handleMessageChange.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
        this.addMessage = this.addMessage.bind(this);
    }

    public render() {
        return <div>
            <div className='panel panel-default'>
                <div className='panel-body panel-chat'>
                    <ul>
                        {this.state.messages.map(message =>
                            <li key={message.id}>[Juergen] ({message.date.getMinutes}:{message.date.getSeconds}) {message.message}</li>
                        )}
                    </ul>
                </div>
                <div className='panel-footer'>
                    <form className='form-inline' onSubmit={this.onSubmit}>
                        <label className='sr-only' htmlFor='msg'>Amount (in dollars)</label>
                        <div className='input-group col-md-12'>
                            <div className='chat-emoji-button input-group-addon'>:-)</div>
                            <input type='text' value={this.state.currentMessage}
                                onChange={this.handleMessageChange}
                                className='form-control'
                                id='msg'
                                placeholder='Your message'
                                ref={this.handleMessageRef} />
                        </div>
                    </form>
                </div>
            </div>
        </div>;
    }

    handleMessageRef(input: HTMLInputElement) {
        this.msg = input;
    }

    handleMessageChange(event: any) {
        this.setState({ currentMessage: event.target.value });
    }

    onSubmit(event: any) {
        this.addMessage();
        event.preventDefault();
        return false;
    }

    addMessage() {
        let msg = this.state.messages;
        let id = this.state.messages.length;
        msg.push({ id: id, date: new Date(), message: this.state.currentMessage })
        this.setState({
            messages: msg,
            currentMessage: ''
        });
        this.msg.focus();
    }
}
