
using FiniteStateMachineLibrary.Managers;
using ComportamientoIA.Runtime.Managers;
using UnityEngine;

namespace ComportamientoIA.Runtime.State
{
    public class SeekState : EnemyState
    {
        public SeekState(FiniteStateMachine finiteStateMachine, EnemyBehaviour controller) : base(finiteStateMachine, controller) { }

        public override void OnEnter()
        {
            this._controller.agent.destination = this._controller.lastPositionPlayer;
            this._controller.visionCone.changeColorTo(this._controller.visionCone.SeekColor);
        }

        public override void DoState()
        {
            this._controller.Seek();
        }
    }
}
