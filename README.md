##
In this project I made a class Actor which is going to have all the variables and method that the player and the enemy have. Both player and enemies are going to inheritate the Actor class
The difference between the player and the enemy is the enemy has it own AI method for movement. 
The generation of the tilemap is in a specific script, and it create it own class that is called tilemap
A create a health system class which the all actors are going to have it, in there, I coded everything that has to do with stats and calculation.
When the the player hits the enemy, leave it in a stun status which make the enemy not move for 1 turn.


You move with ASWD

For the third sprint, I create an inventory which the player is going to have one. I created four types of items: Healing Potion, Fire Rod (Scroll of Fireball), Lightning Rod (Scroll of Lightning) and a Sacred Potion.
Healing Potion will heal  5 hp to the player.
Fire Rod make the player enter into aiming mode. The player will be able to select a direction and create a fire ball that follows that direction. Uon hit an enemy, it will inflict 5 hp damage.
Lightning Rod attack all the enemies on the map inflicting 3 hp damage to each one
The sacred Potion will fully recover the player hp (max hp) and it will be inmune to all attack during that turn
After using an item, the player turn is done.
The inventory has 5 slots, to use an item you have to press the correponsing number of the item slot. (e.g If the potion is in the third slot and I want to use it, I should press '3')

For the enemies, I created a ghost who can walk through wall and non-walkable tiles. The player won't be able to attack the ghost by the normal method. To inflict damage if using a fire rod or lightning rod.
I also created a Dark Mage who if he is in the same X position or Y postion as the player, will shoot a misma ball. The miasma ball will inflict 4 hp damage to the player upon hit

To go to the next level, you have to beat all the enemies that are not ghost and move to the door. 

FOR THE FOUTH SPRINT

I created seven scenes: Main Menu, level 1, level 2, level 3, boss level, Game End (Win) and Game Over (Lose).
The main menu has two buttons: Start Game and Exit
Game End has two buttons: Play again (Restart the game) and go to menu
Game Over has two buttons: Try Again (Restart the game) and go to menu

The boss is a cyclops monster who has 20 hp. The boss will take one of four actions at random: Do nothing, move normally, shoot a venom breath and move three tiles (moves three times)
The first level, the map is premade and it always going to be the same. The enemies placement are set. 
In the second level and forward, the map and the enemies will be placed at random.
The items are always going to be placed at random

##
