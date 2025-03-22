##
In this project I made a class Actor which is going to have all the variables and method that the player and the enemy have. Both player and enemies are going to inheritate the Actor class
The difference between the player and the enemy is the enemy has it own AI method for movement. I thought of giving the player a score variable but in the end I didn't use it, so I eliminated it in the script
The generation of the tilemap is in a specific script, and it create it own class that is called tilemap
A create a health system class which the all actors are going to have it, in there, I coded everything that has to do with stats and calculation.
When the the player hits the enemy, leave it in a stun status which make the enemy not move for 1 turn.
The same happens but in reverse in case is the enemy who hits the player.
##
