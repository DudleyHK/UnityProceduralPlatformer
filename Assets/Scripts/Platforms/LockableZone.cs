/*
 
 
 
 
*/
using System;
using UnityEngine;



public class LockableZone : Zone
{
    public GameObject rewardZone;
    public GameObject thresholdZone;
    public GameObject generator;

    public bool isLocked = false;



    public void IdentifyZoneFriends()
    {
        var findRewardZone = GameObject.Find("Reward Zone");
        if(findRewardZone)
        {
            rewardZone = findRewardZone.gameObject;
        }

        var findGeneratorObject = FindObjectOfType<Generator>();
        if (findGeneratorObject)
        {
            generator = findGeneratorObject.gameObject;
        }
    }



    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isLocked && rewardZone.GetComponent<RewardZone>().isRewarded)
            {
                // Game won
                generator.GetComponent<Generator>().Regenerate(); 
            }
            else if(thresholdZone.GetComponent<ThresholdZone>().isHit && !rewardZone.GetComponent<RewardZone>().isRewarded)
            {
                // kill player
                Destroy(other.gameObject);
                return;
            }
        }
    }
}