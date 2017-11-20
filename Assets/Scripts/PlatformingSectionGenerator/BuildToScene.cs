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
    public GameObject debugPrefab;



    public List<Vector2> BuildParents(ushort total)
    {
        var parentList = new List<Vector2>();
        var parentSize = parentPrefab.transform.lossyScale;
        var position = new Vector2(0f, 0f);

        var xPos = -180f;
        var yPos = -70f;

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
        List<GameObject> tileList = new List<GameObject>();
        for (ushort i = 0; i < obstacleData.Count; i++)
        {
            var tilePosition = GetAlignment(parentPosition, dataLength, i);
            var tileType = GetTileType(obstacleData[i]);
            if (!tileType)
            {
                print("Tile type is null (jump or space)");
                continue;
            }
            var newTile = Instantiate(tileType, tilePosition, Quaternion.identity);
            tileList.Add(newTile);
        }
        return tileList;
    }


    private Vector2 GetAlignment(Vector2 parentPosition, ushort dataLength, ushort index)
    {
        var xOffset = parentPosition.x - parentPrefab.transform.lossyScale.x + 16; // 16 = tile size
        var yOffset = parentPosition.y - parentPrefab.transform.lossyScale.y - 16;
        var position = new Vector2(xOffset, yOffset);

        if ((index % dataLength == 0) && (index > 0))
        {
            position.x = xOffset;
            position.y += 32; // 32 - tile prefab size
        }
        else
        {
            position.x += 32; // 32 - tile prefab size
        }
        return position;
    }


    private GameObject GetTileType(char type)
    {
        switch (type)
        {
            case 'F': return floorPrefab;
            case 'G': return groundPrefab;
            case 'D': return null;
            case 'J': return null;
            case '*': return null;
        }
        return debugPrefab;
    }
}
