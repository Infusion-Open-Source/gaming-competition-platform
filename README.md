gaming-competition-platform
===========================

This project is about building up gaming competition plaform from scratch.
Our target is to have game engine running game specific logic on a server and so numerous players will connect to it via UDP and will be able to participate.
Game will run on a server and all clients will be notified of a change in game state and so they can react to that and respond back with player actions.

That kind of platform will be useful to run competitions in a group of people where each participant is developing his/her AI playing the game.
Main goal is to have a lot of fun... not only while coding but also from the competitions.

General architecture summary
============================
- Client (Player) may run on same or different host in LAN
- Client (Player) might be written in anything that is able to use UDP
- Game is started by Controller client application
- There is Renderer client application showing state of the game
- Server is broadcasting game state in ASCII on UDP 
- Game is asynchronous; if you won't manage to respond to given state then you will run default action (go straight in Light Cycles game)
- There will be extra Human player client, so you can fight against the AIs

Light Cycles game
================

Gaming platform is target to be able to host different games.
But as a start point it will handle Light Cycles game.

Game brief description:
There is a 2D map on which players are riding Light Cycles (futuristic motorbikes leaving light trail behind them - like in Tron movie). 
The goal of game is to eliminate other players by setting own light trail in such a way that opponents won't escape and collide.

Game rules summary:
- Last living player wins the game
- If no living players in game then there is no winner
- When players hits a wall then dies
- When players hits trail of any other player (including self) then dies
- When player hits another player both players dies
- When player dies its trail is erased from map

Game planned features:
- Light trail will fade out on time
- There will be ability to run in team play mode
- There will be optional Fog of War mode, so the player will see only part of the map in the front.

Light Cycles game protocol
=========================

Server will broadcast message in pure ASCII wrapped by UDP.
Each massage will introduce the game and the version of protocol.
Game state will be printed out as an ASCI map with following meaning of each of the characters:

# - wall
" " - space, you can travel through

A - Player A position
a - Player A light trail

B - Player B position
b - Player B light trail

..

Z - Player Z position
z - Player Z light trail


Client responds with one of following commands:

"S","s" - go straight ahead (in current direction) 
"L","l" - turn left 
"R","r" - turn right

(Rockets and extra stuff will be added later on ;)
