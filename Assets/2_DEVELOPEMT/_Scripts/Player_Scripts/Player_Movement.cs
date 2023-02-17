using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float cooldown;
    public float dodgeSpeed = 100;
    public static bool movementControls = true;
    
    private float lastDodge;
    private Animator animator;
    private CharacterController charController;
    private Rigidbody rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (movementControls == true)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            Dodge();
        }

    }

    IEnumerator Cooldown()
    {
        animator.SetBool("IsDodging", true);
        movementControls = false;
        //rb.AddForce(transform.movementDirection * dodgeSpeed);
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsDodging", false);
        movementControls = true;
    }

    public void Dodge()
    {
        if (Time.time - lastDodge < cooldown)
        {
            return;
        }
        lastDodge = Time.time;
        StartCoroutine(Cooldown());
    }


}
