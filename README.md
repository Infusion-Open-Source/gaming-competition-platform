Specification Document
Version 0.1
Last update: 18/03/2013


Who we are?
===========

Bunch of guys working in Infusion building this projects just for fun and learn from each other :)


Gaming Competition Platform
===========================

This project is about building up gaming competition platform from scratch.
Our target is to have game engine running game specific logic on a server and so numerous players will connect to it via UDP and will be able to participate.
Game will run on a server and all clients will be notified of a change in game state and so they can react to that and respond back with player actions.

That kind of platform will be useful to run competitions in a group of people where each participant is developing his/her AI playing the game.
Main goal is to have a lot of fun... not only while coding but also from the competitions.

We would like to have gaming platform able to host different games but as a starting point it should just handle Light Cycles game.
Later on... maybe tanks....


General Architecture Overview
=============================
- Client (Player) may run on same or different host in LAN
- Client (Player) might be written in anything that is able to use UDP
- Game is started by Controller client application
- There is Renderer client application showing state of the game
- Server is broadcasting game state in ASCII on UDP 
- Game is asynchronous; if you won't manage to respond to given state then you will run default action (go straight in Light Cycles game)
- There will be extra Human player client, so you can fight against the AIs


Light Cycles - Game
===================

Game brief description:

The goal of game is to eliminate other players by setting own light trail in such a way that opponents won't escape and collide. 
It's obvious that each player should avoid collision as long as possible.

Game plays on a 2D map on which players are riding Light Cycles (futuristic motorbikes leaving light trail behind them - like in Tron movie). 
Map consists of locations that you can travel through and impassable ones. 
Maximum size of the map is 100x100.

General game rules:
- When players hits a wall then dies.
- When players hits trail of any other player (including self) then dies.
- When player hits another player both players dies.
- When player dies his/her trail is erased from the map.
- When player does nothing then goes straight ahead.
- Game will run continuously game state will update 10 times per second.

Game modes:
- Free for All - each player plays for its own, last player on map wins the game. There might be up to X players. No winner game result is possible.
- Team Death match - each player is assigned to a team. There might be up to X teams. If only one team lasts on the map then it wins. No winner game result is possible.
- Continuous - game runs without end condition. Players can join while game is running. Play is like Free for All.

Game planned features:
- Light trail will fade out with time (controlled by parameter)
- There will be optional Fog of War mode, so the player will see only part of the map in the front, not entire map.

Game clients:
- Player - hosts game player, receives game broadcasted updates and responds to it.
- Random bot - hosts computer player, receives game broadcasted updates and responds to it. Goes straight and chooses random direction to avoid collision.
- View - receives game broadcasted updates and renders the state.
- Controller - allows to remotely change game server state, mode, turn on/off features, set up teams, restarts…. and so on.


Light Cycles - Networking
=========================

General communication idea:
There is one server in LAN which broadcast game state in pure ASCII wrapped by UDP.
Each massage introduces version of protocol to keep clients compatible.

When client connects to server for the first time, it receives GUID which will be identifier of this client during game time.
Client should send each message with a GUID to confirm he is the true sender of the message.

Game state will be printed out as an ASCII map with following meaning of each of the characters:

'sharp sign' - wall, impassable
" " - space, you can travel through

A - Player A position
a - Player A light trail
B - Player B position
b - Player B light trail
...
Z - Player Z position
z - Player Z light trail

. - dot stands for fog in FoW mode

There will be also set of extra information included, like following:
- Version of protocol
- Players team assignment if applicable
- Turn number

Client will respond with one of following commands:

GUID - id of client
"S","s" - go straight ahead (keep current direction) 
"L","l" - turn left
"R","r" - turn right

There might be need of splitting game update broadcast message to smaller chunks - to be verified.

Light Cycles - Client
=====================

Client can be made in ANY technology that is able to send/receive UDP packets.
Game client will play agent logic, sending to server agent behaviour after receiving broadcasted game state update from server.
Client can send more than one action but only last will be taken into consideration.

Light Cycles - View
===================

View client listens to game state changes broadcasted by server and renders game progress in smooth and effective way.
Behaviour of display should be rather dynamic for better game look and feel and more interesting entertainment.

Camera modes:
- Follow - follows one player keeping him/her in the middle of the screen, can switch from player to player.
- Follow all - follows all players at once, automatically zooms in/out to keep all on a screen
- Full - shows entire map, this view is static 

Features:
- it should be possible to toggle extra on screen info like.: player/team names, current game mode, protocol version, turn number

Coloristic: (inspired with image attached - concept1.png, found on http://www.design-laorosa.com/2011/01/creative-iphone-ipod-touch-home-screen.html)
- Player light cycle and trail should be able to be rendered in different colours. 
- Trail and bike should glow and be a bit blurry. 
- All moves from one game state to another should be displayed in smooth way.
- When playing in team mode each team will have one dedicated colour.
- Background will be in dark colours displaying grid on which players will ride.
- When player dies it should fade out.
- Players trail should be narrower at the end.

Should there be a sound? - To be defined.

