
using ComportamientoIA.Runtime.Managers;
using ComportamientoIA.Runtime.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] GameObject enemyVisionCone;

    private EnemyBehaviour _controller;

    private void Start()
    {
        _controller = GetComponentInParent<EnemyBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "PlayerSoundCollider" && _controller._finiteStateMachine.getCurrentState() is not ChaseState)
        {
            _controller.lastPositionPlayer = other.transform.position;
            _controller.ChangeStateTo(new SeekState(_controller._finiteStateMachine, _controller));
        }
    }
}
