using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoid : MonoBehaviour
{
    public float min = 2f;
    public float max = 3f;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        min = transform.position.x;
        max = transform.position.x + 3;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * speed, max - min) + min, transform.position.y, transform.position.z);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
