
using FiniteStateMachineLibrary.Managers;
using ComportamientoIA.Runtime.Managers;
using UnityEngine;
using UnityEngine.UIElements;

namespace ComportamientoIA.Runtime.State
{
    public class PatrolState : EnemyState
    {
        public PatrolState(FiniteStateMachine finiteStateMachine, EnemyBehaviour controller) : base(finiteStateMachine, controller) { }

        public override void OnEnter()
        {
            this._controller.agent.destination = this._controller.waypoints[this._controller.currentWaypoint].transform.position;
            
            this._controller.visionCone.changeColorTo(this._controller.visionCone.PatrolColor);
            _controller.visionCone.PatrolStateRange();
        }

        public override void DoState()
        {
            if (_controller.agent.remainingDistance <= _controller.agent.stoppingDistance)
            {
                _controller.currentWaypoint++;

                if (_controller.currentWaypoint >= _controller.waypoints.Length)
                    _controller.currentWaypoint = 0;

                _controller.agent.destination = _controller.waypoints[_controller.currentWaypoint].transform.position;
            }

            var hasSeenPlayer = IsSeeingPlayer();
            
            if (hasSeenPlayer)
                _controller.ChangeStateTo(new ChaseState(_controller._finiteStateMachine, _controller));
            else if (IsHearingPlayer() && !hasSeenPlayer)
                _controller.ChangeStateTo(new SeekState(_controller._finiteStateMachine, _controller));

        }


    }
}
