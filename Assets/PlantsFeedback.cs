using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

public class PlantsFeedback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    Animator animator;
    BegalHealth begalHealth;
    PlantsFeedback plantsFeedback;
    PlantsHealth plantsHealth;

    [SerializeField] private float strength =16 ,delay = 0.15f;

    public UnityEvent OnBegin,OnDone;

    public void Awake(){
        animator = GetComponent<Animator>();
        begalHealth=GetComponent<BegalHealth>();
        plantsHealth = GetComponent<PlantsHealth>();
    }

    public void PlayFeedback(GameObject sender){
        StopAllCoroutines();
        
        animator.SetBool("GettingHit",true);
        
        UnityEngine.Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction*strength,ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset(){
        yield return new  WaitForSeconds(delay);
        animator.SetBool("GettingHit",false);
       

    }

    private void OnTriggerEnter2D(Collider2D sender){
       
        if(sender.tag == "Player"){
             StopAllCoroutines();
      
 
        StartCoroutine(Reset());
        }
    }
}
