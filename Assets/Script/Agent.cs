using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Threading;

public class Agent : MonoBehaviour
{

    PlayerInteraction playerInteraction;
    private Vector2 pointerInput, movementInput;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    public AgentMover agentMover;

    private WeaponParent weaponParent;

    SpriteRenderer spriteRenderer;

    Rigidbody2D rb;

    float HAxis;
    float ZAxis;

    Health health;

    LookingAtRecipeBook lookingAtRecipeBook;

    Animator animator;

    TrailRenderer tr;

    // Start is called before the first frame update
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        agentMover = GetComponent<AgentMover>();
     
        weaponParent = GetComponentInChildren<WeaponParent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator= GetComponent<Animator>();
        health = GetComponent<Health>();
        playerInteraction = GetComponent<PlayerInteraction>();
        lookingAtRecipeBook = GetComponent<LookingAtRecipeBook>();
    }

    // Update is called once per frame
    void Update()
    {   
        ZAxis = Input.GetAxis("Vertical");
        // movementInput = movement.action.ReadValue<Vector2>().normalized; 
        // pointerInput = GetPointerInput();
        weaponParent.PointerPosition = pointerInput;
       
        agentMover.MovementInput = movementInput;
        facing();
        Animation();
    }

    
    public void PerformAttack()
    {
        weaponParent.Attack();
    }

    public void PerformDash()
    {
        agentMover.Dash();
    }

    public void PerformInteract()
    {
        playerInteraction.Update();
    }

    public void PerformRecipeBook()
    {
        lookingAtRecipeBook.Update();
    }
    

    
    public void facing()
    {
        // is player going left scale = -1
        HAxis = Input.GetAxis("Horizontal");
        if(HAxis < 0 && health.isDead == false)
        {
            spriteRenderer.flipX = enabled;        
        }
        else if(HAxis > 0 && health.isDead == false)
        {
            spriteRenderer.flipX = false;     
        }
        // is player doing right scale = 1
    }
    
    void Animation()
    {
        HAxis = Input.GetAxis("Horizontal");
        ZAxis = Input.GetAxis("Vertical");
        animator.SetFloat("RightLeft", Mathf.Abs(HAxis));
        animator.SetFloat("UpDown",Mathf.Abs(ZAxis)); 
    }
 



 
}
