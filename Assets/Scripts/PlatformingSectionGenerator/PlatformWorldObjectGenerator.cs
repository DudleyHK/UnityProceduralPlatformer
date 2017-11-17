using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformWorldObjectGenerator : MonoBehaviour
{
    public List<GameObject> obstacleContainers = new List<GameObject>();
    public GameObject floorPrefab;
    public GameObject groundPrefab; 
    public GameObject debugPrefab;
    public GameObject obstacleContainerPrefab;
    public float xAlignment = 0f;
    public float yAlignment = 0f;


    private List<float> positions = new List<float>(new float[] { 0f, 32f, 64f, 96f });
    private Vector2 obstacleContainerPosition = new Vector2(-200f, -110f);
    private float obstacleContainerSize = 96f;



    public void CreateWorldObject(List<List<char>> characterLists)
    {
   
        for(var i = 0; i < characterLists.Count; i++)
        {
            SetContainerAligment(i);
            GenerateSingleCharacterList(characterLists[i]);
        }   
    }

    private void GenerateSingleCharacterList(List<char> obstacle)
    {
        var obstacleContainer = Instantiate(obstacleContainerPrefab, new Vector2(obstacleContainerPosition.x, obstacleContainerPosition.y), Quaternion.identity);
        for(int i = 0; i < obstacle.Count; i++)
        {
            SetAlignment(i);
            var tileType = GetTileType(obstacle[i]);

            if(!tileType)
            {
                print("Tile type is null (jump or space)");
                continue;
            }
            var newTile = Instantiate(tileType, new Vector2(xAlignment, yAlignment), Quaternion.identity, obstacleContainer.transform);
        }
        obstacleContainers.Add(obstacleContainer);
    }

    private void SetAlignment(int index)
    {
        var x = index % 4;
        var y = index / 4;
        var tileSize = floorPrefab.transform.localScale;

        yAlignment = -positions[y];
        xAlignment = positions[x];
       // print("xAlignment: " + xAlignment);
       // print("yAlignment: " + yAlignment);
    }


    private GameObject GetTileType(char type)
    {
        switch(type)
        {
            case 'F': return floorPrefab;
            case 'G': return groundPrefab;
            case 'D': return null;
            case 'J': return null;
            case '*': return null;
        }
        return debugPrefab;
    }


    private void SetContainerAligment(int index)
    {
        if(index == 0)
        {
            obstacleContainerPosition.x += (obstacleContainerSize / 2f);
        }
        else
        {
            obstacleContainerPosition.x = (obstacleContainerSize / 2f) + (obstacleContainers[index - 1].transform.lossyScale.x / 2f);
        }
    }
}
