using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float amplitudeX = 10f;
    public float amplitudeY = 5f;
    public float omegaX = 1f;
    public float omegaY = 5f;
    public float index = 0;

    private Vector2 moveTo = Vector2.zero;



	private void Update ()
    {
		if(Input.GetKey(KeyCode.W))
        {
            index += Time.deltaTime;
          
            moveTo.y = Mathf.Abs(amplitudeY * Mathf.Sin(omegaY * index));

        }
        if(Input.GetKey(KeyCode.D))
        {
            moveTo.x = amplitudeX * Mathf.Cos(omegaX * index);
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveTo.x = -amplitudeX * Mathf.Cos(omegaX * index);
        }
        transform.position = moveTo;
	}
}
