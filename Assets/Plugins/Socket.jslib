mergeInto(LibraryManager.library, {
    WebSocketInit: (url) => {
        this.socket = new WebSocket(Pointer_stringify(url));
        this.socket.onmessage = (msg) => {
            const data = JSON.parse(msg.data);
            console.log(data.message);
            unityInstance.SendMessage(
                "receivedMessages",
                "handleReceiveMessage",
                data.message
            );
        };
    },
});

function onMessageReceived(message) {
    switch (message.action) {
        case 'init1':
            init1(message);
            break;
        case 'init2':
            break;
        case 'played_init1':
            break;
        case 'played_init2':
            break;
    }
}

function init1(message) {
    
}