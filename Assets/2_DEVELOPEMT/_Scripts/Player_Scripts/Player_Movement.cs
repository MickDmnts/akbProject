using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    private Animator animator;
    private CharacterController charController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();

        charController.SimpleMove(movementDirection * magnitude);

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("IsRunning", true);
            Quaternion toRotate = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }
}
