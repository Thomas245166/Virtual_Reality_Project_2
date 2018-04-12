# Virtual Reality Project 2
Team: Thomas Sanchez, Megan Taylor, Andrew Im, Cody Otterbine
# [Video](https://www.youtube.com/watch?v=dRlu8d9nerQ&feature=youtu.be)
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
#### [Port Upgrade](https://poly.google.com/view/bzRjbJ74JCr)
![alt text](https://github.com/Thomas245166/Virtual_Reality_Project_2/blob/master/Mock%20Ups/Assets%20Picture/Port.png)
* Port Upgrade cost 200 Wood and 200 Minerals
* Plus 20 water per gather
### Land

#### [Sheep](https://poly.google.com/view/aWFQcDSaDyo)
![alt text](Mock%20Ups/Assets%20Picture/Sheep.png)
* Gather 10 food
##### [Farm Upgrade](https://poly.google.com/view/dSsUaUlaxHk)
![alt text](Mock%20Ups/Assets%20Picture/Farm.png)
* Farm Upgrade cost 200 wood and 200 minerals
* Plus 20 food per gather

#### [Minerals](https://poly.google.com/view/0Fl55ZzsVzT)
![alt text](Mock%20Ups/Assets%20Picture/Mountain.png)
* Gather 10 minerals 
##### [Factory Upgrade](https://poly.google.com/view/fCiJW5Qdgbf)
![alt text](Mock%20Ups/Assets%20Picture/Factory.png)
* Farm Upgrade cost 200 wood and 200 mineals
* Plus 20 minerals per gather

#### [Forest](https://poly.google.com/view/2_fv3tn3NG_)
![alt text](Mock%20Ups/Assets%20Picture/Forest.png)
* Gather 10 Wood
##### [Logging](https://poly.google.com/view/dTSrDa0oz0a) [Mill Upgrade](https://poly.google.com/view/ctIRaIM3zXu)
![alt text](Mock%20Ups/Assets%20Picture/Log%20Mill.png)
* Logging Mill Upgrade cost 200 wood and 200 minerals
* plus 20 to wood per gather

## Blank Tiles
* Blank tiles can converted into any of the

## Upgrades
#### [Cites](https://poly.google.com/view/6sBXNsb9CFH)
![alt text](Mock%20Ups/Assets%20Picture/City.png)
* increases Population, necessary for win
* Cost 500 wood and 500 minerals
#### [Wind Turbine](https://poly.google.com/view/8Tke6WIyZtg)
![alt text](Mock%20Ups/Assets%20Picture/WindTurbine.png)
* Energy Producer, plus 50 Energy per turn
* Cost 400 minerals and 400 wood
* Makes all near by tiles clean
##### Nuclear Upgrade
![alt text](Mock%20Ups/Assets%20Picture/Nuclear%20Plant.jpg)
* Energy Producer, plus 75 Energy per turn
* Cost 500 minerals and 500 wood
* Makes all near by tiles polluted 

# Mock Ups
![alt text](Mock%20Ups/Cody%20mock%20up.JPG)
## Original Top Down Design
Created by Cody to demonstrate the general map design and the perspective of the player character to the island, and resources

![alt text](Mock%20Ups/Mock%20Up.png)
## Blendr Mock Up
Created by Andrew in Blendr based on Cody's Design. We adopted a grid tile pattern to demonstrate the limited space and resources.

## Final Implementation

![alt text](Mock%20Ups/menu%20vr.JPG)

![alt text](Mock%20Ups/how%20to%20play%20vr.JPG)

![alt text](Mock%20Ups/Game%20View.png)

![alt text](Mock%20Ups/SceneView.png)
