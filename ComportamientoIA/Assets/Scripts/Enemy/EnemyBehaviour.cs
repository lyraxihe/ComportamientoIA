
using FiniteStateMachineLibrary.Managers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

namespace ComportamientoIA.Runtime.Managers
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public Transform    player;
        public NavMeshAgent agent;
        public Vector3      lastPositionPlayer;

        [SerializeField] GameObject[] waypoints;
        [SerializeField] GameObject   coneVision;

        private FiniteStateMachine _finiteStateMachine;
        private int                currentWaypoint = 0;
        private float              originalSpeed;

        //State
        public int state; // 0 - Patrol | 1 - Chase | 2 - Seek

        void Start()
        {
            this._finiteStateMachine = new EnemyFiniteStateMachine(this);

            player = GameObject.Find("Player").GetComponent<Transform>();
            agent  = GetComponent<NavMeshAgent>();

            agent.destination = waypoints[currentWaypoint].transform.position;
            state             = 0;
            originalSpeed     = agent.speed;
        }

        private void UpdateMachine()
        {
            this._finiteStateMachine.UpdateMachine();
        }

        public void Patrol()
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                currentWaypoint++;

                if (currentWaypoint >= waypoints.Length)
                    currentWaypoint = 0;

                agent.destination = waypoints[currentWaypoint].transform.position;
            }
        }

        public void Chase()
        {
            agent.destination = player.transform.position;
            
            if (agent.speed == originalSpeed)
                agent.speed += 4;
        }

        public void Seek()
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
}
