﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Obstacle = ObstacleStructures.Obstacle;


public class WorldManager : MonoBehaviour 
{
    public ushort totalObstacles = 5;
    public List<Obstacle> obstacleStructures;
    public List<GameObject> obstacleParents;

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
        ushort count = 0;

        ResetScene();
       
        // Procedurally generate series of obsticles.
        obstacleStructures = generateLevel.RunObstacleGenerator(totalObstacles);

        // buidl the parent objects to the scene
        // obstacleParents = buildToScene.BuildParents(totalObstacles);
        while(count <= totalObstacles)
        {
            // read data for single obstacle
            // build the obstacle into a gameobject
            // build the obstacle to the scene

            count++;
            yield return false;
        }
        yield return true;
    }

    private void ResetScene()
    {
        // Destory all world objects.
        
        // Clear list.
        obstacleStructures.Clear();
    }

}
