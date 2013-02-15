gaming-competition-platform
===========================

Game brief description:
There is a 2D map on which players are riding light cycles (futuristic motorbikes leaving light trail behind them). The goal of game is to eliminate other players by setting own light trail in such a way that opponents won't escape and collide.

Game rules summary:
- last living player wins the game
- if no living players in game then there is no winner
- when players hits a wall then dies
- when players hits trail of any other player (including self) then dies
- when player hits antoher player both players dies
- when player dies its trail is erased from map

Game planned features:
- light trail will fade on time
- there will be team play
- there will be kind of Fog Of War, so you'll see only part of map in front of you.

Architecture summary:
- Client may run on same or different host in LAN
- Client might be in enything that is able to use UDP
- Game is started by Controller client application
- There is Reneder client application showing state of game
- Server is broadcasting in ASCII on UDP entrie map to clients X times per second
- Game is asynchronous, if you won't manage to respond to given state in 1sec/X time then you are going streight
- There will be extra Human player client


Ascii protocol overview:

Server will broadcast message:

-- cut here --
Version 1.0
Frame 1
#########
# aa  B #
#  A  b #
#    bb #
#########
-- cut here --

Legend:
# - wall
" " - space, you can travel through

A - player A position
a - player A light trail

B - player B position
b - player B light trail


Client will respond with one of folowing:

"S","s" - go streight (in current direction) "L","l" - turn left "R","r" - turn right

(rockets and extra stuff will be added later on ;)
