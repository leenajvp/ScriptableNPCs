using System.Collections.Generic;
using UnityEngine;

public class BoidBehaviour : MonoBehaviour, IBoid
{
    public BoidData boidData;

    private float boidDetection;
    public float boidSpeed;
    public float boidHealth;
    private float boidMinHealth;

    private Collider[] colliders;
    private GameObject player = null;
    private GameObject target = null;
    [SerializeField]
    private GameObject boidBase;
    private Vector3 startPos;
    private Vector3 targetPos;

    List<GameObject> nearbyBoids = new List<GameObject>(10);
    new Renderer renderer;
    private bool isHealing = false;


    void Start()
    {
        this.name = boidData.name;
        boidSpeed = boidData.speed;
        boidDetection = boidData.detectionRange;
        boidHealth = boidData.health;
        boidMinHealth = boidData.minHealth;
        renderer = GetComponent<Renderer>();
        renderer.material = boidData.material;
        isHealing = false;
        startPos = transform.position;

        if (transform.parent != null)
        {
            boidBase = transform.parent.gameObject;
            target = boidBase;
        }

        else
        {
            targetPos = startPos;
        }
    }

    void Update()
    {
        Observe();
        AlignWithBoids();
        TurnTo(targetPos);
        AvoidBoids();
        Move();
        Heal();

        if (target != null)
        {
            targetPos = target.transform.position;
        }

        else
        {
            targetPos = startPos;
        }
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
                transform.parent = null;
                player = playerDetected.gameObject;
                target = player;
                targetPos = new Vector3(player.transform.position.x, player.transform.position.y + 3, player.transform.position.z);
                boidSpeed = Random.Range(boidSpeed, boidData.attackSpeed);

                if (boidHealth > 5)
                {
                    playerDetected.Swat();
                }
            }

            var distanceToBase = Vector3.Distance(transform.position, boidBase.transform.position);
            float speedUp = 10;

            if (distanceToBase >= 20 || boidHealth < 5)
            {
                if (boidBase != null)
                {
                    boidSpeed = speedUp;
                    target = boidBase;
                }

                else
                {
                    target = null;
                    targetPos = startPos;
                }

                boidSpeed = boidData.speed;
                boidDetection = boidData.detectionRange;
                isHealing = true;
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

            if (boidHealth >= 20)
            {
                isHealing = false;
            }
        }
    }
}
