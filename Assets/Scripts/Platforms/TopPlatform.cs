using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TopPlatform : Platform
{
    public override float GetHeight(GameObject lastPlatform)
    {
        return lastPlatform.transform.position.y + PlayerMetrics.jumpHeight;
    }

    public override string GetPlatformType()
    {
        return "Top";
    }
}
