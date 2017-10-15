﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public List<GameObject> levelPlatforms = new List<GameObject>();
    public GameObject platformPrefab;
    public int lastPlatformIdx = 0;
    public ushort levelLength = 5;
    public string startingPlatform = "Middle";




    private void Start()
    {
        GenerateLevel();
        AddPlayer();
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
        GenerateLevel();
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

            Debug.Log("Display options based on last type (" + lastPlatformType + ")");
            foreach (var option in nextPlatformOptions)
            {
                Debug.Log(option);
            }


            int randomPick = Random.Range(0, nextPlatformOptions.Length);

            Debug.Log("Random number min (" + 0 + "), max  (" + nextPlatformOptions.Length + ")");
            Debug.Log("Random choice: " + randomPick);


            AddPlatform(nextPlatformOptions[randomPick]);
        }
    }


    private void AddPlatform(string type)
    {
        GameObject newPlatform;
        Vector3    newPosition;

        

        // If this is the first platform place it at the beginning. 
        if (levelPlatforms.Count <= 0)
        {
            Debug.Log("Setting position values for the first platform");
            newPosition.x = 0f;
            newPosition.z = 0f;
        }
        else
        {
            GameObject lastPlatform = levelPlatforms[lastPlatformIdx];

            Debug.Log("Setting position values for platform at index " + (lastPlatformIdx + 1));
            newPosition.x = lastPlatform.transform.position.x + lastPlatform.transform.lossyScale.x + PlayerMetrics.staticJumpDistance;
            newPosition.z = lastPlatform.transform.position.z;
        }

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
        // Set the platforms Y value.
        newPosition.y = newPlatform.GetComponent<Platform>().GetHeight();
        newPlatform.transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
        Debug.Log("Full vec position for new " + newPlatform.GetComponent<Platform>().GetPlatformType() + " platform "  + newPosition);


        levelPlatforms.Add(newPlatform);

        Debug.Log(type + " platform has been added to the list");
    }


    private void AddPlayer()
    {
        
    }

}
