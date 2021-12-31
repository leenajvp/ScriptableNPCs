using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private bool playerDetected;
    private GameObject player;
    private EnemyBehaviour state;

    private void Awake()
    {
        state = transform.parent.GetComponent<EnemyBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            playerDetected = true;
            state.currentSate = EnemyBehaviour.EnemyStates.Suspicious;
            StartCoroutine(LocatePlayer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerDetected = false;
        }
    }

    private IEnumerator LocatePlayer()
    {
        yield return new WaitForSeconds(2);

        if (playerDetected)
        {
            state.currentSate = EnemyBehaviour.EnemyStates.FindPlayer;
        }

        else
        {
            state.currentSate = EnemyBehaviour.EnemyStates.Patrol;
        }
    }
}
