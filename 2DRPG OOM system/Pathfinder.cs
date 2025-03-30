using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Pathfinder
{
    public PathNode[,] nodeMap;
    public List<PathNode> unexploredNodes = new List<PathNode>();
    public List<PathNode> exploredNodes = new List<PathNode>();
    public List<PathNode> BestPath = new List<PathNode>(); 
    public Point startingPoint, goalPoint;
    public PathNode lastPathNode;
    public PathNode startPathNode;
    public PathNode bestNode; 

    public void InitializePathfinding(char[,] map, Point startingFrom, Point goal)
    {
        startingPoint = startingFrom;
        goalPoint = goal;

        if (nodeMap == null || nodeMap.GetLength(0) != map.GetLength(0) || nodeMap.GetLength(1) != map.GetLength(1))
        {
            // initializing my node map with the same size as my map
            nodeMap = new PathNode[map.GetLength(0), map.GetLength(1)];
        }
        else
        {
            for (int x = 0; x < nodeMap.GetLength(0); x++)
                for (int y = 0; y < nodeMap.GetLength(1); y++)
                    nodeMap[x, y].Reset();
        }

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (nodeMap[x, y] == null) nodeMap[x, y] = new PathNode();
                if (map[x, y] == '$' || map[x, y] == '#')
                     nodeMap[x, y].isWalkable = false;                
                else
                    nodeMap[x, y].isWalkable = true; 

                nodeMap[x, y].position = new Point(x, y);
            }
        }

        startPathNode = nodeMap[startingFrom.X, startingFrom.Y];
        unexploredNodes.Add(nodeMap[startingFrom.X, startingFrom.Y]); 
        lastPathNode = nodeMap[goal.X, goal.Y];

    }


    public void ExploringNode() 
    {
        if (unexploredNodes.Count == 0) 
        {
            // No path found! need to handle this later
            Debug.Fail("Failed to find a path, unexplored nodes is empty");
            return;
        }

        int bestFCost = int.MaxValue;
        int bestNodeIndex = -1;

        for (int i = 0; i < unexploredNodes.Count; i++) {
            if(unexploredNodes[i].FCost < bestFCost)
            {
                
                bestNodeIndex = i;
                bestFCost = unexploredNodes[i].FCost;
            }
        }

        PathNode exploringNode = unexploredNodes[bestNodeIndex];        
        unexploredNodes.RemoveAt(bestNodeIndex);

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (y != 0 && x != 0)
                    continue;              


                    if (exploringNode.X + x < nodeMap.GetLength(0) && exploringNode.Y + y < nodeMap.GetLength(1))
                    {
                        PathNode checkingNode = nodeMap[exploringNode.X + x, exploringNode.Y + y];
                        checkingNode.GCost = (Math.Abs(startingPoint.X - checkingNode.X) + Math.Abs(startingPoint.Y - checkingNode.Y)) * 10;
                        checkingNode.HCost = (Math.Abs(goalPoint.X - checkingNode.X) + Math.Abs(goalPoint.Y - checkingNode.Y)) * 10;
                        checkingNode.FCost = checkingNode.GCost + checkingNode.HCost;

                        checkingNode.calculated = true;


                        if (y == 0 && x == 0)
                        {
                            //Closing this one
                            checkingNode.hasClosed = true;
                            if (!exploredNodes.Contains(checkingNode))
                            {
                                exploredNodes.Add(checkingNode);
                            }
                            else
                            {
                                unexploredNodes.RemoveAt(bestNodeIndex);
                            }
                        }
                        else
                        {
                            // this is a neighbour
                            if (!checkingNode.hasClosed && !unexploredNodes.Contains(checkingNode) && checkingNode.isWalkable)
                            {
                                unexploredNodes.Add(checkingNode);
                                BestPath.Add(checkingNode);
                            }
                        }
                    }
                
            }
        }

        exploredNodes.Add( getTheBestPath(unexploredNodes)); 


    }
    

    public PathNode getTheBestPath(List<PathNode> nodes)
    {
        int bestF = nodes[0].FCost;
        int _index = 0;
        for(int i = 1; i < nodes.Count; i++) 
        {
            if (nodes[i].FCost < bestF)
            {
                bestF = nodes[i].FCost;
                _index = i;
            }


        }

        return nodes[_index]; 
    }
}

