using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MiddlePlatform : Platform
{
    public override float GetHeight(GameObject lastPlatform)
    {
        return lastPlatform.transform.position.y;
    }

    public override string GetPlatformType()
    {
        return "Middle";
    }
}
