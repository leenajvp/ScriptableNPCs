using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeyboardControls), typeof(Animator))]

public class PlayerAnimations : MonoBehaviour
{
    private Animator animState;
    private KeyboardControls playerControls;

    void Awake()
    {
        animState = GetComponent<Animator>();
        playerControls = GetComponent<KeyboardControls>();
    }

    void Update()
    {
        // Animations: Animstate 1 = walk, 2 = run, 3 = walkbw, 4 = runbw, Bool = Swat, Bool = Magic

        if (Input.GetKey(playerControls.forward))
        {
            animState.SetInteger("AnimState", 1);

            if (Input.GetKey(playerControls.run))
            {
                animState.SetInteger("AnimState", 2);
            }
        }

        if (Input.GetKey(playerControls.backward))
        {
            animState.SetInteger("AnimState", 3);

            if (Input.GetKey(playerControls.run))
            {
                animState.SetInteger("AnimState", 4);
            }
        }

        if (!Input.anyKey)
        {
            animState.SetInteger("AnimState", 0);
        }

        if (Input.GetKeyDown(playerControls.swat))
        {
            animState.SetBool("Swat", true);
        }

        if (Input.GetKeyUp(playerControls.swat))
        {
            animState.SetBool("Swat", false);
        }

        if (Input.GetKey(playerControls.magic))
        {
            animState.SetBool("Magic", true);
        }

        if (Input.GetKeyUp(playerControls.magic))
        {
            animState.SetBool("Magic", false);
        }
    }
}
