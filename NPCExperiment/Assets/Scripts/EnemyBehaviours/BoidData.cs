using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    [CreateAssetMenu(fileName = "newBasicBoid", menuName = "Boid")]
    public class BoidData : ScriptableObject
    {
    public string prefabName;


    public new string name = "Boid";
        [Tooltip("Normal Speed")]
        public float speed = 1f;
        public float maxSpeed = 1f;
        [Tooltip("Speed when player detected")]
        public float attackSpeed = 8f;
        public float health = 20f;
        [Tooltip("Health that boid will return to base")]
        public float minHealth = 5f;
        [Tooltip("How far will the boid follow the player")]
        public float maxTravelDistance = 20f;
        public float detectionRange = 0.3f;
        public Material material;
        public GameObject boidPrefab;
        public GameObject boidBase;
    }