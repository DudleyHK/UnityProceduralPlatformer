/*
 
 
 
 
*/
using System;
using UnityEngine;



public class Platform : MonoBehaviour
{
    /// <summary>
    /// Use the lastPlatforms y position to calculate the new height.
    /// </summary>
    /// <param name="lastPlatform"></param>
    /// <returns></returns>
    public virtual float GetHeight(GameObject lastPlatform)
    {
        return 0f;
    }

    public virtual string GetPlatformType()
    {
        return "";
    }
}