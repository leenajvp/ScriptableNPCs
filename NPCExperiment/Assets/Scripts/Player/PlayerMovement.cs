using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    [SerializeField]
    float rotationSpeed = 10;

    private Animator animState;

    void Start()
    {
        animState = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Forward();
            animState.SetBool("isMoving", true);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            animState.SetBool("isMoving", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Backward();
            animState.SetBool("isMoving", true);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            animState.SetBool("isMoving", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            TurnLeft();

        }

        if (Input.GetKey(KeyCode.D))
        {
            TurnRight();
        }
    }

    private void Forward()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

    }

    private void Backward()
    {
        transform.position -= transform.forward * speed * Time.deltaTime;

    }

    private void TurnLeft()
    {
        transform.Rotate(0.0f, -rotationSpeed * Time.deltaTime, 0.0f);
    }

    private void TurnRight()
    {
        transform.Rotate(0.0f, rotationSpeed * Time.deltaTime, 0.0f);
    }
}
