using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControls : MonoBehaviour
{
    [Header("ControlScrip")]
    [SerializeField]
    public KeyCode forward = KeyCode.UpArrow;
    [SerializeField]
    public KeyCode backward = KeyCode.DownArrow;
    [SerializeField]
    public KeyCode run = KeyCode.LeftShift;
    [SerializeField]
    public KeyCode right = KeyCode.RightArrow;
    [SerializeField]
    public KeyCode left = KeyCode.LeftArrow;
    [SerializeField]
    public KeyCode pickup = KeyCode.R;
    [SerializeField]
    public KeyCode swat = KeyCode.Q;
    [SerializeField]
    public KeyCode magic = KeyCode.E;

    [Header("PlayerControllerScript")]
    [SerializeField]
    MonoBehaviour target;


    IPlayerControls playerCharacter;

    void Start()
    { 
            playerCharacter = target as IPlayerControls;

    }


    void Update()
    {
        if (Input.GetKey(forward)) playerCharacter.Forward();
        if (Input.GetKey(backward)) playerCharacter.Backward();
        if (Input.GetKeyDown(run) && Input.GetKey(forward)) 
        { 
            playerCharacter.Run();
            playerCharacter.isRunning = true;
        }
        if (Input.GetKeyUp(run))
        {
            playerCharacter.isRunning = false;
        }

        if (Input.GetKey(right)) playerCharacter.TurnRight();
        if (Input.GetKey(left)) playerCharacter.TurnLeft();
        if (Input.GetKeyDown(pickup)) playerCharacter.PickUp();
        if (Input.GetKeyDown(swat)) playerCharacter.Swat();
        if (Input.GetKeyDown(magic)) playerCharacter.Magic();
    }

    [ContextMenu("Keyboard Controls - WASD")]
    public void SetKeysToWASD()
    {
        forward = KeyCode.W;
        backward = KeyCode.S;
        right = KeyCode.D;
        left = KeyCode.A;
        pickup = KeyCode.R;
    }

    [ContextMenu("Keyboard Controls - Arrows")]
    public void SetKeysToArrows()
    {
        forward = KeyCode.UpArrow;
        backward = KeyCode.DownArrow;
        right = KeyCode.RightArrow;
        left = KeyCode.LeftArrow;
    }
}
