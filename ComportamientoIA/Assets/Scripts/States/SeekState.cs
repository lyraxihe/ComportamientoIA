
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
            this._controller.agent.destination = this._controller.player.transform.position;
            this._controller.visionCone.changeColorTo(this._controller.visionCone.SeekColor);
            _controller.visionCone.ChaseSeekStateRange();
            this._controller.agent.speed = _controller.originalSpeed;
        }

        public override void DoState()
        {
            if ((_controller.agent.remainingDistance <= _controller.agent.stoppingDistance))
                _controller.ChangeStateTo(new PatrolState(this._finiteStateMachine, _controller));
            else if (IsSeeingPlayer())
                _controller.ChangeStateTo(new ChaseState(_controller._finiteStateMachine, _controller));
        }

    }
}
