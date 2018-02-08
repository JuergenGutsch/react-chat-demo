import * as React from 'react';
import * as moment from 'moment';

import { ChatService, ChatMessage } from '../../services/ChatService';

interface ChatState {
    messages: ChatMessage[];
    currentMessage: string;
}

export class Chat extends React.Component<{}, ChatState> {
    msg: HTMLInputElement;
    panel: HTMLDivElement;

    private chatService: ChatService = new ChatService(this.handleOnSocket);

    constructor() {
        super();
        this.state = {
            messages: [],
            currentMessage: ''
        };

        this.handleOnInitialMessagesFetched = this.handleOnInitialMessagesFetched.bind(this);
        this.handleOnSocket = this.handleOnSocket.bind(this);
        this.handlePanelRef = this.handlePanelRef.bind(this);
        this.handleMessageRef = this.handleMessageRef.bind(this);
        this.handleMessageChange = this.handleMessageChange.bind(this);
        this.onSubmit = this.onSubmit.bind(this);

        this.chatService.fetchInitialMessages(this.handleOnInitialMessagesFetched);
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

    handleOnInitialMessagesFetched(messages: ChatMessage[]) {
        this.setState({
            messages: messages
        });

        this.scrollDown(this);
    }

    handleOnSocket(message: ChatMessage) {
        let messages = this.state.messages;
        messages.push(message);
        this.setState({
            messages: messages
        });
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
        this.addMessage(this);
    }

    private addMessage(that: Chat) {
        let currentMessage = that.state.currentMessage;
        if (currentMessage.length === 0) {
            return;
        }
        let id = that.state.messages.length + 1;
        let date = new Date();

        let messages = that.state.messages;
        messages.push({
            id: id,
            date: date,
            message: currentMessage,
            sender: 'Juergen'
        })
        that.setState({
            messages: messages,
            currentMessage: ''
        });
        that.focusField(that);
        that.scrollDown(that);
    }

    private focusField(that: Chat) {
        that.msg.focus();
    }

    private scrollDown(that: Chat) {
        let div = that.panel;
        div.scrollTop = div.scrollHeight;
    }
}
