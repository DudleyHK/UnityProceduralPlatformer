using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script which sets the dimentions of the section. Height, Length, Start position, End position.
/// </summary>
public class SectionScript : MonoBehaviour 
{
    public GameObject associatedSection;
    public List<Transform> elements;
    public ushort difficulty;
    public ushort costOfFailure;

    public float Height { get; set; }
    public float Length { get; set; }
    public float Bottom { get; set; }
    public float Left   { get; set; }
    public float Right  { get; set; }
    public float Top    { get; set; }
    public Vector3 originPosition { get; set; }
    public Vector3 EntryPoint     { get; set; }
    public Vector3 ExitPoint      { get; set; }


    private void Start()
    {
        associatedSection = this.gameObject;
        originPosition = associatedSection.transform.position;

        /* Get the object on the far left and far right */
        // Keep a list of all the objs which make the section.
        associatedSection.GetComponentsInChildren(elements);
        if(elements.Count <= 0)
        {
            print(associatedSection.name + " has no children with transform components");
            return;
        }

        // remove self from list.
        elements.Remove(transform);

        // Set some basic values.
        Left   = elements[0].position.x;
        Right  = elements[0].position.x;
        Top    = elements[0].position.y;
        Bottom = elements[0].position.y;

        // Go through the list and check which object has the lowest x position and highest x position.
        foreach(var elem in elements) 
        {
            if(elem.position.x < Left)
            {
                Left = elem.position.x;
            }
            
            if(elem.position.x > Right)
            {
                Right = elem.position.x;
            }

            if(elem.position.y < Bottom)
            {
                Bottom = elem.position.y;
            }

            if(elem.position.y > Top)
            {
                Top = elem.position.y;
            }
        }

        // Take into account the tile size and that the centre of the tile is 
        //   used for calculations.
        Length = (Right - Left) + elements[0].localScale.x;
        Height = (Top - Bottom) + elements[0].localScale.y;
    }

    private void Update()
    {
        originPosition = transform.position;
    }
}
