using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SpikerKnockback : MonoBehaviour, IDataPersistence
{
    [SerializeField] private Rigidbody2D rb;
    Health health;

    BegalHealth begalHealth;
    AgentMover agentMover;
    Animator animator;

    bool canBeHitted =true;

    [SerializeField] private float strength = 16, delay = 0.15f;

    public UnityEvent OnBegin, OnDone;
    public int spikeDamage = 1;

    PlayerInput playerInput;

    private void Awake()
    {
        animator = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Animator>(); 
        health = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Health>(); 
        begalHealth = GameObject.FindGameObjectWithTag("Enemy")?.GetComponent<BegalHealth>(); 
        begalHealth = GameObject.FindGameObjectWithTag("Enemy2")?.GetComponent<BegalHealth>(); 
        agentMover = GameObject.FindGameObjectWithTag("Player")?.GetComponent<AgentMover>(); 
        playerInput = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerInput>(); 
    }

    // Connect to GameData and Data Persistence for saving the player's last position 
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
            Debug.LogWarning("PlayerKnockback object is null or player is dead.");
        }
    }

    public void PlayFeedback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        animator.SetBool("GettingHit", true);
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    { 
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
        animator.SetBool("GettingHit", false);
        OnDone?.Invoke();
        if(health.currentHealth > 0 ){
        agentMover.acceleration = 50;
        agentMover.maxSpeed =10;
        }

        if(health.currentHealth <=0){
            agentMover.acceleration =0;
        }
    }

    private void OnTriggerEnter2D(Collider2D sender)
    {
        if (health.isDead) return;

        float push = 10;
        

        if (sender.CompareTag("Spike"))
        {
            ApplySpikeDamage(sender, push);
        }
    }

    private void ApplyKnockback(Collider2D sender, float push)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        animator.SetBool("GettingHit", true);
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction * push, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private void ApplySpikeDamage(Collider2D sender, float push)
    {
        if (health.currentHealth > 0 )
        {
            StopAllCoroutines();
            OnBegin?.Invoke();
            animator.SetBool("GettingHit", true);
            Vector2 direction = (transform.position - sender.transform.position).normalized;
            rb.AddForce(direction * push, ForceMode2D.Impulse);

            health.currentHealth -= spikeDamage;
            
            agentMover.acceleration = 0;
            agentMover.maxSpeed =0;
            StartCoroutine(canHitted());
         
            StartCoroutine(Reset());
        }

        if (health.currentHealth <= 0)
        {
            StopAllCoroutines();
            OnBegin?.Invoke();
            
            animator.SetBool("GettingHit", true);
            Vector2 direction = (transform.position - sender.transform.position).normalized;
            rb.AddForce(direction * push, ForceMode2D.Impulse);
            
            agentMover.acceleration = 0;
            health.StartCoroutine(health.Death());

            
            StartCoroutine(Reset());
        }
    }

    IEnumerator canHitted(){
       
       
        canBeHitted = true;
        yield return new WaitForSeconds(0.6f);
        canBeHitted = false;
        yield return new WaitForSeconds( 0.3f);
        canBeHitted = true;
        
    }
}
