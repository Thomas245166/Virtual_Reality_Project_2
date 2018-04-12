# Virtual Reality Project 2
Team: Thomas Sanchez, Megan Taylor, Andrew Im, Cody Otterbine

# Project Assignment
The watershed development and management game is envisioned as a "multiplayer" game modeled after the classic Monopoly and SimCity. Students are provided a fixed amount of coins and a distinct parcel of land (which is assigned randomly) on which they can either build or expand urban areas, grow crops or engage in industrial activities. All these activities release pollutants (biochemical oxygen demand (BOD) and nutrients) into streams and creeks. The release of these pollutants will affect the in-stream health which is measured by decreases in dissolved oxygen (DO) and increases in algae. Critical thresholds of DO and algae (chlorophyll-a) will be used to simulate catastrophic events such as fish kills and loss of riparian habitats. A set of compliance points (CP) are set up on the stream segment to monitor water quality in the stream. 

# Virtual Insanity Game
Third Person View over a small island with a certain manageable number of resources where every turn certain consequences happen that pollute the environment and reduces the resources. You may also click and gather resource from the map, the map will either remove the model from the map if tile is polluted or pollute the tile if stable.   

# Turns
+ Actions performed based on energy
+ Each action uses energy
+ Cleaning a tile which switches from polluted to clean cost 10
+ Upgrades to tiles cost 50 energy
+ Building a city cost 100 energy
+ Max energy increases with the creation of wind turbines or nuclear
+ At end of the turn, the board will update using Conway's Game of Life rules 

# Win Condition
Develope three cities on the map

# Lose Condition
Have your people count drop to zero or below

# Resources: Certain food to meet per people, 
## People
+ If food > 0 for 3 consective turns, plus 3 to people
+ If food < 0 for 3 consective turns, minus 3 to people

## Food
+ Gather food from sheep, farms, or docks
+ 3 food required per person

## Minerals
+ Gather Minerals from mountains or factories

## Wood
+ Gather Wood from wood or logging mills

# Different Tile Types
## Main City (one in game, random)
* Cost 500 minerals, 500 wood
* Plus 10 to Population

## Resource Tiles

### Water
* Gather 10 water
#### Port Upgrade
* Port Upgrade cost 200 Wood and 200 Minerals
* Plus 20 water per gather
### Land

#### Sheep
* Gather 10 food
##### Farm Upgrade
* Farm Upgrade cost 200 wood and 200 minerals
* plus 20 food per gather

#### Minerals
* Gather 10 minerals 
##### Factory Upgrade
* Farm Upgrade cost 200 wood and 200 mineals
* plus 20 minerals per gather

#### Forest
* Gather 10 Wood
##### Logging Mill Upgrade
* Logging Mill Upgrade cost 200 wood and 200 minerals
* plus 20 to wood per gather

## Blank Tiles
* Blank tiles can converted into any of the
### Upgrades
#### Cites
* increases Population, necessary for win
* Cost 500 wood and 500 minerals
#### Windmill
* Energy Producer, plus 50 Energy per turn
* Cost 400 minerals and 400 wood
* makes all near by tiles clean
##### Nuclear Upgrade
* Energy Producer, plus 75 Energy per turn
* Cost 500 minerals and 500 wood
* makes all near by tiles polluted 

# Current Mock Ups
![alt text](https://github.com/Thomas245166/Virtual_Reality_Project_2/blob/master/Mock%20Ups/Cody%20mock%20up.JPG "Cody Mock Up")
## Original Top Down Design
Created by Cody to demonstrate the general map design and the perspective of the player character to the island, and resources

![alt text](https://github.com/Thomas245166/Virtual_Reality_Project_2/blob/master/Mock%20Ups/Mock%20Up.png "Blendr Mock Up")
## Blendr Mock Up
Created by Andrew in Blendr based on Cody's Design, Adpoted a grid tile pattern to demonstrate the limited space and resources
