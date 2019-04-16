using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private NavMeshAgent[] navAgents;
    public GameObject player;

    private void Start ()
    {
        navAgents = FindObjectsOfType(typeof(NavMeshAgent)) as NavMeshAgent[];
        
    }

    private void Update ()
    {
        foreach (NavMeshAgent agent in navAgents)
        {
            agent.destination = player.transform.position;
        }
    }
}
