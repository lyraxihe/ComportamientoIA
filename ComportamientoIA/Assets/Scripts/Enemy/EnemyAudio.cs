
using ComportamientoIA.Runtime.Managers;
using ComportamientoIA.Runtime.State;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] GameObject enemyVisionCone;

    private EnemyBehaviour _controller;


    private void Start()
    {
        _controller = GetComponentInParent<EnemyBehaviour>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "PlayerSoundCollider")
        {
            _controller.lastPositionPlayer = other.transform.position;
            _controller.isHearingPlayer = true;
        }
                
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "PlayerSoundCollider")
        {
            _controller.isHearingPlayer = false;
        }
    }
}
