/*
 
 
 
 
*/
using System;
using UnityEngine;



public class RewardZone : Zone
{
    public GameObject lockableZone;
    public bool isRewarded = false;



    public void IdentifyZoneFriends()
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
            if (lockableZone.GetComponent<LockableZone>().isLocked)
            {
                lockableZone.GetComponent<LockableZone>().isLocked = false;
                isRewarded = true;

                this.GetComponent<Renderer>().material.color = Color.green;
                lockableZone.GetComponent<Renderer>().material.color = Color.gray;
            }
        }
    }
}