using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class PlayerMovement : MonoBehaviour, IPlayerControls
{
    [SerializeField]
    float speed = 2;
    [SerializeField]
    float walkingSpeed = 2;
    [SerializeField]
    float runningSpeed = 5;
    [SerializeField]
    float rotationSpeed = 10;

    List<GameObject> boidsDetected = new List<GameObject>();

    public bool isRunning { get; set; }
    private KeyboardControls playerControls;

    private Animator animState;

    void Start()
    {
        animState = GetComponent<Animator>();
        playerControls = GetComponent<KeyboardControls>();
    }

    void Update()
    {
        if (isRunning)
        {
            speed = runningSpeed;
        }

        else
        {
            speed = walkingSpeed;
        }
    }

    public void Forward()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void Backward()
    {
        transform.position -= transform.forward * speed * Time.deltaTime;
    }


    public void Run()
    {
        if (Input.GetKeyDown(playerControls.run))
        {
            isRunning = true;
        }

    }

    public void TurnLeft()
    {
        transform.Rotate(0.0f, -rotationSpeed * Time.deltaTime, 0.0f);
    }

    public void TurnRight()
    {
        transform.Rotate(0.0f, rotationSpeed * Time.deltaTime, 0.0f);
    }

    public void PickUp()
    {
       
    }

    public void Swat()
    {
        
    }

    public void Magic()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 8);

        foreach (var col in colliders)
        {
            BoidBehaviour followBoids = col.gameObject.GetComponent<BoidBehaviour>();

            if (followBoids != null)
            {
                boidsDetected.Add(col.gameObject);

                foreach (GameObject boid in boidsDetected)
                {
                    followBoids.boidHealth = 0;
                }
            }
        }
    }
}
