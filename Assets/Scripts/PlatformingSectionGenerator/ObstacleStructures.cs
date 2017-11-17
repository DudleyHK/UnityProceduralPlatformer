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

    public abstract class Obstacle
    {
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


    public class FloorJumpFloor : Obstacle
    {
        public FloorJumpFloor()
        {
            this.Name          = "FJF";
            this.Difficulty    = 1;
            this.CostOfFailure = 1;
        }
    }
}
