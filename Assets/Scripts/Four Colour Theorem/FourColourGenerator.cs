using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class FourColourGenerator : MonoBehaviour
{
    private enum Place
    {
        Top    = 1,
        Right  = 2, 
        Bottom = 3,
        Left   = 4
    }
    
    public List<GameObject> platforms = new List<GameObject>();
    public GameObject platformPrefab;
    public short totalPlatforms = 7;
    public float fraction = 10f;
    public float timeToGenerate = 0f;




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


    public void Regenerate()
    {
        ResetLevel();
        StartCoroutine(Generate());
    }



    private void ResetLevel()
    {
        foreach(var platform in platforms)
        {
            if(platform)
            { 
                Destroy(platform);
            }
        }
        platforms.Clear();
    }


    private IEnumerator Generate()
    {
        bool createNext = true;
        timeToGenerate = 0f;
        while(true)
        {
            if(platforms.Count >= totalPlatforms) break;
            if(platforms.Count <= 0)
            {
                var newPlatform = CreateNew(new Vector2(0f, 0f));
                platforms.Add(newPlatform);
                continue;
            }

            if(createNext)
            {
                StartCoroutine(FindNewPosition(result =>
                {
                    if(result)
                    {
                        createNext = true;
                        //print("create next flag is " + createNext);
                    }
                    else
                    {
                        createNext = false;
                        //print("create next flag is " + createNext);
                    }
                }));
            }
            timeToGenerate += Time.deltaTime;
            yield return false;
        }
        Debug.Log("Time to generate: " + timeToGenerate);
        yield return true;
    }



    private GameObject CreateNew(Vector2 position)
    {
       return Instantiate(platformPrefab, position, Quaternion.identity);
    }



    private IEnumerator FindNewPosition(Action<bool> flag)
    {
        //print("Starting find new position calculations");

        var lastPlatform = platforms[platforms.Count - 1];
        var lastPlatformRenderer = lastPlatform.GetComponent<SpriteRenderer>();
        var platformPrefabRenderer = platformPrefab.GetComponent<SpriteRenderer>();

        var lastSizeOffsetX   = (lastPlatformRenderer.size.x / 2);
        var lastSizeOffsetY   = (lastPlatformRenderer.size.y / 2);
        var prefabSizeOffsetX = (platformPrefabRenderer.size.x / 2);
        var prefabSizeOffsetY = (platformPrefabRenderer.size.y / 2);

        var top    = lastPlatform.transform.position.y + lastSizeOffsetY + prefabSizeOffsetY;    
        var right  = lastPlatform.transform.position.x + lastSizeOffsetX + prefabSizeOffsetX;    
        var bottom = lastPlatform.transform.position.y - lastSizeOffsetY - prefabSizeOffsetY;    
        var left   = lastPlatform.transform.position.x - lastSizeOffsetX - prefabSizeOffsetX;

        //print("Top position: "    + top);
        //print("Right position: "  + right);
        //print("Bottom position: " + bottom);
        //print("Left position: "   + left);

        var fractionOfWidth  = lastPlatformRenderer.size.x / fraction;
        var fractionOfHeight = lastPlatformRenderer.size.y / fraction;

        var maxY = top    - fractionOfHeight;
        var minY = bottom + fractionOfHeight;
        var maxX = right  - fractionOfWidth;
        var minX = left   + fractionOfWidth;

        while(true)
        {
            var randomHeight = UnityEngine.Random.Range(minY, maxY);
            var randomWidth  = UnityEngine.Random.Range(minX, maxX);

            var newPosition = Vector2.zero;
            var randomPlacement = UnityEngine.Random.Range(1, 4);
            //print("New Random Placement is " + randomPlacement);

            switch((Place)randomPlacement)
            {
                case Place.Top:
                    newPosition.y = top;
                    newPosition.x = randomWidth;
                    break;
                case Place.Right:
                    newPosition.y = randomHeight;
                    newPosition.x = right;
                    break;
                case Place.Bottom:
                    newPosition.y = bottom;
                    newPosition.x = randomWidth;
                    break;
                case Place.Left:
                    newPosition.y = randomHeight;
                    newPosition.x = left;
                    break;
                default:
                    print("ERROR: Random placement value invalid");
                    break;
            }

            //print("New position is at " + newPosition);
            GameObject newPlatform = CreateNew(newPosition);
            if(PositionValid(newPlatform)) break;

            flag(false);
            yield return false;
       
        } 
        //print("Valid platform position found.");
        flag(true);
        yield return true;
    }


    private bool PositionValid(GameObject newPlatform)
    {
        var bounds = newPlatform.GetComponent<SpriteRenderer>().bounds;
        foreach(var platform in platforms)
        {
            if(Overlapping(bounds, platform.GetComponent<SpriteRenderer>().bounds))
            {
               // print("Platform intersects another. Regenerate.");
                Destroy(newPlatform);
                return false;
            }
        }
        platforms.Add(newPlatform);
        return true;
    }


    private bool Overlapping(Bounds subject, Bounds other)
    {
        if (subject.max.x <= other.min.x) return false; // subject is left of other
        if (subject.min.x >= other.max.x) return false; // subject is right of other
        if (subject.max.y <= other.min.y) return false; // subject is above other
        if (subject.min.y >= other.max.y) return false; // subject is below other
        return true; // bounds overlap
    }

    //// %#c  == CMD + SHIFT + C
    [MenuItem("Tools/Clear Console %c")] // C
    private static void ClearConsoleWindow()
    {
        var logEntries = Type.GetType("UnityEditorInternal.LogEntries, UnityEditor.dll");
        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        clearMethod.Invoke(null, null);

    }
}
