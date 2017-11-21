using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildToScene : MonoBehaviour
{
    // Fills the scene with the game objects.
    // Places the parents into the scene
    public GameObject parentPrefab;
    public GameObject floorPrefab;
    public GameObject groundPrefab;
    public GameObject jumpPrefab;
    public GameObject debugPrefab;
    public List<GameObject> tileList;

    private Vector2 tilePosition;
    private float xOffset  ;
    private float yOffset  ;


    public List<Vector2> BuildParents(ushort total)
    {
        var parentList = new List<Vector2>();
        var parentSize = parentPrefab.transform.lossyScale;
        var position = new Vector2(0f, 0f);

      
        var xPos = -136f;
        var yPos = -46f;

        for (var i = 0; i < total; i++)
        {
            if (i == 0)
            {
                position.x = xPos;
                position.y = yPos;
            }
            else
            {
                position.x = parentList[i - 1].x + parentSize.x;
            }
            parentList.Add(position);
        }

        return parentList;
    }

    // Use the parents position instead of the Game object
    public List<GameObject> BuildObstacle(List<char> obstacleData, Vector2 parentPosition, ushort dataLength)
    {
        SetTileOffsets(parentPosition);
        tileList = new List<GameObject>();
        for (ushort i = 0; i < obstacleData.Count; i++)
        {
            var tilePosition = GetAlignment(parentPosition, dataLength, i);
            var tileType = GetTileType(obstacleData[i]);
            if (!tileType)
            {
                //print("Tile type is null (jump or space)");
                continue;
            }
            var newTile = Instantiate(tileType, tilePosition, Quaternion.identity);
            tileList.Add(newTile);
        }
        return tileList;
    }


    private Vector2 GetAlignment(Vector2 parentPosition, ushort dataLength, ushort index)
    {
        if(index % dataLength == 0 && index > 0)
        {
            //Debug.Log("Tile " + index + " is set to original");
            tilePosition.x = xOffset;
            tilePosition.y += -32f; // 32 - tile prefab size
        }
        else
        {
            tilePosition.x += 32f; // 32 - tile prefab size
        }
        return tilePosition;
    }


    private GameObject GetTileType(char type)
    {
        switch (type)
        {
            case 'F': return floorPrefab;
            case 'G': return groundPrefab;
            case 'J': return jumpPrefab;
            case 'j': return null;
            case 'W': return null;
            case 'w': return null;
            case 'D': return null; // Danger
            case '*': return null;
        }
        return debugPrefab;
    }


    private void SetTileOffsets(Vector2 parentPosition)
    {
       xOffset = parentPosition.x - (parentPrefab.transform.lossyScale.x / 2f) - 16f; // 16 = tile size
       yOffset = parentPosition.y + (parentPrefab.transform.lossyScale.y / 2f) - 16f;
       tilePosition = new Vector2(xOffset, yOffset);
        Debug.Log("Tile offset position is " + tilePosition);
    }
}
