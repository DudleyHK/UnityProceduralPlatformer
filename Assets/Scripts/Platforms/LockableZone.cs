/*
 
 
 
 
*/
using System;
using UnityEngine;



public class LockableZone : Zone
{
    public GameObject rewardZone;

    public bool  isLocked   { get; set; }



    private void Start()
    {
        var findObject = FindObjectOfType<RewardZone>();
        if(findObject)
        {
            rewardZone = findObject.gameObject;
        }
    }



    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (!isLocked && rewardZone.GetComponent<RewardZone>().isRewarded)
            {
                // Game won
            }
            else
            {
                // kill player
                // return;
            }
        }
    }
}