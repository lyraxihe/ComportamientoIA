
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
        //public bool               isHearingPlayer;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Start()
        {
            this._finiteStateMachine = new EnemyFiniteStateMachine(this);
            player                   = GameObject.Find("Player").GetComponent<Transform>();
            originalSpeed            = agent.speed;
            
            InvokeRepeating(nameof(UpdateMachine), 0, 0.2f);
        }

        private void UpdateMachine()
        {
            this._finiteStateMachine.UpdateMachine();
        }

        public void ChangeStateTo(FiniteStateMachineLibrary.States.State state)
        {
            this._finiteStateMachine.ChangeToState(state);
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    isHearingPlayer    = (other.transform.tag == "PlayerSoundCollider") ? true : false;
        //    if (isHearingPlayer)
        //    {
        //        lastPositionPlayer = other.transform.position;
        //    }
        //}

    }
}
