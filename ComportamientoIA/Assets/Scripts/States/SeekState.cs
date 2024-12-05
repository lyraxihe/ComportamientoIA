
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
            if (_controller.agent.remainingDistance <= _controller.agent.stoppingDistance)
                if (_controller.agent.remainingDistance <= _controller.agent.stoppingDistance)
                    _controller.ChangeStateTo(new PatrolState(this._finiteStateMachine, _controller));

            //if ((_controller.agent.remainingDistance <= _controller.agent.stoppingDistance) && !IsSeeingPlayer())
            //    _controller.ChangeStateTo(new PatrolState(this._finiteStateMachine, _controller));
            //else if (IsSeeingPlayer())
            //_controller.ChangeStateTo(new ChaseState(_controller._finiteStateMachine, _controller));

        }

    }
}
