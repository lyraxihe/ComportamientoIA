using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 7f; // Velocidad de movimiento del personaje
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;

    void Start()
    {
        // Obtener el componente Rigidbody del personaje
        rb = GetComponent<Rigidbody>();
        // Impide que el personaje gire y se pueda caer
        rb.freezeRotation = true;
    }

    void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
        rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
    }

}
