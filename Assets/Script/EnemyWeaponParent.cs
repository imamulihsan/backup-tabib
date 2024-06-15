using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Numerics;

public class EnemyWeaponParent : MonoBehaviour
{
    public UnityEngine.Vector2 PointerPosition{get;set;}
     public Transform circleOrigin;
    public float radius;

    public Animator animator;
    public float delay = 0.8f;

    public bool attackLocked;
healthAnimation healthAnimation;
   

    public SpriteRenderer characterRenderer, weaponRenderer;

    public void ResetIsAttacking(){
        isAttacking = false;
    }
    public bool isAttacking { get; private set; }

    public void Awake()
    {
         healthAnimation = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<healthAnimation>();
    }

    private void Update(){

        if(isAttacking)
            return;
        
        UnityEngine.Vector2 direction = (PointerPosition-(UnityEngine.Vector2)transform.position).normalized;
        transform.right =direction;

        UnityEngine.Vector2 scale = transform.localScale;
        if(direction.x <0 ){
                scale.y = -1;
        } else if(direction.x > 0){
            scale.y=1;
        }

        transform.localScale = scale;
        
        if (transform.eulerAngles.z>0 &&transform.eulerAngles.z <180 ){
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder +1;

        }else{
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder +1;
        }
        
    }

    public void Attack(){
        if(attackLocked)
        return;
        animator.SetTrigger("Attack");
        isAttacking = true;
        attackLocked = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack(){
        yield return new WaitForSeconds(delay);
        attackLocked = false;
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.blue;
        UnityEngine.Vector3 position = circleOrigin == null ? UnityEngine.Vector3.zero : circleOrigin.position; 
        Gizmos.DrawWireSphere(position,radius);
    }

    public void DetectColliders(){
        foreach(Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position,radius)){
            Health health;
            if(health = collider.GetComponent<Health>()){
                health.GetHit(1,transform.parent.gameObject);
                
            }
        }

        
    }

}
