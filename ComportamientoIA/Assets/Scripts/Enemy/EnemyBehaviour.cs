
using ComportamientoIA.Runtime.State;
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
        public Transform          player;
        public NavMeshAgent       agent;
        public Vector3            lastPositionPlayer;
        public FiniteStateMachine _finiteStateMachine;
        public VisionCone         visionCone;
        public GameObject[]       waypoints;
        public int                currentWaypoint = 0;
        public float              originalSpeed;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Start()
        {
            this._finiteStateMachine = new EnemyFiniteStateMachine(this);
            player                   = GameObject.Find("Player").GetComponent<Transform>();
            originalSpeed            = agent.speed;
        }

        private void Update()
        {
            UpdateMachine();
        }

        private void UpdateMachine()
        {
            this._finiteStateMachine.UpdateMachine();
        }

        public void ChangeStateTo(FiniteStateMachineLibrary.States.State state)
        {
            this._finiteStateMachine.ChangeToState(state);
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
        }

        public void Seek()
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
                if (agent.remainingDistance <= agent.stoppingDistance)
                    ChangeStateTo(new PatrolState(this._finiteStateMachine, this));
        }
    }
}
