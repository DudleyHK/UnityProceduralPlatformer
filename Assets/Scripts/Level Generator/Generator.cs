using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public List<GameObject> generatedZones = new List<GameObject>();
    public GameObject zonePrefab;
    public GameObject playerPrefab;
    public ushort totalActs = 4;

    private GameObject playerClone;


    public enum Acts
    {
        CallToAdventure,
        LockedArea,
        CrossingTheThreshold,
        Reward
    }

    private List<Acts> actList = new List<Acts>(new Acts[]
    {
        Acts.CallToAdventure,
        Acts.LockedArea,
        Acts.CrossingTheThreshold,
        Acts.Reward
    });




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
        foreach (var zone in generatedZones)
        {
            Destroy(zone);
        }
        generatedZones.Clear();
        Destroy(playerClone);

        HerosJourney();
        AddPlayer();
    }





    private void HerosJourney()
    {
        for(int act = 0; act < totalActs; act++)
        {
            if (act == 0)
            {
                //AddZone(actList[act]);
                continue;
            }



        }
    }


    private void AddZone(string layer, Acts act)
    {
        Vector3 zonePosition = Vector3.zero;

        GameObject newZone = Instantiate(zonePrefab, Vector3.zero, Quaternion.identity);
        newZone.GetComponent<Zone>().layer = layer;


        if (act == Acts.CallToAdventure)
        {
            newZone.AddComponent<LockableZone>();

            zonePosition.x = 0f;
            zonePosition.z = 0f;
        }

        // Assign Platform correct type
        switch (layer)
        {

            case "Top":
                
                break;

            case "Middle":
                newPlatform.AddComponent<MiddlePlatform>();
                break;

            case "Bottom":
                newPlatform.AddComponent<BottomPlatform>();
                break;
        }




    }



    //private void GenerateLevel()
    //{
    //    for(int i = 0; i < levelLength; i++)
    //    {
    //        lastPlatformIdx = levelPlatforms.Count - 1;

    //        // Place the starting platform.
    //        if (i == 0)
    //        {
    //            AddPlatform(startingPlatform);
    //            continue;
    //        }


    //        // Get a list of the possible next moves
    //        string lastPlatformType = levelPlatforms[lastPlatformIdx].GetComponent<Platform>().GetPlatformType();
    //        string[] nextPlatformOptions;
    //        if (lastPlatformType == "High Top")
    //        {
    //            nextPlatformOptions = Rules.NextPlatform("Middle");
    //        }
    //        else
    //        {
    //            nextPlatformOptions = Rules.NextPlatform(lastPlatformType);
    //        }
    //        //Debug.Log("Display options based on last type (" + lastPlatformType + ")");
    //        //foreach (var option in nextPlatformOptions)
    //        //{
    //        //    Debug.Log(option);
    //        //}

    //        int randomPick = Random.Range(0, nextPlatformOptions.Length);
    //        string currentPlatformType = nextPlatformOptions[randomPick];

    //        //Debug.Log("Random number min (" + 0 + "), max  (" + nextPlatformOptions.Length + ")");
    //        //Debug.Log("Random choice: " + randomPick);

    //        if (lastPlatformType == "Top" && currentPlatformType == "Middle")
    //        {
    //            int flipCoin = Random.Range(0, 1);
    //            if (flipCoin == 1)
    //            {
    //                AddUnlistedPlatform("High Top");
    //            }
    //            AddPlatform("Middle");


    //        }
    //        else
    //        {
    //            AddPlatform(currentPlatformType);
    //        }


    //    }
    //}


    //private void AddPlatform(string type)
    //{
    //    GameObject newPlatform;
    //    Vector3    newPosition;


    //    // Create the new platform
    //    newPlatform = Instantiate(platformPrefab);

    //    // Assign Platform correct type
    //    switch (type)
    //    {
    //        case "High Top":
    //            newPlatform.AddComponent<HighTopPlatform>();
    //            break;

    //        case "Top":
    //            newPlatform.AddComponent<TopPlatform>();
    //            break;

    //        case "Middle":
    //            newPlatform.AddComponent<MiddlePlatform>();
    //            break;

    //        case "Bottom":
    //            newPlatform.AddComponent<BottomPlatform>();
    //            break;
    //    }


    //    // If this is the first platform place it at the beginning. 
    //    if (levelPlatforms.Count <= 0)
    //    {
    //       // Debug.Log("Setting position values for the first platform");
    //        newPosition.x = 0f;
    //        newPosition.y = 0f;
    //        newPosition.z = 0f;
    //    }
    //    else
    //    {
    //        GameObject lastPlatform = levelPlatforms[lastPlatformIdx];

    //       // Debug.Log("Setting position values for platform at index " + (lastPlatformIdx + 1));
    //        newPosition.x = lastPlatform.transform.position.x + lastPlatform.transform.lossyScale.x + PlayerMetrics.staticJumpDistance;
    //        newPosition.y = newPlatform.GetComponent<Platform>().GetHeight();
    //        newPosition.z = lastPlatform.transform.position.z;
    //    }

    //    newPlatform.transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
    //    levelPlatforms.Add(newPlatform);

    //    //Debug.Log("Full vec position for new " + newPlatform.GetComponent<Platform>().GetPlatformType() + " platform "  + newPosition);
    //    //Debug.Log(type + " platform has been added to the list");
    //}


    //private void AddUnlistedPlatform(string type)
    //{
    //    GameObject newPlatform;
    //    Vector3 newPosition;


    //    // Create the new platform
    //    newPlatform = Instantiate(platformPrefab);

    //    // Assign Platform correct type
    //    switch (type)
    //    {
    //        case "High Top":
    //            newPlatform.AddComponent<HighTopPlatform>();
    //            break;

    //        case "Top":
    //            newPlatform.AddComponent<TopPlatform>();
    //            break;

    //        case "Middle":
    //            newPlatform.AddComponent<MiddlePlatform>();
    //            break;

    //        case "Bottom":
    //            newPlatform.AddComponent<BottomPlatform>();
    //            break;
    //    }

    //    GameObject lastPlatform = levelPlatforms[lastPlatformIdx];

    //    // Debug.Log("Setting position values for platform at index " + (lastPlatformIdx + 1));
    //    newPosition.x = lastPlatform.transform.position.x + lastPlatform.transform.lossyScale.x + PlayerMetrics.staticJumpDistance;
    //    newPosition.y = newPlatform.GetComponent<Platform>().GetHeight();
    //    newPosition.z = lastPlatform.transform.position.z;

    //    newPlatform.transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);

    //}


    private void AddPlayer()
    {
        float yOffset = levelPlatforms[0].transform.lossyScale.y + (playerPrefab.transform.lossyScale.y / 2);

        float x = levelPlatforms[0].transform.position.x;
        float y = levelPlatforms[0].transform.position.y + yOffset;
        float z = levelPlatforms[2].transform.position.z;


        playerClone = Instantiate(playerPrefab, new Vector3(x, y, z), Quaternion.identity);
    }

}
