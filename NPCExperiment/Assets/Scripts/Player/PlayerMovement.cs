using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerAnimations), typeof(KeyboardControls))]
public class PlayerMovement : MonoBehaviour, IPlayerControls
{
    public float speed = 2f;
    [SerializeField]
    float walkingSpeed = 2f;
    [SerializeField]
    float runningSpeed = 5f;
    [SerializeField]
    float rotationSpeed = 10f;

    float lastReation = 0f;

    List<GameObject> boidsDetected = new List<GameObject>();

    public bool isRunning { get; set; }
    public bool isDisabled = false;
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
        if (!isDisabled)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    public void Backward()
    {
        if (!isDisabled)
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
    }


    public void Run()
    {
        if (!isDisabled)
        {
            if (Input.GetKeyDown(playerControls.run))
            {
                isRunning = true;
            }
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
        if (Time.time < lastReation + 5.0f)
            return;

        lastReation = Time.time;

        PlayerAnimations swatAnim = GetComponent<PlayerAnimations>();
        swatAnim.isSwatting = true;
        speed = walkingSpeed;
    }



    public void Magic()
    {
        if (!isDisabled)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 15);

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
}
