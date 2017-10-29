using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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
                        print("create next flag is " + createNext);
                    }
                    else
                    {
                        createNext = false;
                        print("create next flag is " + createNext);
                    }
                }));
            }
            yield return false;
        }
        yield return true;
    }



    private GameObject CreateNew(Vector2 position)
    {
       return Instantiate(platformPrefab, position, Quaternion.identity);
    }



    private IEnumerator FindNewPosition(Action<bool> flag)
    {
        print("Starting find new position calculations");

        var lastPlatform = platforms[platforms.Count - 1];

        var top    = lastPlatform.transform.position.y + lastPlatform.transform.lossyScale.y + platformPrefab.transform.lossyScale.y;    
        var right  = lastPlatform.transform.position.x + lastPlatform.transform.lossyScale.x + platformPrefab.transform.lossyScale.x;    
        var bottom = lastPlatform.transform.position.y - lastPlatform.transform.lossyScale.y - platformPrefab.transform.lossyScale.y;    
        var left   = lastPlatform.transform.position.x - lastPlatform.transform.lossyScale.x - platformPrefab.transform.lossyScale.x;

        print("Top position: "    + top);
        print("Right position: "  + right);
        print("Bottom position: " + bottom);
        print("Left position: "   + left);

        // If this produces diagonally placed platforms. Add or Subtract a 10th of the platforms size off.
        var maxY = top    + lastPlatform.transform.lossyScale.y;
        var minY = bottom - lastPlatform.transform.lossyScale.y;
        var maxX = right  + lastPlatform.transform.lossyScale.x;
        var minX = left   - lastPlatform.transform.lossyScale.x;

        bool intersection = false;
        do
        {
            var randomHeight = UnityEngine.Random.Range(minY, maxY);
            var randomWidth  = UnityEngine.Random.Range(minX, maxX);

            var newPosition = Vector2.zero;
            var randomPlacement = UnityEngine.Random.Range(1, 4);
            print("New Random Placement is " + randomPlacement);

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

            print("New position is at " + newPosition);
            GameObject newPlatform = CreateNew(newPosition);
            intersection = ValidatePosition(newPlatform);

            flag(false);
            yield return false;
       
        } while(intersection);

        print("Valid platform position found.");
        flag(true);
        yield return true;
    }


    private bool ValidatePosition(GameObject newPlatform)
    {
        var bounds = newPlatform.GetComponent<SpriteRenderer>().bounds;
        foreach(var platform in platforms)
        {
            if(Overlapping(bounds, platform.GetComponent<SpriteRenderer>().bounds))
            {
                print("Platform intersects another. Regenerate.");
                Destroy(newPlatform);
                return true;
            }
        }
        platforms.Add(newPlatform);
        return false;
    }


    private bool Overlapping(Bounds subject, Bounds other)
    {
        if (subject.max.x <= other.min.x) return false; // subject is left of other
        if (subject.min.x >= other.max.x) return false; // subject is right of other
        if (subject.max.y <= other.min.y) return false; // subject is above other
        if (subject.min.y >= other.max.y) return false; // subject is below other
        return true; // bounds overlap
    }


    private bool Overlapping(Vector2 lastPos, Vector2 newPos)
    {
        var distance = Vector2.Distance(lastPos, newPos);
        if(distance <= 0)
        {
            return true;
        }
        return false;
    }
}
