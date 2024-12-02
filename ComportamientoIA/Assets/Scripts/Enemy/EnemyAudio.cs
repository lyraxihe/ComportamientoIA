
using ComportamientoIA.Runtime.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] GameObject enemyVisionCone;

    private EnemyBehaviour enemyScript;

    private void Start()
    {
        enemyScript = GetComponentInParent<EnemyBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "PlayerSoundCollider" && enemyScript.state != 1)
        {
            VisionCone enemyVisionConeScript = enemyVisionCone.GetComponent<VisionCone>();

            enemyVisionCone.transform.GetComponent<MeshRenderer>().material.color = enemyVisionConeScript.SeekColor;
            
            enemyScript.state              = 2;
            enemyScript.lastPositionPlayer = other.transform.position;
        }
    }
}
