gaming-competition-platform
===========================

This project is about building up gaming competition plaform from scratch.
Our target is to have game engine runing game specific logic on a server and so numerous players will connect to it via UDP and will be able to participate.
Game will run on a server and all clients will be notified of a changes in game state and so they can react to that and respond back with player actions.

That kind of platform will be useful to run competitions in a group of people where each participant is developing his/her AI playing the game.
Main goal is to have a lot of fun... not only while coding but also from the copetitions.

General architecture summary
============================
- Client (Player) may run on same or different host in LAN
- Client (Player) might be written in anything that is able to use UDP
- Game is started by Controller client application
- There is Reneder client application showing state of the game
- Server is broadcasting game state in ASCII on UDP 
- Game is asynchronous, if you won't manage to respond to given state then you will run default action (go straight in LightCycles game)
- There will be extra Human player client, so you can fight against the AIs

LightCycles game
================

Gaming platform is target to be able to host different games.
But as a start point it will handle LightCycles game.

Game brief description:
There is a 2D map on which players are riding LightCycles (futuristic motorbikes leaving light trail behind them - like in Tron movie). 
The goal of game is to eliminate other players by setting own light trail in such a way that opponents won't escape and collide.

Game rules summary:
- last living player wins the game
- if no living players in game then there is no winner
- when players hits a wall then dies
- when players hits trail of any other player (including self) then dies
- when player hits antoher player both players dies
- when player dies its trail is erased from map

Game planned features:
- light trail will fade out on time
- there will be ability to run in team play mode
- there will be optional Fog Of War mode, so the player will see only part of the map in the front.

LightCycles game protocol
=========================

Server will broadcast message in pure ASCII wrapped by UDP.
Each massage will introduce the game and the version of protocol.
Game state will be printed out as an ASCI map with following meaning of each of the characters:

# - wall
" " - space, you can travel through

A - player A position
a - player A light trail

B - player B position
b - player B light trail

..

Z - player Z position
z - player Z light trail


Client responds with one of folowing commands:

"S","s" - go straight ahead (in current direction) 
"L","l" - turn left 
"R","r" - turn right

(rockets and extra stuff will be added later on ;)
