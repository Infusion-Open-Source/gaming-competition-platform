Here's a short QA

How to write a bot?
===================
Easiest way is to use working example and do the appropriate changes on it.
Please refer to sources for two bots provided, you can find them in .\Players\Bots\


What input will my player get from the game engine and how should I respond?
============================================================================

Message you will receive are structured in a following way:
First you will be asked are you ready? You need to reply that certainly you are! 
Game waits for all the players to acknowledge that they're up and running.

IN> Ready?
<OUT Ready!

Next you will get game basic setup information

IN> You Are: D
IN> Players: ABCDEFGH
IN> Teams: ABCDEFGH
IN> Game mode: FreeForAll

You don't reply anything to that, Game will tell you what type of game you are playing, how many players there are and what letter they have... also will tell you which player you are.
For Team game it will look like follows:

IN> You Are: D
IN> Players: ABCDEFGH
IN> Teams: AAAABBBB
IN> Game mode: TeamDeathMatch

This shows you also which players belongs to which team: so A,B,C,D are playing in team A against E,F,G,H in team B.

After that you will receive game state data on given turn, that is something you have to analyze (at least you should ;)):

IN> Turn: 1
IN> 21
IN> #####################
IN> #####################
IN> #####################
IN> .###################.
IN> ..#################..
IN> ...###############...
IN> ....#############....
IN> .....###########.....
IN> ......##       ......
IN> .......       .......
IN> ........  D  ........
IN> ......... d .........
IN> .......... ..........
IN> .....................
IN> .....................
IN> .....................
IN> .....................
IN> .....................
IN> .....................
IN> .....................
IN> .....................
IN>

It tells us that it's a turn 1 in the game and there is 21 rows of data to read.
After reading it you will know what you actually 'see' in front of you in the game world.
We are player D and our bike is where letter D is. Behind us is one grid with our light cycle, it's marked as 'd'.
Signs # in front tells us that we are heading towards huge obstacle in a front, and '.' characters means a fog of war (something we don't see).

We reply back to game engine with just a one character depending on our decision:
  S - go straight
  L - turn left
  R - go right

In this case let's say that obstacle is looking scary so lets turn left.

<OUT L

After while game will process all the inputs and will generate next turn data. In our example it will be:

IN> Turn: 2
IN> 21
IN>       ###############
IN>        ##############
IN>         #############
IN> .        ###########.
IN> ..        #########..
IN> ...        #######...
IN> ....        #####....
IN> .....       ####.....
IN> ......      ###......
IN> .......      #.......
IN> ........  D  ........
IN> .........dd .........
IN> .......... ..........
IN> .....................
IN> .....................
IN> .....................
IN> .....................
IN> .....................
IN> .....................
IN> .....................
IN> .....................
IN> 

So you see we have turned left... and game engine gives us once again data in the direction we are looking.
Later on we met player B:

IN> Turn: 20
IN> 21
IN> ########## b        a
IN> #########  b        a
IN> ########   b        a
IN> .#######   b       #.
IN> ..#####    b     ##..
IN> ...####    b    ##...
IN> ....###    b   ##....
IN> .....#     b  ##.....
IN> ......     b  #......
IN> .......  d B  .......
IN> ........ dD  ........
IN> .........ddd.........
IN> ..........d..........
IN> .....................
IN> .....................
IN> .....................
IN> .....................
IN> .....................
IN> .....................
IN> .....................
IN> .....................
IN> 

This kind of communication continues until end of the game, in this example quite quickly:

IN> Turn: 24
IN> 1
IN> You are dead!
IN> 

Which obviously says, that we were not too fortunate in this game.


What's the meaning of ASCII values I receive from game engine?
==============================================================

Characters has following meaning:

    # - means obstacle
    ' ' (space) - means free space I can travel through
    '.' (dot) - means fog of war
    A,B,C...Z - means bike of a specific player, so 'A' is a bike of player A
    a,b,c...z - means light trail of a specific player, so 'a' is a light trail of player A

example on a map with 8 players (you will find data like this in game logs):

##################################################
###    ###########################################
##      ##########################################
#        #########################################
#        #########################################
#        #########################################
##        ########################################
###     Ccc#######################################
########  c                       ################
######### c                          #############
##########c                             ##########
##########cccc                   eeeeeeeee########
#########                                E  ######
########                                 Gggg ####
####                                        g ####
###       #######################           g  ###
##       ###########################        g   ##
#aaaA    ############################       g   ##
#a       #############################      g   ##
#a       ##############################     g   ##
#a       ###############################         #
#aaa     ###############################         #
#        ################################        #
#        ################################        #
#        ################################        #
#        ################################        #
#        ################################        #
#        ################################        #
#        ################################        #
#    Bb  ################################        #
#     bb ################################        #
#      bb################################        #
#      bb################################        #
#      bb################################        #
#        ################################        #
#         ###############################  hh    #
#         ############################## hhhh    #
##         ############################  hh     ##
##          ##########################    h     ##
##           ########################     H     ##
###             ##################ffF          ###
####                              ff          ####
#####                            fff          ####
######                            ff         #####
#######           Dd                        ######
########           ddd                    ########
##########       ddddd                  ##########
#############                        #############
#################                #################
##################################################


How to add a new player?
========================
Let's assume we want create new "SuperBot" player.
We need to have 2 files to do that: SuperBot.exe and PlayerInfo.xml
In PlayerInfo.xml we can define what will be the name of our player, it's trail color and how to run our player.
Here's an example of xml file content:

	<?xml version="1.0" encoding="utf-8"?>
	<PlayerInfo Name="SuperBot" TrailColor="200,0,190" ExePath="SuperBot.exe" />

So I've set name to be SuperBot, color to be my favorite: Pink, and I told game engine that player is started just by running the exe file. 
Next I need to make a folder under .\Players directory like .\Players\SuperBot and put both files out there.

Last step is to add my new bot to players repository file .\TeamsAndPlayers.xml 
I need to add extra PlayerConfig line to make sure game knows about my player setup.
Note the root path of player directories.

...
    <TeamsAndPlayers PathRoot=".\Players\">
	<PlayerConfigs>
		<PlayerConfig Name="LineFollower" Config="Bots\LineFollower\PlayerInfo.xml" />
		<PlayerConfig Name="RandomPig" Config="Bots\RandomPig\PlayerInfo.xml" />
		<PlayerConfig Name="SuperBot" Config="SuperBot\PlayerInfo.xml" />
	</PlayerConfigs>
...


So I've told game engine that I will refer later on to my bot by name "SuperBot" and it's player info can be found in .\Players\SuperBot\PlayerInfo.xml
Now the player has been added to the game structure.


How to make a new player play custom game?
==========================================
Let's assume that I've added new player named "SuperBot" to game structure (as above) and now I would like to run a game in which my new bot will be dueling 7 random bots on some random map in Free For All game play. 
To do so, first I need to make XML file with game players setup, let's put it here: .\Players\SuperBot_vs_RandomBots.xml and content like this:

<?xml version="1.0" encoding="utf-8"?>
<RunSettings>
	<TimeLimit>1000</TimeLimit> <!-- number of milliseconds game waits for player response each turn -->
	<ViewArea>10</ViewArea> <!-- player view radius -->
	<FogOfWar>true</FogOfWar> <!-- is fog of war enabled -->
	<PlayerMappings>
		<Mapping Id="A" Name="SuperBot" /> <!-- my new player should start on game slot A -->
		<Mapping Id="B" Name="RandomPig" /> <!-- on other slots we have random bot -->
		<Mapping Id="C" Name="RandomPig" />
		<Mapping Id="D" Name="RandomPig" />
		<Mapping Id="E" Name="RandomPig" />
		<Mapping Id="F" Name="RandomPig" />
		<Mapping Id="G" Name="RandomPig" />
		<Mapping Id="H" Name="RandomPig" />
	</PlayerMappings>
	<TeamMappings/> <!-- no team mapping need for FFA game mode -->
</RunSettings>

Now I have a players file I can feed into game engine, let's setup game itself.
To do that we need another XML let's call it .\Maps\myRandomMap.xml with following content:

<?xml version="1.0" encoding="utf-8"?>
<GameSettings>
	<GameMode>FreeForAll</GameMode>
	<PlayerSlotAssignment>ABCDEFGH</PlayerSlotAssignment>
	<TeamSlotAssignment>ABCDEFGH</TeamSlotAssignment>
	<MapSource>Generate</MapSource>
	<MapName>Random</MapName>
	<MapFileName></MapFileName>
	<MapWidth>50</MapWidth>
	<MapHeight>20</MapHeight>
	<TrailAging>0.2</TrailAging>
	<ObstacleRatio>0.1</ObstacleRatio>
	<CleanMoveScore>1</CleanMoveScore>
	<TrailHitScore>20</TrailHitScore>
	<LastManStandScore>100</LastManStandScore>
</GameSettings>

The file will tell the game that we want a random map in dimensions 50x20 with 10% of map filled with obstacles.
All the rest of parameters are at their defaults.

To finally run the game we create bat file like .\run.bat as follows:

	.\bin\LightCycles.exe ".\Players\SuperBot_vs_RandomBots.xml" ".\Maps\myRandomMap.xml" ".\TeamsAndPlayers.xml" 

This will run the game and feed files created by us.
After game completes we can view all the replays in Logs folder by executing RunLogViewer.bat


How to run a game with map from image file?
===========================================
You need to adjust your game settings file like follows:

	<MapSource>File</MapSource> <!-- tells it that source of map is a file -->
	<MapName>Infusion</MapName> <!-- put some cool name for your map -->
	<MapFileName>.\Maps\FreeForAll\my_map.png</MapFileName> <!-- path to you map file -->

It is advised to put you custom maps into appropriate .\Maps\FreeForAll\ or .\Maps\TeamDeathmatch\ for clarity.


How to make a custom map file?
==============================
You can draw it in any image processing program, even in windows paint.
Remember few rules:
- color black (0,0,0) is an obstacle
- color white (255,255,255) is a free space players can travel trough
- in FFA mode players starting position are marked by red (255,0,0) color. Typically you'll need to draw 8 red pixels.
- in Team game you need to use red (255,0,0) color for starting positions of team A and green (0,255,0) for starting positions of team B. If you'd like to have more than 2 teams here is color table for your reference:
	A (255, 0, 0)
        B (0, 255, 0)
        C (0, 0, 255)
        D (255, 255, 0)
        E (255, 0, 255)
        F (0, 255, 255)
        G (255, 127, 0)
        H (255, 0, 127)

Max 8 team is allowed in game.
Save your custom map in an appropriate place in .\Maps folder and wire a game map just as in previous question.
Note: map max dimensions are 1000x1000 (bigger map requires change in code, refer Constraints class)




