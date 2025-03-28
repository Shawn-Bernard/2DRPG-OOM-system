using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PathNode
{
    // H Cost is the distance from end node
    // F Cost is the distance from starting node
    // G Cost is H Cost + F Cost
    public int HCost, FCost, GCost;
    public Point position, exploreFrom;
    public bool calculated, isWalkable, hasClosed = false; 

    public int X => position.X;
    public int Y => position.Y;

    public PathNode() 
    {
        Reset(); 
    }

    public void Reset() 
    {
        HCost = -1;
        FCost = -1;
        GCost = -1;
        calculated = false;
        isWalkable = false;
        hasClosed = false;
        position = Point.Zero; 
        exploreFrom = Point.Zero;
    }

}

