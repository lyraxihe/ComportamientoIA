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

    //State
    public int state; // 0 - Patrol | 1 - Chase | 2 - Seek

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = waypoints[currentWaypoint].transform.position;
        state = 0;
    }

    void Update()
    {

        switch(state)
        {
            case 0:
                Patrol();
                break;
            case 1:
                Chase();
                break;
        }

    }

    private void Patrol()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;

            agent.destination = waypoints[currentWaypoint].transform.position;
        }
    }

    private void Chase()
    {
        agent.destination = player.transform.position;
    }

}
