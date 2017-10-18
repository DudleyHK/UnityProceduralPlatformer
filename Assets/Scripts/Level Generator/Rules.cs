﻿using System.Collections;
using UnityEngine;




public class Rules : ScriptableObject
{
    public static string[] NextPlatform(string lastType)
    {
        switch(lastType)
        {
            case "Top":     return new string[] { "Middle", "Top", "High Top"};

            case "Middle":  return new string[] { "Top", "Middle", "Bottom" };

            case "Bottom":  return new string[] { "Middle", "Bottom"};

            default:
                Debug.Log("ERROR: Last Platform name (" + lastType + ") is invalid.");
                break;
        }
        return null;
    }
}
