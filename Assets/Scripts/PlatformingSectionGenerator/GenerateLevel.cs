using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Obstacle = ObstacleStructures.Obstacle;


public class GenerateLevel : ScriptableObject
{

    // Use the Parse Tree structure to determine how the level is built.


    /// <summary>
    /// Generate a series of Obstacles which make up the level.
    /// </summary>
    /// <returns></returns>
    public List<Obstacle> RunObstacleGenerator(ushort totalObstacles)
    {

        // Currently use basic level.
        return new List<Obstacle>(new Obstacle[]
        {
             new ObstacleStructures.FloorJumpFloor(),
             new ObstacleStructures.FloorJumpFloor(),
             new ObstacleStructures.FloorJumpFloor()
        });
    }
}
