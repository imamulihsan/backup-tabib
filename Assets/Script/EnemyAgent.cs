using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Threading;

public class EnemyAgent : MonoBehaviour
{

 public int ID{get;set;}
private Vector2 pointerInput, movementInput;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    public AgentMover agentMover;

private EnemyWeaponParent enemyWeaponParent;

 SpriteRenderer spriteRenderer;

 Rigidbody2D rb;

float HAxis;
float ZAxis;

EnemyArea enemyArea;


Animator animator;



TrailRenderer tr;



    // Start is called before the first frame update
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        agentMover = GetComponent<AgentMover>();
        facing();
        enemyWeaponParent = GetComponentInChildren<EnemyWeaponParent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator= GetComponent<Animator>();
        ID = 0;
        enemyArea = GameObject.FindGameObjectWithTag("EnemyThreshold").GetComponent<EnemyArea>();
    }

    // Update is called once per frame
    void Update()
    {
        
       
        ZAxis = Input.GetAxis("Vertical");
        // movementInput = movement.action.ReadValue<Vector2>().normalized; 
        // pointerInput = GetPointerInput();
        enemyWeaponParent.PointerPosition = pointerInput;
       animator = GetComponent<Animator>();

       agentMover.MovementInput = movementInput;
     
   
     


    }

    
public void PerformAttack(){
        enemyWeaponParent.Attack();
    }

    
void facing(){
        // is player going left scale = -1
        
        if(transform.localScale.x < 0 ){
            spriteRenderer.flipX = enabled;
            
           
        } else if(HAxis > 0 ){
            spriteRenderer.flipX = false;
            
            
            
        }
        // is player doing right scale = 1
    }


    
  
 



 
}
