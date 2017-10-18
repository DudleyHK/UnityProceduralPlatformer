using System.Collections;
using UnityEngine;




public class Rules : ScriptableObject
{
    private static float upperZoneHeight = PlayerMetrics.jumpHeight;
    private static float middleZoneHeight = 0f;
    private static float lowerZoneHeight = -PlayerMetrics.jumpHeight;


    public static string[] NextZoneLayer(string layer)
    {
        switch(layer)
        {
            case "Top":     return new string[] { "Middle", "Top"};

            case "Middle":  return new string[] { "Top", "Middle", "Bottom" };

            case "Bottom":  return new string[] { "Middle", "Bottom"};

           
            default:
                Debug.Log("ERROR: Last Platform name (" + layer + ") is invalid.");
                break;
        }
        return null;
    }
   


}
