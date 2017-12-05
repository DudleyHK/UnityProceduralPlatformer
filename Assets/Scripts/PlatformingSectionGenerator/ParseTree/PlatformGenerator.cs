using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlatformGenerator : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject player;
    public Vector2 instantiationMarker;

    private List<GameObject> platformingSections;



    private void Start()
    {       
        platformingSections = new List<GameObject>(Resources.LoadAll<GameObject>("PrebuiltPlatforms/"));
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
            BuildLevel();
        }
    }


    /// <summary>
    /// Clear the list and destory world objects.
    /// </summary>
    private void ResetLevel()
    {
        if(platformingSections.Count <= 0)
        {
            print("No platforms to clear.");
            return;
        }

        foreach(var section in platformingSections)
        {
            Destroy(section);
        }
        platformingSections.Clear();
        Destroy(player);
    }


    private void BuildLevel()
    {
        PlacePlatforms();
        PlacePlayer();
    }


    private void PlacePlatforms()
    {
        PhaseOne();
        PhaseTwo();
        PhaseThree();
        PhaseFour();
        PhaseFive();
    }


    private void PlacePlayer()
    {

    }


    private void PhaseOne()
    {
        // run through the list and return anything with a easy value and add it to the eay list.
        platformingSections.FindAll(obj =>
        {
            return (obj.GetComponent<SectionScript>().difficulty == 1);
        });

        // stretch: if objects are added to lists as the generation process progresses, each check will be quicker


    }
    private void PhaseTwo()
    {

    }
    private void PhaseThree()
    {

    }
    private void PhaseFour()
    {

    }
    private void PhaseFive()
    {

    }
}
