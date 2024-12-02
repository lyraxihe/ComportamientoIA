
using FiniteStateMachineLibrary.Managers;
using ComportamientoIA.Runtime.Managers;
using UnityEngine;

namespace ComportamientoIA.Runtime.State
{
    public class ChaseState : EnemyState
    {
        public ChaseState(FiniteStateMachine finiteStateMachine, EnemyBehaviour controller) : base(finiteStateMachine, controller) { }

        public override void DoState()
        {
            this._controller.Chase();
        }
    }
}
