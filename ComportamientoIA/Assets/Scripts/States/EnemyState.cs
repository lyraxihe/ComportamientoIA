
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

            var directionToPlayer       = (_controller.player.position - _controller.transform.position).normalized;
            var angleBetweenEnemyPlayer = Vector3.Angle(_controller.transform.forward, directionToPlayer);

            if (angleBetweenEnemyPlayer > (_controller.visionCone.VisionAngle * 0.5))
                return false;
            
           if (Physics.Raycast(_controller.transform.position, directionToPlayer, out RaycastHit hit, _controller.visionCone.VisionRange, LayerMask.GetMask("Wall", "Player")))
           {
                var hitedObject = hit.collider.gameObject;
                
                if (hitedObject.tag == "Player")
                    return true;
                else
                    return false;
           }

           return false;
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
