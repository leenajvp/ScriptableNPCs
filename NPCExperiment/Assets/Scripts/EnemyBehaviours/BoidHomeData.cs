using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "newBoidBaseLocation", menuName = "BoidBase")]

public class BoidHomeData : ScriptableObject
{
    public enum BoidBaseMovement
    {
        None,
        BackAndForth,
        Cicular
    }

    public BoidBaseMovement baseMovement;

    public float speed;
    public float min = 2f;
    public float max = 3f;
    [Tooltip("Max rotation speed will provide smaller ratio")]
    [Range(10,50)]
    public float rotationSpeed = 30f;
    public GameObject objectToSpawn;
    public BoidData boidData;
    private GameObject target;
    public int numberOfPrefabsToCreate = 100;

    public void SetMovement(GameObject obj)
    {
        if (baseMovement == BoidBaseMovement.BackAndForth)
        {
            obj.transform.position = new Vector3(Mathf.PingPong(Time.time * speed, max - min) + min, obj.transform.position.y, obj.transform.position.z);
        }

        if (baseMovement == BoidBaseMovement.Cicular)
        {
            if (target == null)
            {
                GameObject newTarget = new GameObject("Target");
                newTarget.transform.position = obj.transform.position;
                target = newTarget; 
            }

            obj.transform.position += obj.transform.forward * speed * Time.deltaTime;
            obj.transform.rotation = Quaternion.RotateTowards(obj.transform.rotation, Quaternion.LookRotation(target.transform.position - obj.transform.position), rotationSpeed * Time.deltaTime);
        }
    }
}
