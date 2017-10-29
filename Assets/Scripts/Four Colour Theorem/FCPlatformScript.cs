using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCPlatformScript : MonoBehaviour 
{
    public enum Colour
    {
        Blue   = 1, 
        Red    = 2,
        Yellow = 3,
        Green  = 4,
        Unassigned = 5
    }
    public Colour colourValue = Colour.Unassigned;
    public Colour ColourValue
    {
        get
        {
            return colourValue;
        }

        set
        {
            SetColour(value);
        }
    }


    public List<GameObject> neighbours;
    public List<GameObject> Neighbours
    {
        get
        {
            return neighbours;
        }
        set
        {
            if(neighbours == null)
            {
                neighbours = new List<GameObject>();
            }
            neighbours = value;
        }
    }


    private void SetColour(Colour colourValue)
    {
        print("Setting colour to " + colourValue);
        var col = this.GetComponent<SpriteRenderer>().color;
        switch(colourValue)
        {
            case Colour.Blue:
                col = Color.blue;
                break;
            case Colour.Red:
                col = Color.red;
                break;
            case Colour.Yellow:
                col = Color.yellow;
                break;
            case Colour.Green:
                col = Color.green;
                break;
            case Colour.Unassigned:
                col = Color.grey;
                break;
            default:
                print("ERROR: Colour value invalid");
                return;
        }

        GetComponent<SpriteRenderer>().color = col;
        this.colourValue = colourValue;
    }
}
