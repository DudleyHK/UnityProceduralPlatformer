/*
 
 
 
 
*/
using System;
using UnityEngine;



public class ThresholdZone : Zone
{
    public GameObject lockableZone;



    private void Start()
    {
        var findObject = FindObjectOfType<LockableZone>();
        if (findObject)
        {
            lockableZone = findObject.gameObject;
        }
    }



    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            lockableZone.GetComponent<LockableZone>().isLocked = true;
            // set zone color to blue.
            // set the lockable zone to red.
        }
    }
}