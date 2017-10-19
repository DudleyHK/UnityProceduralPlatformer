using System.Collections;
using UnityEngine;

//public class Rules : ScriptableObject
//{
//    private static float upperZoneHeight = PlayerMetrics.jumpHeight;
//    private static float middleZoneHeight = 0f;
//    private static float lowerZoneHeight = -PlayerMetrics.jumpHeight;


//    public static string[] NextZoneLayer(string layer)
//    {
//        switch(layer)
//        {
//            case "Top":     return new string[] { "Middle", "Top"};

//            case "Middle":  return new string[] { "Top", "Middle", "Bottom" };

//            case "Bottom":  return new string[] { "Middle", "Bottom"};

           
//            default:
//                Debug.Log("ERROR: Last Platform name (" + layer + ") is invalid.");
//                break;
//        }
//        return null;
//    }
   

//    public static float GetHeight(string layer)
//    {
//        switch (layer)
//        {
//            case "Top":    return upperZoneHeight;

//            case "Middle": return middleZoneHeight;

//            case "Bottom": return lowerZoneHeight;

//            default:
//                Debug.Log("ERROR: Last Platform name (" + layer + ") is invalid.");
//                break;
//        }
//        return 0f;
//    }
//}



class Rules : ScriptableObject
{
   private struct LittleLayerPositions
    {
        public static float centre;
        public static float left;
        public static float right;
    };

    private struct BigLayerPositions
    {
        public static float farLeft;
        public static float left;
        public static float right;
        public static float farRight;
    };

    private static bool initialised = false;


    private static void Init()
    {
        if (initialised) return;

        LittleLayerPositions.centre = 0f;
        LittleLayerPositions.left = -PlayerMetrics.jumpHeight * 1.5f;
        LittleLayerPositions.right = PlayerMetrics.jumpHeight * 1.5f;

        BigLayerPositions.farLeft   = -PlayerMetrics.jumpHeight * 2f;
        BigLayerPositions.left      = -PlayerMetrics.jumpHeight;
        BigLayerPositions.right     = PlayerMetrics.jumpHeight;
        BigLayerPositions.farRight  = PlayerMetrics.jumpHeight * 2f;

        initialised = true;
    }



    public static float[] Options(string layer, string platform)
    {
        Init();
        switch (layer)
        {
            case "Little Layer": return LittleLayer(platform);
            case "Big Layer":    return BigLayer(platform); 
        }
        return null;
    }

    public static float[] AllOfType(string layer)
    {
        Init();
        switch (layer)
        {
            case "Little Layer": return new float[] 
            {
                LittleLayerPositions.left,
                LittleLayerPositions.centre,
                LittleLayerPositions.right
            };
            case "Big Layer": return new float[]   
            {
                BigLayerPositions.farLeft,
                BigLayerPositions.left,
                BigLayerPositions.right,
                BigLayerPositions.farRight
            };
        }
        return null;
    }


    public static string PlatformName(string layer, float position)
    {
        Init();
        switch (layer)
        {
            case "Little Layer":
                //Debug.Log("Check little layer positions");
                if (position == LittleLayerPositions.left)   return "Left";
                if (position == LittleLayerPositions.centre) return "Centre";
                if (position == LittleLayerPositions.right)  return "Right";
                break;
            case "Big Layer":
                if (position == BigLayerPositions.farLeft)  return "Far Left";
                if (position == BigLayerPositions.farRight) return "Far Right";
                if (position == BigLayerPositions.left)     return "Left";
                if (position == BigLayerPositions.right)    return "Right";
                break;
        }
        return null;
    }

    private static float[] LittleLayer(string platform)
    {
        switch (platform)
        {
            case "Left":   return new float[] 
            {
                BigLayerPositions.farLeft,
                BigLayerPositions.left,
                BigLayerPositions.right
            };
            case "Centre": return new float[]
            {
                BigLayerPositions.farLeft,
                BigLayerPositions.left,
                BigLayerPositions.right,
                BigLayerPositions.farRight
            }; 
            case "Right":  return new float[] 
            {
                BigLayerPositions.left,
                BigLayerPositions.right,
                BigLayerPositions.farRight
            };
        }
        return null;
    }


    private static float[] BigLayer(string platform)
    {
        switch (platform)
        {
            case "Far Left":  return new float[] { LittleLayerPositions.left    };
            case "Far Right": return new float[] { LittleLayerPositions.right   };
            case "Left":      return new float[] { LittleLayerPositions.left,  LittleLayerPositions.centre };
            case "Right":     return new float[] { LittleLayerPositions.right, LittleLayerPositions.centre };
        }
        return null;
    }
}