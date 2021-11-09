using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBasicBoid", menuName = "Boid")]
public class BoidData : ScriptableObject
{
    public new string name = "Boid";
    public float speed = 1f;
    public float attackSpeed = 8f;
    public float health = 20f;
    public float detectionRange = 0.3f;
    public float attackDetectionRange = 1f;
    public Material material;
    public GameObject characterPrefab;
    public GameObject target;
}
