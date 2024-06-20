using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class AgentMover : MonoBehaviour
{

    float HAxis,ZAxis;
    public Rigidbody2D rb;
    
    [SerializeField] private Transform thisGameObject;

    public float delay = 1.5f;
    [SerializeField]float dashPower = 2;
    public bool dashLocked = false;

    bool dashBlocked = false;
    public bool isDashing = true;
    Animator animator;
    WeaponParent weaponParent;

    TrailRenderer tr;
    float dashingTime = 0.2f;


    [SerializeField] public float maxSpeed = 2, acceleration = 50, deacceleration = 100;

    [SerializeField] private float currentSpeed=0;

    private Vector2 oldMovementInput;

    public Vector2 MovementInput {get;set;}

    private void Awake (){
        rb=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        weaponParent = GetComponent<WeaponParent>();
        tr= GetComponentInChildren<TrailRenderer>();
    }

  

    
    public void FixedUpdate()
    {   
        if( MovementInput.magnitude > 0 && currentSpeed == 0)
        {
            oldMovementInput=MovementInput;
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
            
            
        }
        else
        {
            currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed,0,maxSpeed);
        rb.velocity = oldMovementInput * currentSpeed;
  
    }

    public void Dash()
    {
        if( MovementInput.magnitude > 0 && currentSpeed == 0)
        {
            oldMovementInput=MovementInput;
            currentSpeed += acceleration * maxSpeed*Time.deltaTime;
        } else
        {
            currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed,0,maxSpeed);
        rb.velocity = oldMovementInput * currentSpeed;
  
        
        if(dashLocked)
            return;
        
        rb.MovePosition (rb.position + rb.velocity.normalized * dashPower );
        
        animator.SetTrigger("Dash");
        tr.emitting = true;
        isDashing = true;
        dashLocked = true;
        StartCoroutine(DelayDash());
    }

    private IEnumerator DelayDash()
    {
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        yield return new WaitForSeconds(delay);
        dashLocked = false;
        
    }

    
}