using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

  public class Tilemap
  {
    static private int MapSizeX = 100;
   static public int mapSizeX 
    {
        get { return MapSizeX; }
    }
    static private int MapSizeY = 100; 

    static public int mapSizeY 
    {
        get { return MapSizeY; }
    }

    string sJoined;

    public int verticalIndex;
    public int horizontalIndex; 

    public string[,] multidimensionalMap = new string[mapSizeX, mapSizeY];

    public char GenerateChar()
    {
        // Generate the char at random
        // '#' for walls, '@' for doors, '*' for field '%' for grass, '$' for grass2, '&' for a tree, 'k' for keys
        char charElement;
        int typeOfString = randomNumber(0, 100);

        if (typeOfString < 35)
        {
            charElement = '*';
        }
        else if (typeOfString < 55)
        {
            charElement = '%';
        }
        else if (typeOfString < 75)
        {
            charElement = '$';
        }       
        else if (typeOfString < 86)
        {
            charElement = '#';
        }        
        else
        {
            charElement = '*';
        }


        return charElement;
    }


    public static int randomNumber(int a, int b)
    {
        // Generate a random number to later get a random character
        System.Random random = new System.Random();
        int rslt = random.Next(a, b);
        return rslt;
    }


    public void getTheIndexes(char tile) 
    {
        switch (tile) 
        {
            case '*':
                verticalIndex = 0;
                horizontalIndex = 0;
                break;
            case '%':
                verticalIndex = 1;
                horizontalIndex = 4;
                break;
            case '$':
                verticalIndex = 7;
                horizontalIndex = 1;
                break;
            case '#':
                verticalIndex = 4;
                horizontalIndex = 3;
                break;
            default:
                verticalIndex = 0;
                horizontalIndex = 0;
                break;
                                  
                
        }
    }

}

