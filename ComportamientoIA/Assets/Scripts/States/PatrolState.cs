﻿
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
            if (IsSeeingPlayer())
                _controller.ChangeStateTo(new ChaseState(_controller._finiteStateMachine, _controller));

            //if (IsHearingPlayer())
            //    _controller.ChangeStateTo(new SeekState(_controller._finiteStateMachine, _controller));

        }

        public bool IsSeeingPlayer()
        {
            if (Vector3.Distance(_controller.transform.position, _controller.player.position) > _controller.visionCone.VisionRange)
                return false;

            Vector3 directionToPlayer       = (_controller.player.position - _controller.transform.position).normalized;
            double  angleBetweenEnemyPlayer = Vector3.Angle(_controller.transform.forward, directionToPlayer);

            if (!(angleBetweenEnemyPlayer < (_controller.visionCone.VisionAngle * 0.5)))
                return false;

            return Physics.Raycast(_controller.transform.position, directionToPlayer, _controller.visionCone.VisionRange, _controller.visionCone.VisionObstructingLayer); //no está funcionando la layerMask
        }
        
        //public bool IsHearingPlayer()
        //{
        //    if (_controller.isHearingPlayer)
        //        return true;
        //    else 
        //        return false;
        //}
    }
}
