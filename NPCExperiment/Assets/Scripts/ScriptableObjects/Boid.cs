using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBoid", menuName = "Boid")]
public class Boid : ScriptableObject
{
    public int damage;
    public int detectionRange;
    public bool isFollower;
    public Transform followNPC;
}
