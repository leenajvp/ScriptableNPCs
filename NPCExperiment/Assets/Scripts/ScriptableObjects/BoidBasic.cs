using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBasicBoid", menuName = "BasicBoid")]
public class BoidBasic : ScriptableObject
{
    public new string name;
    public float speed = 1f;
    public float detectionRange = 0.3f;
    public float health = 20f;
    public Material material;
    public GameObject characterPrefab;
}
