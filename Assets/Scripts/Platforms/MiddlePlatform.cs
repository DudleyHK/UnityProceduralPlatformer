using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddlePlatform : Platform
{
    public override float GetHeight()
    {
        return 0f;
    }

    public override string GetPlatformType()
    {
        return "Middle";
    }
}
