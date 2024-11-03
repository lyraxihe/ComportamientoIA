using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float originalSpeed = 7f;
    public float sprintSpeed = 5f;
    public float currentSpeed;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    [SerializeField] GameObject playerSound;

    void Start()
    {
        // Obtener el componente Rigidbody del personaje
        rb = GetComponent<Rigidbody>();
        // Impide que el personaje gire y se pueda caer
        rb.freezeRotation = true;

        currentSpeed = originalSpeed;
    }

    void Update()
    {
        MyInput();
        Sprint();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput == 0f && verticalInput == 0)
            playerSound.GetComponent<SphereCollider>().gameObject.SetActive(false);
        else
            playerSound.GetComponent<SphereCollider>().gameObject.SetActive(true);
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
        rb.AddForce(moveDirection.normalized * currentSpeed * 10f, ForceMode.Force);
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = originalSpeed + sprintSpeed;
            playerSound.GetComponent<SphereCollider>().radius = 7f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = originalSpeed;
            playerSound.GetComponent<SphereCollider>().radius = 1f;
        }
    }

}
