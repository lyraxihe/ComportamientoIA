
using FiniteStateMachineLibrary.Managers;
using ComportamientoIA.Runtime.Managers;
using UnityEngine;

namespace ComportamientoIA.Runtime.State
{
    public class PatrolState : EnemyState
    {
        public PatrolState(FiniteStateMachine finiteStateMachine, EnemyBehaviour controller) : base(finiteStateMachine, controller) { }

        public override void OnEnter()
        {
            this._controller.agent.destination = this._controller.waypoints[this._controller.currentWaypoint].transform.position;
            this._controller.visionCone.changeColorTo(this._controller.visionCone.PatrolColor);
        }

        public override void DoState()
        {
            this._controller.Patrol();
        }
    }
}
