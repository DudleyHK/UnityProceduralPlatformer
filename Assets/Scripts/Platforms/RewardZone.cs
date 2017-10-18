/*
 
 
 
 
*/
using System;
using UnityEngine;



public class RewardZone : Zone
{
    public GameObject lockableZone;
    public bool  isRewarded { get; set; }




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
        if(other.gameObject.tag == "Player")
        {
              if (lockableZone.GetComponent<LockableZone>().isLocked)
              {
                  lockableZone.GetComponent<LockableZone>().isLocked = false;
                  isRewarded = true;

                  // set colour of zone to green.
                  // set colour of lockable zone back to normal.. 
              }
            }
        }
    }
}