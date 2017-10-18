﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public List<GameObject> levelPlatforms = new List<GameObject>();
    public GameObject platformPrefab;
    public GameObject playerPrefab;
    public int lastPlatformIdx = 0;
    public ushort levelLength = 5;
    public string startingPlatform = "Middle";

    private GameObject playerClone;




    private void Start()
    {
        Regenerate();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Regenerate();
        }
    }


    private void Regenerate()
    {
        foreach (var platform in levelPlatforms)
        {
            Destroy(platform);
        }
        levelPlatforms.Clear();
        Destroy(playerClone);

        GenerateLevel();
        AddPlayer();
    }



    private void GenerateLevel()
    {
        for(int i = 0; i < levelLength; i++)
        {
            lastPlatformIdx = levelPlatforms.Count - 1;

            // Place the starting platform.
            if (i == 0)
            {
                AddPlatform(startingPlatform);
                continue;
            }


            // Get a list of the possible next moves
            string lastPlatformType = levelPlatforms[lastPlatformIdx].GetComponent<Platform>().GetPlatformType();
            string[] nextPlatformOptions = Rules.NextPlatform(lastPlatformType);

            //Debug.Log("Display options based on last type (" + lastPlatformType + ")");
            //foreach (var option in nextPlatformOptions)
            // {
            //     Debug.Log(option);
            // }

            int randomPick = Random.Range(0, nextPlatformOptions.Length);

            //Debug.Log("Random number min (" + 0 + "), max  (" + nextPlatformOptions.Length + ")");
            //Debug.Log("Random choice: " + randomPick);


            AddPlatform(nextPlatformOptions[randomPick]);
        }
    }


    private void AddPlatform(string type)
    {
        GameObject newPlatform;
        GameObject lastPlatform = default(GameObject);
        Vector3    newPosition;


        // Create the new platform
        newPlatform = Instantiate(platformPrefab);

        // Assign Platform correct type
        switch (type)
        {
            case "Top":
                newPlatform.AddComponent<TopPlatform>();
                break;

            case "Middle":
                newPlatform.AddComponent<MiddlePlatform>();
                break;

            case "Bottom":
                newPlatform.AddComponent<BottomPlatform>();
                break;
        }


        // If this is the first platform place it at the beginning. 
        if (levelPlatforms.Count <= 0)
        {
            Debug.Log("Setting position values for the first platform");
            newPosition.x = 0f;
            newPosition.y = 0f;
            newPosition.z = 0f;
        }
        else
        {
            lastPlatform = levelPlatforms[lastPlatformIdx];

            Debug.Log("Setting position values for platform at index " + (lastPlatformIdx + 1));
            newPosition.x = lastPlatform.transform.position.x + lastPlatform.transform.lossyScale.x + PlayerMetrics.staticJumpDistance;
            newPosition.y = newPlatform.GetComponent<Platform>().GetHeight(lastPlatform);
            newPosition.z = lastPlatform.transform.position.z;
        }
        
       
        newPlatform.transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
        levelPlatforms.Add(newPlatform);

        //Debug.Log("Full vec position for new " + newPlatform.GetComponent<Platform>().GetPlatformType() + " platform "  + newPosition);
        //Debug.Log(type + " platform has been added to the list");
    }


    private void AddPlayer()
    {
        float yOffset = levelPlatforms[0].transform.lossyScale.y + (playerPrefab.transform.lossyScale.y / 2);

        float x = levelPlatforms[0].transform.position.x;
        float y = levelPlatforms[0].transform.position.y + yOffset;
        float z = levelPlatforms[2].transform.position.z;


        playerClone = Instantiate(playerPrefab, new Vector3(x, y, z), Quaternion.identity);
    }

}
