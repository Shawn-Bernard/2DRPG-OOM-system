using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

  public class Tilemap
  {
    static private int MapSizeX = 25;
   static public int mapSizeX 
    {
        get { return MapSizeX; }
    }
    static private int MapSizeY = 10; 

    static public int mapSizeY 
    {
        get { return MapSizeY; }
    }

    string sJoined;

    public int verticalIndex;
    public int horizontalIndex; 

    public char[,] multidimensionalMap = new char[mapSizeX, mapSizeY];

    

    public string GenerateMapString(int width, int height) 
    {
        char[,] mapMatrix = new char[width, height];

        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if (i == 0 || j == 0 || i == width - 1 || j == height - 1)
                    mapMatrix[i, j] = '#';  //1st rule: The borders should be walls
                else if ((i == 3 && j == 3) || (i == 22 && j == 5))
                    mapMatrix[i, j] = '*';
                else
                    mapMatrix[i, j] = GenerateChar();
            }
        }

        return convertMapToString(width, height, mapMatrix); 
    }


    public char GenerateChar()
    {
        // Generate the char at random
        // '*' for normal field, '%' for a second type of field, '$' for blocks, '#' for walls
        char charElement;
        int typeOfString = randomNumber(0, 100);

        if (typeOfString < 45)
        {
            charElement = '*';
        }
        else if (typeOfString < 90)
        {
            charElement = '%';
        }
        else if (typeOfString < 95)
        {
            charElement = '$';
        }       
        else if (typeOfString < 100)
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

    public string convertMapToString(int x, int y, char[,] smap)
    {
        string result = "";

        for (int j = 0; j < y; j++)
        {
            for (int i = 0; i < x; i++)
            {
                result += smap[i, j];
            }
            result += Environment.NewLine;
        }

        return result;
    }

    public void ConvertToMap(string sMap, char[,] daMap)
    {
        var lines = sMap.Split('\n');

        for (int j = 0; j < lines.Length; j++)
        {

            for (int i = 0; i < lines[j].Length - 1; i++)
            {
                if (lines[j][i] == '#') // wall
                {
                    daMap[i, j] = '#';
                }
                else if (lines[j][i] == '*')  // field
                {
                    daMap[i, j] = '*';
                }
                else if (lines[j][i] == '%')  //Field2
                {
                    daMap[i, j] = '%';
                }
                else if (lines[j][i] == '$') //Block
                {
                    daMap[i, j] = '$';
                }
                else
                {
                    daMap[i, j] = '*';
                }
            }
        }
    }
        
    public char MapToChar(char[,] cMap, int i, int j) 
    {
        return cMap[i, j];
    }


    public void getTheIndexes(char tile) 
    {
        switch (tile) 
        {
            case '*':
                horizontalIndex = 0;
                verticalIndex = 4;
                break;
            case '%':
                horizontalIndex = 1;
                verticalIndex = 4;
                break;
            case '$':
                horizontalIndex = 7;
                verticalIndex = 1;
                break;
            case '#':
                horizontalIndex = 4;
                verticalIndex = 3;
                break;
            default:
                horizontalIndex = 0;
                verticalIndex = 4;
                break;
                                  
                
        }
    }

    

}

