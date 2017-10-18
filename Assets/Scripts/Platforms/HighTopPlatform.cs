using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighTopPlatform : Platform
{
    public override float GetHeight()
    {
        return PlayerMetrics.jumpHeight * 2;
    }

    public override string GetPlatformType()
    {
        return "High Top";
    }
}
