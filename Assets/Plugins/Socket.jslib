mergeInto(LibraryManager.library, {
    WebSocketInit: (url) => {
        this.socket = new WebSocket(Pointer_stringify(url));
        this.socket.onmessage = (msg) => {
            const data = JSON.parse(msg.data);
            const [object, method, args] = onMessageReceived(data);
            unityInstance.SendMessage(
                object,
                method,
                args
            );
        };
    },
    showDialog: (title, body) => {
        swal(title, body);
    }
});

const gameObject = 'GameController';

function onMessageReceived(message) {
    switch (message.action) {
        case 'init1':
            return init1(message);
        case 'init2':
            return init2(message);
        case 'played_init1':
            return playedInit1(message);
        case 'played_init2':
            return playedInit2(message);
        case 'play_year_of_plenty':
            return playYearOfPlenty(message);
        case 'play_road_building':
            return playRoadBuilding(message);
    }
}

function init1({turn}) {
    return [gameObject, 'PlayInit1', [turn]];
}

function init2({turn}) {
    return [gameObject, 'PlayInit2', [turn]];
}

function playedInit1({args, turn}) {
    const {vertex, road_v1, road_v2} = args;
    return [gameObject, 'PlayedInit1', [turn, vertex, road_v1, road_v2]];
}

function playedInit2({args, turn}) {
    const {vertex, road_v1, road_v2} = args;
    return [gameObject, 'PlayedInit2', [turn, vertex, road_v1, road_v2]];
}

function playYearOfPlenty({args, turn}) {
    const {resource1, resource2} = args;
    return [gameObject, 'PlayYearOfPlenty', [turn, resource1, resource2]];
}

function playRoadBuilding({args, turn}) {
    const {road1_vertex1, road1_vertex2, road2_vertex1, road2_vertex2} = args;
    return [gameObject, 'PlayRoadBuilding', [turn, road1_vertex1, road1_vertex2, road2_vertex1, road2_vertex2]];
}

function playMonopoly({args, turn}) {
    const {resource} = args;
    return [gameObject, 'PlayMonopoly', [turn, resource]];
}

function playKnightCard({args, turn}) {
    const {tile} = args;
    return [gameObject, 'PlayKnightCard', [turn, tile]];
}

function dice({turn}) {
    return [gameObject, 'Dice', [turn]];
}

function playedDice({args, turn}) {
    const {dice1, dice2} = args;
    return [gameObject, "PlayedDice", [turn]];
} 