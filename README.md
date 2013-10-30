Specification Document
Version 1.0
Last update: 30/10/2013


Who we are?
===========

Bunch of guys working in Infusion building this projects just for fun and learn from each other :)


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
- Game will run continuously, game state will update defined times per second.

Game modes:
- Free for All - each player plays for its own, last player on map wins the game. There might be up to X players. No winner game result is possible.
- Team Death match - each player is assigned to a team. There might be up to X teams. If only one team lasts on the map then it wins. No winner game result is possible.

Light Cycles - Need more info / help?
=====================================

Refer to .\Infusion\Games\LightCycles\Readme.txt file.

Contact
=======

In queries realted to those sources feel free to reach me pdrozdowski AT infusion DOT com

