using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAgressiveBoid", menuName = "AgressiveBoid")]

public class BoidAgressive : ScriptableObject
{
    public float speed = 8f;
    public float detectionRange = 1f;
}
