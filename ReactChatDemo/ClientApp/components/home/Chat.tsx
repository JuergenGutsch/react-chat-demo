import * as React from 'react';
import * as moment from 'moment';

interface ChatState {
    messages: ChatMessage[];
    currentMessage: string;
}
interface ChatMessage {
    id: number;
    date: Date;
    message: string;
    sender: string;
}

export class Chat extends React.Component<{}, ChatState> {
    msg: HTMLInputElement;
    panel: HTMLDivElement;

    constructor() {
        super();
        this.state = { messages: [], currentMessage: '' };

        this.handlePanelRef = this.handlePanelRef.bind(this);
        this.handleMessageRef = this.handleMessageRef.bind(this);
        this.handleMessageChange = this.handleMessageChange.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
        this.addMessage = this.addMessage.bind(this);
    }

    public render() {
        return <div className='panel panel-default'>
            <div className='panel-body panel-chat'
                ref={this.handlePanelRef}>
                <ul>
                    {this.state.messages.map(message =>
                        <li key={message.id}><strong>{message.sender} </strong>
                            ({moment(message.date).format('HH:mm:ss')})<br />
                            {message.message}</li>
                    )}
                </ul>
            </div>
            <div className='panel-footer'>
                <form className='form-inline' onSubmit={this.onSubmit}>
                    <label className='sr-only' htmlFor='msg'>Amount (in dollars)</label>
                    <div className='input-group col-md-12'>
                        <button type='button' className='chat-button input-group-addon'>:-)</button>
                        <input type='text'
                            value={this.state.currentMessage}
                            onChange={this.handleMessageChange}
                            className='form-control'
                            id='msg'
                            placeholder='Your message'
                            ref={this.handleMessageRef} />
                        <button className='chat-button input-group-addon'>Send</button >
                    </div>
                </form>
            </div>
        </div>;
    }

    handlePanelRef(div: HTMLDivElement) {
        this.panel = div;
    }
    handleMessageRef(input: HTMLInputElement) {
        this.msg = input;
    }

    handleMessageChange(event: any) {
        this.setState({ currentMessage: event.target.value });
    }

    onSubmit(event: any) {
        event.preventDefault();
        this.addMessage();
    }

    addMessage() {
        let currentMessage = this.state.currentMessage;
        if (currentMessage.length === 0) {
            return;
        }
        let id = this.state.messages.length;
        let date = new Date();

        let messages = this.state.messages;
        messages.push({
            id: id,
            date: date,
            message: currentMessage,
            sender: 'juergen'
        })
        this.setState({
            messages: messages,
            currentMessage: ''
        });
        this.msg.focus();
        let div = this.panel;
        div.scrollTop = div.scrollHeight - div.clientHeight;
    }
}
