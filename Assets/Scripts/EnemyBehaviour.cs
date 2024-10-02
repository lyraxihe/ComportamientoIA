using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private int currentWaypoint = 0;

    [SerializeField] GameObject[] waypoints;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = waypoints[currentWaypoint].transform.position;
    }

    void Update()
    {

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;

            agent.destination = waypoints[currentWaypoint].transform.position;  
        }
    }
}
