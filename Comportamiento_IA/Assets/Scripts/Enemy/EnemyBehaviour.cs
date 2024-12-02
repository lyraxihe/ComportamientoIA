using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    private int currentWaypoint = 0;
    [SerializeField] GameObject[] waypoints;
    private float originalSpeed;
    public Vector3 lastPositionPlayer;
    [SerializeField] GameObject coneVision;

    //State
    public int state; // 0 - Patrol | 1 - Chase | 2 - Seek

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = waypoints[currentWaypoint].transform.position;
        state = 0;
        originalSpeed = agent.speed;
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
            case 2:
                Seek();
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
        if (agent.speed == originalSpeed)
            agent.speed += 4;
    }

    private void Seek()
    {
        agent.destination = lastPositionPlayer;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            coneVision.transform.GetComponent<MeshRenderer>().material.color = coneVision.GetComponent<VisionCone>().PatrolColor;
            agent.destination = waypoints[currentWaypoint].transform.position;
            if (agent.remainingDistance <= agent.stoppingDistance)
                state = 0;
        }

    }

}
