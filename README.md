# Virtual Reality Project 2
Team: Thomas Sanchez, Megan Taylor, Andrew Im, Cody Otterbine

# Eco Island
Eco island is a turn based resource management game. The player has a third person view over a small island with a certain manageable number of resources where every turn certain consequences happen that pollute the environment and reduces the resources. You may also click and gather resources from the map. The map will either remove the model from the map if tile is polluted or pollute the tile if it is  stable.   

# Turns
+ Actions performed based on energy
+ Each action uses energy
+ Cleaning a tile which switches from polluted to clean - costs 10 Energy
+ Upgrades to tiles - costs 50 Energy
+ Building a city - costs 100 Energy
+ Max energy increases with the creation of wind turbines or nuclear power plants
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
![alt text](https://github.com/Thomas245166/Virtual_Reality_Project_2/blob/master/Mock%20Ups/Assets%20Picture/City.png)
* Port Upgrade cost 200 Wood and 200 Minerals
* Plus 20 water per gather
### Land

#### Sheep
![alt text](https://github.com/Thomas245166/Virtual_Reality_Project_2/blob/master/Mock%20Ups/Assets%20Picture/Sheep.png)
* Gather 10 food
##### Farm Upgrade
![alt text](https://github.com/Thomas245166/Virtual_Reality_Project_2/blob/master/Mock%20Ups/Assets%20Picture/Farm.png)
* Farm Upgrade cost 200 wood and 200 minerals
* Plus 20 food per gather

#### Minerals
![alt text](https://github.com/Thomas245166/Virtual_Reality_Project_2/blob/master/Mock%20Ups/Assets%20Picture/Mountain.png)
* Gather 10 minerals 
##### Factory Upgrade
![alt text](https://github.com/Thomas245166/Virtual_Reality_Project_2/blob/master/Mock%20Ups/Assets%20Picture/Factory.png)
* Farm Upgrade cost 200 wood and 200 mineals
* Plus 20 minerals per gather

#### Forest
![alt text](https://github.com/Thomas245166/Virtual_Reality_Project_2/blob/master/Mock%20Ups/Assets%20Picture/Forest.png)
* Gather 10 Wood
##### Logging Mill Upgrade
![alt text](https://github.com/Thomas245166/Virtual_Reality_Project_2/blob/master/Mock%20Ups/Assets%20Picture/Log%20Mill.png)
* Logging Mill Upgrade cost 200 wood and 200 minerals
* plus 20 to wood per gather

## Blank Tiles
* Blank tiles can converted into any of the

## Upgrades
#### Cites
![alt text](https://github.com/Thomas245166/Virtual_Reality_Project_2/blob/master/Mock%20Ups/Assets%20Picture/City.png)
* increases Population, necessary for win
* Cost 500 wood and 500 minerals
#### Windmill
![alt text](https://github.com/Thomas245166/Virtual_Reality_Project_2/blob/master/Mock%20Ups/Assets%20Picture/WindTurbine.png)
* Energy Producer, plus 50 Energy per turn
* Cost 400 minerals and 400 wood
* Makes all near by tiles clean
##### Nuclear Upgrade
![alt text](https://github.com/Thomas245166/Virtual_Reality_Project_2/blob/master/Mock%20Ups/Assets%20Picture/Nuclear%20Plant.jpg)
* Energy Producer, plus 75 Energy per turn
* Cost 500 minerals and 500 wood
* Makes all near by tiles polluted 

# Current Mock Ups
![alt text](https://github.com/Thomas245166/Virtual_Reality_Project_2/blob/master/Mock%20Ups/Cody%20mock%20up.JPG)
## Original Top Down Design
Created by Cody to demonstrate the general map design and the perspective of the player character to the island, and resources

![alt text](https://github.com/Thomas245166/Virtual_Reality_Project_2/blob/master/Mock%20Ups/Mock%20Up.png)
## Blendr Mock Up
Created by Andrew in Blendr based on Cody's Design, Adpoted a grid tile pattern to demonstrate the limited space and resources

## Final implementation
![alt text](https://github.com/Thomas245166/Virtual_Reality_Project_2/blob/master/Mock%20Ups/Assets%20Picture/Scene.JPG)

![alt text](https://github.com/Thomas245166/Virtual_Reality_Project_2/blob/master/Mock%20Ups/Assets%20Picture/Game.JPG)
