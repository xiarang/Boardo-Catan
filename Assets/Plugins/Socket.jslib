mergeInto(LibraryManager.library, {
    showDialogWithImage: function (label, body, image) {
        const title = Pointer_stringify(label);
        const text = Pointer_stringify(body);
        const imageUrl = Pointer_stringify(image);
        Swal.fire({
            title: title,
            text: text,
            imageUrl: imageUrl,
            imageWidth: 200,
            imageHeight: 200
        });
    },
    showDialog: function (label, body) {
        const title = Pointer_stringify(label);
        const text = Pointer_stringify(body);
        Swal.fire({
            title: title,
            text: text,
        });
    },
    showLoading: function() {
        Swal.fire({
            title: "منتظر سایر بازیکنان",
            text: "لطفاً تا تکمیل ظرفیت اتاق بازی منتظر بمانید",
            icon: "warning",
            allowOutsideClick: false,
            allowEscapeKey: false,
            showConfirmButton: false,
            backdrop: "rgba(0,0,0,0.75)"
        });
    },
    rollDice: function (dice1, dice2) {
        const d1 = Pointer_stringify(dice1);
        const d2 = Pointer_stringify(dice2);
        rollADie({element: document.getElementById("dice-container"), numberOfDice: 2, callback: function(a) {}, values: [parseInt(d1), parseInt(d2)]});
    },
    InitGame: function () {
        const token = localStorage.getItem("token");
        console.log("token is read from ls", token);
        window.unityInstance.SendMessage("GameController", "SetToken", token);
    },
    WebSocketInit: function (url) {
        this.socket = new WebSocket(Pointer_stringify(url));
        this.socket.onmessage = function (msg) {
            const data = JSON.parse(msg.data);
            const x = onMessageReceived(data);
            window.unityInstance.SendMessage(
                x[0],
                x[1],
                x[2]
            );
        };
    },
});

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
        case 'play_monopoly':
            return playMonopoly(message);
        case 'play_knight_card':
            return playKnightCard(message);
        case 'dice':
            return dice(message);
        case 'played_dice':
            return playedDice(message);
        case 'thief_tile':
            return thiefTile(message);
        case 'trade_buy_build':
            return tradeBuyBuild(message);
        case 'build_home':
            return buildHome(message);
        case 'build_road':
            return buildRoad(message);
        case 'build_city':
            return buildCity(message);
        case 'bought_development_card':
            return boughtDevelopmentCard(message);
        case 'trade_offer':
            return tradeOffer(message);
        case 'answered_trade':
            return answeredTrade(message);
        case 'accepted_trade':
            return acceptedTrade(message);
        case 'trade_bank':
            return tradeBank(message);
        case 'finish':
            return finish(message);
        default:
            console.log("Unknown action:", message.action);
            break;
    }
}

function init1(x) {
    return ['GameController', 'Init1', [x.turn]];
}

function init2(x) {
    return ['GameController', 'Init2', [x.turn]];
}

function playedInit1(x) {
    return ['GameController', 'PlayedInit1', [x.turn, x.args.vertex, x.args.road_v1, x.args.road_v2]];
}

function playedInit2(x) {
    return ['GameController', 'PlayedInit2', [x.turn, x.args.vertex, x.args.road_v1, x.args.road_v2]];
}

function playYearOfPlenty(x) {
    return ['GameController', 'PlayYearOfPlenty', [x.turn, x.args.resource1, x.args.resource2]];
}

function playRoadBuilding(x) {
    return ['GameController', 'PlayRoadBuilding', [x.turn, x.args.road1_vertex1, x.args.road1_vertex2, x.args.road2_vertex1, x.args.road2_vertex2]];
}

function playMonopoly(x) {
    return ['GameController', 'PlayMonopoly', [x.turn, x.args.resource]];
}

function playKnightCard(x) {
    return ['GameController', 'PlayKnightCard', [x.turn, x.args.tile]];
}

function dice(x) {
    return ['GameController', 'Dice', [x.turn]];
}

function playedDice(x) {
    rollADie({element: document.getElementById("dice-container"), numberOfDice: 2, callback: function(a) {}, values: [parseInt(x.args.dice1), parseInt(x.args.dice2)]});
    return ['GameController', "PlayedDice", [x.turn]];
}

function thiefTile(x) {
    return ['GameController', 'ThiefTile', [x.turn]];
}

function tradeBuyBuild(x) {
    return ['GameController', 'TradeBuyBuild', [x.turn]];
}

function buildHome(x) {
    return ['GameController', 'BuildHome', [x.turn, x.args.vertex]];
}

function buildRoad(x) {
    return ['GameController', "BuildRoad", [x.turn, x.args.vertex1, x.args.vertex2]];
}

function buildCity(x) {
    return ['GameController', 'BuildCity', [x.turn, x.args.vertex]];
}

function boughtDevelopmentCard(x) {
    return ['GameController', 'BoughtDevelopmentCard', [x.turn]];
}

function tradeOffer(x) {
    return ['GameController', "TradeOffer", [x.turn, x.args]];
}

function answeredTrade(x) {
    return ['GameController', 'AnsweredTrade', [x.player, x.args.answer, x.args.id]];
}

function acceptedTrade(x) {
    return ['GameController', "AcceptedTrade", [x.turn, x.args.player]];
}

function tradeBank(x) {
    return ['GameController', "TradeBank", [x.turn, x.args.give, x.args.want]];
}

function finish(x) {
    return ['GameController', "Finish", [x.winner]];
}