/*
 
 
 
 
*/
using System;
using UnityEngine;



public class ThresholdZone : Zone
{
    public GameObject lockableZone;
    public GameObject rewardZone;
    public bool isHit = false;


    public void IdentifyZoneFriends()
    {
        var findObject = FindObjectOfType<LockableZone>();
        if (findObject)
        {
            lockableZone = findObject.gameObject;
        }

        var findRewardZone = GameObject.Find("Reward Zone");
        if (findRewardZone)
        {
            rewardZone = findRewardZone.gameObject;
        }

    }



    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Player")
        {
            if (rewardZone.GetComponent<RewardZone>().isRewarded) return;

            lockableZone.GetComponent<LockableZone>().isLocked = true;

            // set zone color to blue.
            this.GetComponent<Renderer>().material.color = Color.blue;
            // set the lockable zone to red.
            lockableZone.GetComponent<Renderer>().material.color = Color.red;

            isHit = true;
        }
    }
}