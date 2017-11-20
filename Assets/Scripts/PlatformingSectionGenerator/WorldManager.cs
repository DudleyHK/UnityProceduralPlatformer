using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Obstacle = ObstacleStructures.Obstacle;


public class WorldManager : MonoBehaviour 
{
    public ushort totalObstacles = 5;
    public List<Obstacle> obstacleStructures;
    public List<GameObject> obstacleParents;
    public List<List<char>> characterLists = new List<List<char>>();

    private GenerateLevel   generateLevel;
    private DataReader      dataReader;
    private BuildToScene    buildToScene;
    private ObstacleBuilder obstacleBuilder;



    private void Start()
    {
        generateLevel = new GenerateLevel();
        dataReader = new DataReader();
        obstacleBuilder = new ObstacleBuilder();
        buildToScene = new BuildToScene();

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

        ResetScene();
       
        // Procedurally generate series of obsticles.
        obstacleStructures = generateLevel.RunObstacleGenerator(totalObstacles);

        // buidl the parent objects to the scene
        // obstacleParents = buildToScene.BuildParents(totalObstacles);
        for(ushort i = 0; i <= totalObstacles; i++)
        {
            // read data for single obstacle
            var obstacleData = dataReader.RunDataReader(obstacleStructures[i]);

            // build the obstacle into a gameobject
            obstacleBuilder.BuildObstacle(obstacleData);

            // build the obstacle to the scene

            // add the obstacle to the obstacle list.

            yield return false;
        }
        yield return true;
    }

    private void ResetScene()
    {
        // Destory all world objects.
        
        // Clear list.
        if(obstacleStructures != null && obstacleStructures.Count >= 0)
        {
            obstacleStructures.Clear();
        }
    }

}
