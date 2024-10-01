using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 100f; // Velocidad de movimiento del personaje
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el componente Rigidbody del personaje
    }

    void Update()
    {

        // Crear un vector de movimiento
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        // Mover al personaje
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
    }
}
