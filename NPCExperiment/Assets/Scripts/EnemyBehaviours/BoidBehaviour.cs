using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehaviour : MonoBehaviour, IBoid
{
    public BoidData basicBehaviour;

    private float boidDetection;
    private float boidSpeed;
    public float boidHealth;

    private Collider[] colliders;
    private GameObject player = null;
    private GameObject boidBase;
    private GameObject target;
    private Vector3 targetPos;

    List<GameObject> nearbyBoids = new List<GameObject>(10);
    new Renderer renderer;
    private bool isHealing = false;


    void Start()
    {
        boidSpeed = basicBehaviour.speed;
        boidDetection = basicBehaviour.detectionRange;
        boidHealth = basicBehaviour.health;
        boidBase = basicBehaviour.target;
        target = boidBase;
        renderer = GetComponent<Renderer>();
        renderer.material = basicBehaviour.material;
        isHealing = false;
    }

    void Update()
    {
        Observe(); 
        AlignWithBoids();       
        TurnTo(targetPos); 
        AvoidBoids();  
        Move();
        Heal();

        
        targetPos = target.transform.position;
        TurnTo(targetPos);
    }

    void Move()
    {
        transform.position += transform.up * boidSpeed * Time.deltaTime;
    }

    void Observe()
    {
        nearbyBoids.Clear();

        colliders = Physics.OverlapSphere(transform.position, boidDetection);

        foreach (var col in colliders)
        {
            IBoid followBoids = col.gameObject.GetComponent<IBoid>();

            if (followBoids != null)
            {
                nearbyBoids.Add(col.gameObject);
            }

            PlayerMovement playerDetected = col.gameObject.GetComponent<PlayerMovement>();

            if (playerDetected != null)
            {
                player = playerDetected.gameObject;
                target = player.gameObject;
                boidSpeed = basicBehaviour.attackSpeed;
                boidDetection = basicBehaviour.attackDetectionRange;

                var distanceToBase = Vector3.Distance(transform.position, boidBase.transform.position);

                if (distanceToBase >= 20 || boidHealth < 5)
                {
                    target = boidBase;
                    boidSpeed = basicBehaviour.speed;
                    boidDetection = basicBehaviour.detectionRange;
                    isHealing = true;
                }
            }
        }
    } 

    void AlignWithBoids()
    {
        if (nearbyBoids.Count > 0)
        {
            for (int i = 0; i < nearbyBoids.Count; ++i)
            {
                float angleDiff = Quaternion.Angle(transform.rotation, nearbyBoids[i].transform.rotation) * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, nearbyBoids[i].transform.rotation, angleDiff);
            }
        }
    }

    void AvoidBoids()
    {
        if (nearbyBoids.Count > 0)
        {
            for (int i = 0; i < nearbyBoids.Count; ++i)
            {
                Vector3 toNeighbour = nearbyBoids[i].transform.position - transform.position;

                TurnTo(transform.position - toNeighbour);
            }
        }
    }

    void TurnTo(Vector3 turnTarget)
    {
        Quaternion lookRot = Quaternion.LookRotation(transform.forward, transform.up);
        Quaternion fromTo = Quaternion.FromToRotation(transform.up, (turnTarget - transform.position));

        Quaternion centerRot = fromTo * lookRot;

        float angleDiff = Quaternion.Angle(centerRot, transform.rotation) * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, centerRot, angleDiff);
    }

    private void Heal()
    {
        if (isHealing == true)
        {
            boidHealth += 1 * Time.deltaTime;

            if(boidHealth >= 20)
            {
                isHealing = false;
            }
        }
    }
}
