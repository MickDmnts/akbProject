using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Dodge : MonoBehaviour
{   
    

    
    private Animator animator;
    private CharacterController charController;
    private Player_Movement playerMovement;

    void Start()
    {
        animator = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
        playerMovement = GetComponent<Player_Movement>();
    }

   

}
