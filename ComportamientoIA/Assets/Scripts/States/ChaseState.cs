
using FiniteStateMachineLibrary.Managers;
using ComportamientoIA.Runtime.Managers;
using UnityEngine;

namespace ComportamientoIA.Runtime.State
{
    public class ChaseState : EnemyState
    {
        public ChaseState(FiniteStateMachine finiteStateMachine, EnemyBehaviour controller) : base(finiteStateMachine, controller) { }

        public override void OnEnter()
        {
            if (this._controller.agent.speed == this._controller.originalSpeed)
                this._controller.agent.speed += 3;

            this._controller.visionCone.changeColorTo(this._controller.visionCone.ChaseColor);
            _controller.visionCone.ChaseStateRange();
        }

        public override void DoState()
        {
            _controller.agent.destination = _controller.player.transform.position;
            if (!IsSeeingPlayer())
            {
                _controller.timeToStopChasing -= 0.2f;
                if(_controller.timeToStopChasing <= 0)
                    _controller.ChangeStateTo(new SeekState(_controller._finiteStateMachine, _controller));
            }
        }
    }
}
