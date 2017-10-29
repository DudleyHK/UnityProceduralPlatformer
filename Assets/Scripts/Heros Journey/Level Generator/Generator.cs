using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public List<GameObject> generatedZones = new List<GameObject>();
    public List<GameObject> previousPlatforms = new List<GameObject>();
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            Regenerate();
        }
    }


    public void Regenerate()
    {
        foreach (var zone in generatedZones)
        {
            Destroy(zone);
        }
        generatedZones.Clear();
        Destroy(playerClone);

        GenerateLevel();
        AddZoneTypeFriends();

        AddPlayer();
    }


    private void GenerateLevel()
    {
        // Place the first platform
        float[] firstPlatformPositions = Rules.AllOfType("Little Layer");
        int randomPick1 = Random.Range(0, firstPlatformPositions.Length);
        float firstPlatformPosition = firstPlatformPositions[randomPick1];

        GameObject firstPlatform = Instantiate(zonePrefab, new Vector3(0, firstPlatformPosition, 0), Quaternion.identity);
        firstPlatform.AddComponent<LockableZone>().layer = Rules.PlatformName("Little Layer", firstPlatformPosition);
        firstPlatform.name = "Lockable Zone";
        generatedZones.Add(firstPlatform);


        // Place second lot of platforms.
        int numberOfPlatformsToGenerate = Random.Range(2, 3);
        List<float> secondPlatformsPositions = new List<float>(Rules.Options("Little Layer", firstPlatform.GetComponent<Zone>().layer));
        Vector3 newPosition;

        bool thresholdMade = false;

        for (int i = 0; i < numberOfPlatformsToGenerate; i++)
        {
            int randomPick2 = Random.Range(0, secondPlatformsPositions.Count);

            newPosition.x = firstPlatform.transform.position.x + firstPlatform.transform.lossyScale.x + PlayerMetrics.staticJumpDistance;
            newPosition.y = secondPlatformsPositions[randomPick2];
            newPosition.z = firstPlatform.transform.position.z;

            GameObject layerTwoPlatform = Instantiate(zonePrefab, newPosition, Quaternion.identity);

            if (!thresholdMade)
            {
                layerTwoPlatform.AddComponent<ThresholdZone>().layer = Rules.PlatformName("Big Layer", secondPlatformsPositions[randomPick2]);
                layerTwoPlatform.name = "Threshold Zone";
                thresholdMade = true;
            }
            else
            {
                layerTwoPlatform.AddComponent<Zone>().layer = Rules.PlatformName("Big Layer", secondPlatformsPositions[randomPick2]);
            }



            secondPlatformsPositions.RemoveAt(randomPick2);
            generatedZones.Add(layerTwoPlatform);
            previousPlatforms.Add(layerTwoPlatform);
        }


        // Place third layer
        List<GameObject> thirdLayerPlatforms = ThirdLayer();


        // Place the final platform
        int randomPick4 = Random.Range(0, thirdLayerPlatforms.Count);
        // Debug.Log("Number of third layer platforms " + thirdLayerPlatforms.Count);
        // Debug.Log("Random Pick value: " + randomPick4);


        GameObject thirdLayerPlatformToGenerateFrom = thirdLayerPlatforms[randomPick4];
        List<float> fourthPlatformsPositions = new List<float>(Rules.Options("Little Layer", thirdLayerPlatformToGenerateFrom.GetComponent<Zone>().layer));

        int randomHeight = Random.Range(0, fourthPlatformsPositions.Count);

        newPosition.x = thirdLayerPlatformToGenerateFrom.transform.position.x + thirdLayerPlatformToGenerateFrom.transform.lossyScale.x + PlayerMetrics.staticJumpDistance;
        newPosition.y = fourthPlatformsPositions[randomHeight];
        newPosition.z = thirdLayerPlatformToGenerateFrom.transform.position.z;

        GameObject layerFourPlatform = Instantiate(zonePrefab, newPosition, Quaternion.identity);
        layerFourPlatform.name = "Reward Zone";
        layerFourPlatform.AddComponent<RewardZone>().layer = Rules.PlatformName("Big Layer", fourthPlatformsPositions[randomHeight]);

        generatedZones.Add(layerFourPlatform);

    }



    private void AddZoneTypeFriends()
    {
        GameObject lockableZone  = default(GameObject);
        GameObject rewardZone    = default(GameObject);
        GameObject thresholdZone = default(GameObject);

        foreach (var zone in generatedZones)
        {
            if (zone.name == "Lockable Zone")
            {
                lockableZone = zone;
            }
            else if (zone.name == "Threshold Zone")
            {
                thresholdZone = zone;
            }
            else if (zone.name == "Reward Zone")
            {
                rewardZone = zone;
            }
            else
            {

            }
        }

        lockableZone.GetComponent<LockableZone>().rewardZone    = rewardZone;
        lockableZone.GetComponent<LockableZone>().thresholdZone = thresholdZone;
        lockableZone.GetComponent<LockableZone>().generator     = this.gameObject;

        thresholdZone.GetComponent<ThresholdZone>().rewardZone = rewardZone;
        thresholdZone.GetComponent<ThresholdZone>().lockableZone = lockableZone;

        rewardZone.GetComponent<RewardZone>().lockableZone = lockableZone;
    }




    private List<GameObject> ThirdLayer()
    {
        Vector3 newPosition = Vector3.zero;

        // Place the third lot of platforms.
        List<GameObject> thirdLayerPlatforms = new List<GameObject>();
        for (int i = 0; i < 2; i++)
        {
            int randomPick3 = Random.Range(0, previousPlatforms.Count);
            GameObject platformToGenerateFrom = previousPlatforms[randomPick3];
            List<float> thirdPlatformsPositions = new List<float>(Rules.Options("Big Layer", platformToGenerateFrom.GetComponent<Zone>().layer));

            previousPlatforms.RemoveAt(randomPick3);

            int howManyToGenerate = Random.Range(1, 2);
            for (int j = 0; j < howManyToGenerate; j++)
            {
                int randomPick3A = Random.Range(0, thirdPlatformsPositions.Count);

                newPosition.x = platformToGenerateFrom.transform.position.x + platformToGenerateFrom.transform.lossyScale.x + PlayerMetrics.staticJumpDistance;
                newPosition.y = thirdPlatformsPositions[randomPick3A];
                newPosition.z = platformToGenerateFrom.transform.position.z;

                GameObject layerThreePlatform = Instantiate(zonePrefab, newPosition, Quaternion.identity);
                layerThreePlatform.AddComponent<Zone>().layer = Rules.PlatformName("Little Layer", thirdPlatformsPositions[randomPick3A]);

               // Debug.Log("Layer Three Name: " + layerThreePlatform.GetComponent<Zone>().layer);
               // Debug.Log("Layer Three pos: " + thirdPlatformsPositions[randomPick3A]);


                thirdPlatformsPositions.RemoveAt(randomPick3A);


                generatedZones.Add(layerThreePlatform);
                thirdLayerPlatforms.Add(layerThreePlatform);
            }
        }
        previousPlatforms.Clear();
        return thirdLayerPlatforms;
    }
    


    private void AddPlayer()
    {
        if (generatedZones.Count <= 0) return;

        print("Making player");

        float yOffset = generatedZones[0].transform.lossyScale.y + (playerPrefab.transform.lossyScale.y / 2);

        float x = generatedZones[0].transform.position.x;
        float y = generatedZones[0].transform.position.y + yOffset;
        float z = generatedZones[0].transform.position.z;

        print("PLacing at position " + new Vector3(x, y, z));
        playerClone = Instantiate(playerPrefab, new Vector3(x, y, z), Quaternion.identity);
    }

}