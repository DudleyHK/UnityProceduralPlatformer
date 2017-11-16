using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour 
{
    public List<InputAxisState> inputs = new List<InputAxisState>();
    public InputState inputState;
    public string playerTag = "Player";

    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag(playerTag);
        if(player)
        {
            inputState = player.GetComponent<InputState>();
        }
        else
        {
            print("ERROR: Player tag not applied");
        }
    }

    void Update () 
    {
        foreach(var input in inputs)
        {
            inputState.SetButtonValue(input.button, input.value);
        }
	}
}
