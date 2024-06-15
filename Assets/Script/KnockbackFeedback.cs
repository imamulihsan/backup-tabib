using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

public class KnockbackFeedback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    Animator animator;
    BegalHealth begalHealth;

    [SerializeField] private float strength =16 ,delay = 0.15f;

    public UnityEvent OnBegin,OnDone;

    public void Awake(){
        animator = GetComponent<Animator>();
        begalHealth=GetComponent<BegalHealth>();
    }

    public void PlayFeedback(GameObject sender){
        StopAllCoroutines();
        OnBegin?.Invoke();
        animator.SetBool("GettingHit",true);
        
        UnityEngine.Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction*strength,ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset(){
        yield return new  WaitForSeconds(delay);
        rb.velocity = UnityEngine.Vector3.zero;
        animator.SetBool("GettingHit",false);
        OnDone?.Invoke();

    }

    private void OnTriggerEnter2D(Collider2D sender){
        float push=5;
        if(sender.tag == "Player" && begalHealth.currentHealth >0 ){
             StopAllCoroutines();
        OnBegin?.Invoke();
        animator.SetBool("GettingHit",true);
        
        UnityEngine.Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction*push,ForceMode2D.Impulse);
        StartCoroutine(Reset());
        }
    }
}
