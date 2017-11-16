using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformWorldObjectGenerator : MonoBehaviour
{
    public GameObject groundPrefab;
    public GameObject debugPrefab;
    public float xAlignment = 0f;
    public float yAlignment = 0f;

    private List<float> rowHeights = new List<float>(new float[] { -14f, -46f, -78f, -100f });



    public void CreateWorldObject(List<char> obstacle)
    {
        for(int i = 0; i < obstacle.Count; i++)
        {
            SetAlignment(i);
            var tileType = GetTileType(obstacle[i]);

            if(!tileType)
            {
                print("tile type is null (jump or space)");
                continue;
            }

            var newTile = Instantiate(groundPrefab, new Vector2(xAlignment, yAlignment), Quaternion.identity);
        }
    }


    private void SetAlignment(int index)
    {
        // get rows and columns 
        // yAlignment = rowHeights[row];
    }


    private GameObject GetTileType(char type)
    {
        switch(type)
        {
            case 'G': return groundPrefab;
            case 'J': return null;
            case '*': return null;
        }
        return debugPrefab;
    }
}
