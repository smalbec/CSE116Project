const http = require('http');
const express = require('express');
const socketIo = require('socket.io');

const app = express();
const server = http.createServer(app);
const io = socketIo(server);

const Player = require('./classes/player');

var players = []; //dictionary that maps playerID to playerClasses
var sockets = []; //array of sockets
var gamestate = {"players": []};
var eggstate = {"PlayerDummy":true,"PlayerDummy (12)":true,"PlayerDummy (11)":true,"PlayerDummy (20)":true,"PlayerDummy (24)":true,"PlayerDummy (15)":true,"PlayerDummy (6)":true,"PlayerDummy (21)":true,"PlayerDummy (9)":true,"PlayerDummy (8)":true,"PlayerDummy (19)":true,"PlayerDummy (7)":true,"PlayerDummy (3)":true,"PlayerDummy (10)":true,"PlayerDummy (4)":true,"PlayerDummy (2)":true,"PlayerDummy (16)":true,"PlayerDummy (17)":true,"PlayerDummy (1)":true,"PlayerDummy (18)":true,"PlayerDummy (22)":true,"PlayerDummy (5)":true,"PlayerDummy (14)":true,"PlayerDummy (13)":true,"PlayerDummy (23)":true,"PlayerDummy (21)(Clone)":true,"PlayerDummy (20)(Clone)":true}

io.on('connect', (client) => {
    console.log('user connected with sid: ' + client.id);

    var player = new Player();

    players[player.id] = player; //adds player to list
    sockets[player.id] =  client; //adds socket to list

    //tell client that this is their id for server
    console.log('Registering player with ID: ' + player.id);
    client.emit('register',{id: player.id}); //sends register message to client with JSON id mapping to player ID
    client.emit('spawn', player); //sends spawn message to tell client they have spawned
    client.broadcast.emit('spawn', player); //sends spawn message to every other client/socket to tell that this client has spawned

    //tell client about every other client
    for(var playerID in players) {
        if(playerID !== player.id) { //if the playerID isn't our own
            client.emit('spawn', players[playerID]); //spawn the other player(s)
        }
    }

    client.on('disconnect', () => {
        console.log('user disconnected with sid: ' + client.id);
        delete  players[player.id]; //removes player from list on disconnect
        delete sockets[player.id]; //removes socket from list on disconnect
        client.broadcast.emit('disconnected', player);
    });
	
	client.on('send-gs',(data) => {
        var playerdict = {};
        data = JSON.parse(data);
		for (var piss in gamestate["players"]) {
			if (gamestate["players"][piss]["id"] == data["id"]) {
				//console.log(gamestate["players"][piss]["id"]);
				gamestate["players"][piss]["x"] = (parseFloat(data["x"])).toString();
				gamestate["players"][piss]["y"] = data["y"].toString();
			}
		}
		//console.log(gamestate["players"]);
    });
	
	client.on('update-gs', (data) => {
		var playerdict = {};
		//console.log(gamestate["players"][0]);
		data = JSON.parse(data);
		for (var piss in gamestate["players"]) {
			if (gamestate["players"][piss]["id"] != data) {
				client.emit('gs', gamestate["players"][piss]);
			}
		}
	});
	
	client.on('append-player', (data) => {
		//console.log(data);
		var playerdic = {"id": JSON.parse(data)["id"], "x": JSON.parse(data)["x"].toString(), "y": JSON.parse(data)["y"].toString()};
		gamestate["players"].push(playerdic);
		console.log(playerdic);
	});
	
	client.on('remove-egg', (data) => {
		data = JSON.parse(data);
		for (var egg in Object.keys(data)) {
			//if (eggstate[egg] = true && data[egg] == false) {
				eggstate[egg] = false
			//}
		}
		//console.log(data);
	});
	
	client.on('get-eggs', () => {
		client.emit('update-eggs', eggstate);
	});
});

server.listen(5000, () => {
    console.log('listening on *:5000');
});