using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Kinematic equation
    // final velocity squared = initial velocity squared + 2 * acceleration * displacement;


    // minJumpForce = sqrt(2 * gravity * minJumpHeight);
    private Rigidbody playerRigidbody { get { return GetComponent<Rigidbody>(); } }
    private Vector3 newPosition;
    private float moveSpeed = 5f;
    private float jumpHeight = PlayerMetrics.jumpHeight;
    private float timeToJumpApex = 0.4f;
    private float gravity;
    private float jumpVelocity;


    private void Start()
    {
        newPosition = Vector3.zero;

        gravity = -(25 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
    }


    private void Update()
    {
        Vector3 newPosition = transform.position;
       
        if (Input.GetKeyDown(KeyCode.W))
        {
          newPosition.y += jumpVelocity * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A))
        {
            newPosition.x += -moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newPosition.x += moveSpeed * Time.deltaTime;
        }

        transform.position = newPosition;
    }
}
