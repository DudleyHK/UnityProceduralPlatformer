using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleStructures
{
    // Structure
    // Difficulty
    // Cost of failure

    // 1 - easy   - low
    // 2 - medium - medium
    // 3 - hard   - high




   // Tile types:
   /*
        F - Floor
        G - Ground
        J - High COF Jump
        j - Low COF Jump
        W - High Wall
        w - Low wall
        D - Danger
        
        
   */

    public class Obstacle
    {
        public Obstacle(string name, ushort difficulty, ushort costOfFailure)
        {
            Name          = name;
            Difficulty    = difficulty;
            CostOfFailure = costOfFailure;
        }

        private string name = "";
        public string Name
        {
            get
            {
                return name;
            }
            protected set
            {
                name = value;
            }
        }

        private ushort difficulty = 0;
        public ushort Difficulty
        {
            get
            {
                return difficulty;
            }
            protected set
            {
                difficulty = value;
            }
        }

        private ushort costOfFailure = 0;
        public ushort CostOfFailure
        {
            get
            {
                return costOfFailure;
            }
            protected set
            {
                costOfFailure = value;
            }
        }
    }
}
