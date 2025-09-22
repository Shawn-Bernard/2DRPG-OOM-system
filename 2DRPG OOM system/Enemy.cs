using _2DRPG_OOM_system;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


public class Enemy : Actor
{       

    public string eMovement;

    public enum EnemyType
    {
        Mage,
        Ghost,
        Boss,
    }

    public EnemyType enemyType = EnemyType.Mage;

    public string TypeOfMovement() 
    {
        // This is the AI for the movement for the enemies which is at random
        string _typeOfMovement = "";
        Random random = new Random();
        int accN = random.Next(0, 4);

        switch (accN) 
        {
            case 0:                
                _typeOfMovement = "Right";
                moveDir = new Vector2(1, 0);
                break;
            case 1:
                _typeOfMovement = "Left";
                moveDir = new Vector2(-1, 0);
                break;
            case 2:
                _typeOfMovement = "Down";
                moveDir = new Vector2(0, 1);
                break;
            case 3:
                _typeOfMovement = "Up";
                moveDir = new Vector2(0, -1); 
                break;
            default:
                //Nothing Happens
                break;                 
        }

        return _typeOfMovement;

    }
             

    public override void DrawStats(SpriteBatch _spriteBatch, int num, int posY)
    {
        _spriteBatch.DrawString(Game1.mySpriteFont, "Enemy " + num + ": ", new Vector2(700, posY), Color.White);

        if(ismyTurn)
            _spriteBatch.DrawString(Game1.mySpriteFont, "Turn", new Vector2(700, posY), Color.White);
        else
            _spriteBatch.DrawString(Game1.mySpriteFont, "", new Vector2(700, posY), Color.White);


        _spriteBatch.DrawString(Game1.mySpriteFont, "HP: " + _healthSystem.health, new Vector2(700, posY + 20), Color.White);

        _spriteBatch.DrawString(Game1.mySpriteFont, feedback, new Vector2(tilemap_PosX * Game1.tileSize * 2 - 5, ((tilemap_PosY + 5) * Game1.tileSize * 2) - 25), Color.White);
    }


}

