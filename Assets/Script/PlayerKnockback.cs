using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

public class PlayerKnockback : MonoBehaviour, IDataPersistence
{
    [SerializeField] private Rigidbody2D rb;
    Health health;

    BegalHealth begalHealth;
    Animator animator;

    [SerializeField] private float strength =16 ,delay = 0.15f;

    public UnityEvent OnBegin,OnDone;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        health=GetComponent<Health>();
         begalHealth= GameObject.FindGameObjectWithTag("Enemy").GetComponent<BegalHealth>(); 
         begalHealth= GameObject.FindGameObjectWithTag("Enemy2").GetComponent<BegalHealth>(); 
    }
    
    // connect to GameData and Data Persistence for save the player's last position 
    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(GameData data)
    {
        if (this != null && health.currentHealth != 0)
        {
            data.playerPosition = this.transform.position;
        }
        else
        {
            Debug.LogWarning("PlayerKnockback object is null.");
        }
    }

    public void PlayFeedback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        animator.SetBool("GettingHit",true);
        UnityEngine.Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction*strength,ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new  WaitForSeconds(delay);
        rb.velocity = UnityEngine.Vector3.zero;
        animator.SetBool("GettingHit",false);
        OnDone?.Invoke();

    }

    private void OnTriggerEnter2D(Collider2D sender)
    {
        float push=10;
        if(sender.tag == "Enemy" && health.isDead == false && begalHealth.isDead == false )
        {
            StopAllCoroutines();
            OnBegin?.Invoke();
            animator.SetBool("GettingHit",true);
        
            UnityEngine.Vector2 direction = (transform.position - sender.transform.position).normalized;
            rb.AddForce(direction*push,ForceMode2D.Impulse);
            StartCoroutine(Reset());
        }

        if(sender.tag == "Enemy2" && health.isDead == false && begalHealth.isDead == false )
        {
            StopAllCoroutines();
            OnBegin?.Invoke();
            animator.SetBool("GettingHit",true);
        
            UnityEngine.Vector2 direction = (transform.position - sender.transform.position).normalized;
            rb.AddForce(direction*push,ForceMode2D.Impulse);
            StartCoroutine(Reset());
        }
    }
    
}