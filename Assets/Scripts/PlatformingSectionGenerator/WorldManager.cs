using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Obstacle = ObstacleStructures.Obstacle;


public class WorldManager : MonoBehaviour 
{
    public ushort totalObstacles = 5;
    public List<Obstacle>   obstacleStructures;
    public List<Vector2> obstacleParentPositions;
    public List<List<GameObject>> obstacleObjects = new List<List<GameObject>>();
    public BuildToScene buildToScene;

    private GenerateLevel   generateLevel;
    private DataReader      dataReader;



    private void Start()
    {
        generateLevel   = ScriptableObject.CreateInstance("GenerateLevel")   as GenerateLevel;
        dataReader      = ScriptableObject.CreateInstance("DataReader")      as DataReader;

        StartCoroutine(Regenerate());
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            StopAllCoroutines();
            StartCoroutine(Regenerate());
        }
    }



    private IEnumerator Regenerate()
    {
        int count = 0;

        ResetScene();
       
        // Procedurally generate series of obsticles.
        obstacleStructures = generateLevel.RunObstacleGenerator((ushort)(totalObstacles - 1));

        // Build the parent objects to the scene
        obstacleParentPositions = buildToScene.BuildParents(totalObstacles);
        while(count < (totalObstacles - 1))
        {
            var obstacle = obstacleStructures[count];

            // read data for single obstacle
            var obstacleData = dataReader.RunDataReader(obstacle);
            if(obstacleData == null)
            {
                Debug.Log("ERROR: Obstacle Data is null");
                break;
            }

            // build the obstacle into the scene
            var obsticleObj  = buildToScene.BuildObstacle(obstacleData, obstacleParentPositions[count], dataReader.dataLength);
            if (obsticleObj == null) Debug.Log("ERROR: Obstacle Object has return null");

            obstacleObjects.Add(obsticleObj);
            count++;
            yield return false;
        }
        yield return true;
    }

    private void ResetScene()
    {
        // Destory all world objects.
        foreach (var list in obstacleObjects)
        {
            foreach (var obj in list)
            {
                Destroy(obj);
            }
        }


        if(obstacleParentPositions != null && obstacleParentPositions.Count >= 0)
        {
            obstacleParentPositions.Clear();
        }
        if (obstacleObjects != null && obstacleObjects.Count >= 0)
        {
            obstacleObjects.Clear();
        }
        if(obstacleStructures != null && obstacleStructures.Count >= 0)
        {
            obstacleStructures.Clear();
        }
    }

}
