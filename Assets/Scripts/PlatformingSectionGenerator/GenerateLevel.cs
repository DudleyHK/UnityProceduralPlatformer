using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Obstacle = ObstacleStructures.Obstacle;


public class GenerateLevel : ScriptableObject
{
    private List<Obstacle> obstacleList;
    // Use the Parse Tree structure to determine how the level is built.
    // Group different sets of ObstacleStructures to create different challenges/


    /// <summary>
    /// Generate a series of Obstacles which make up the level.
    /// </summary>
    /// <returns></returns>
    public List<Obstacle> RunObstacleGenerator(ushort totalObstacles)
    {
        obstacleList = new List<Obstacle>();
        for (int i = 0; i < totalObstacles; i++)
        {
            if(i == 0)
            {
                obstacleList.Add(new Obstacle("FFFF", 1, 1));
                continue;
            }
            else if(i == (totalObstacles - 1))
            {
                obstacleList.Add(new Obstacle("FJJF", 1, 1));
            }
            else if (i == (totalObstacles / 2f))
            {
                obstacleList.Add(new Obstacle("FJJJ", 1, 1));
            }
            else if (i == (totalObstacles / 2f) + 1)
            {
                obstacleList.Add(new Obstacle("FJJJ", 1, 1));
            }
            else
            {
                obstacleList.Add(new Obstacle("FjjF", 1, 1));
            }
        }
        return obstacleList;
    }
}
