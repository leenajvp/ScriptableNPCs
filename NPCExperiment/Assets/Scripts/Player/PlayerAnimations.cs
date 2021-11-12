using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeyboardControls), typeof(Animator), typeof(PlayerMovement))]

public class PlayerAnimations : MonoBehaviour
{
    private PlayerMovement playerMove;
    private Animator animState;
    private KeyboardControls playerControls;
    public bool isSwatting=false;

    void Awake()
    {
        animState = GetComponent<Animator>();
        playerControls = GetComponent<KeyboardControls>();
        playerMove = GetComponent<PlayerMovement>();
    }


    void Update()
    {
        // Animations: Animstate 1 = walk, 2 = run, 3 = walkbw, 4 = runbw, Bool = Swat, Bool = Magic

        if (!Input.anyKey)
        {
            animState.SetInteger("AnimState", 0);
        }

        MoveForwardAnimation();
        MoveBackwardsdAnimation();
        MagicAnimation();
        SwatAnimation();
    }

    public void MoveForwardAnimation()
    {
        if (Input.GetKey(playerControls.forward))
        {
            animState.SetInteger("AnimState", 1);

            if (Input.GetKey(playerControls.run))
            {
                animState.SetInteger("AnimState", 2);
            }
        }
    }

    public void MoveBackwardsdAnimation()
    {
        if (Input.GetKey(playerControls.backward))
        {
            animState.SetInteger("AnimState", 3);

            if (Input.GetKey(playerControls.run))
            {
                animState.SetInteger("AnimState", 4);
            }
        }
    }

    public void SwatAnimation()
    {
        if (isSwatting)
        {
           // animState.GetCurrentAnimatorStateInfo(0).IsName("Swat");
            animState.SetBool("Swat", true);
            //StartCoroutine(SwatTimer());
        }

        else 
        {
            animState.SetBool("Swat", false);
        }

        if (Input.GetKeyDown(playerControls.swat))
        {
            animState.SetBool("Swat", true);
        }

        if (Input.GetKeyUp(playerControls.swat))
        {
            animState.SetBool("Swat", false);
        }

        var checkAnimFinished = animState.GetCurrentAnimatorStateInfo(0).IsName("Swat");

        if (checkAnimFinished)
        {
            playerMove.isDisabled = true;
                
        }

        if (!checkAnimFinished)
        {
            isSwatting = false;
            playerMove.isDisabled = false;
        }
    }

    private IEnumerator SwatTimer()
    {
        yield return new WaitForSeconds(3);
        isSwatting = false;
    }


    public void MagicAnimation()
    {
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
