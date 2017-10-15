/*
 
 
 
 
*/
using System;
using UnityEngine;



public class Platform : MonoBehaviour
{
    public virtual float GetHeight()
    {
        return 0f;
    }


    public virtual string GetPlatformType()
    {
        return "";
    }
}