
using FiniteStateMachineLibrary.Managers;
using ComportamientoIA.Runtime.Managers;
using UnityEngine;

namespace ComportamientoIA.Runtime.State
{
    public abstract class EnemyState : FiniteStateMachineLibrary.States.State
    {
        protected EnemyBehaviour _controller;

        protected EnemyState(FiniteStateMachine finiteStateMachine, EnemyBehaviour controller) : base(finiteStateMachine)
        {
            this._controller = controller;
        }

        public bool IsSeeingPlayer()
        {
            if (Vector3.Distance(_controller.transform.position, _controller.player.position) > _controller.visionCone.VisionRange)
                return false;

            Vector3 directionToPlayer = (_controller.player.position - _controller.transform.position).normalized;
            double angleBetweenEnemyPlayer = Vector3.Angle(_controller.transform.forward, directionToPlayer);

            if (!(angleBetweenEnemyPlayer < (_controller.visionCone.VisionAngle * 0.5)))
                return false;

            return Physics.Raycast(_controller.transform.position, directionToPlayer, _controller.visionCone.VisionRange, _controller.visionCone.VisionObstructingLayer); //no está funcionando la layerMask
        }

        public bool IsHearingPlayer()
        {
            if (_controller.isHearingPlayer)
                return true;
            else
                return false;
        }
    }

}
