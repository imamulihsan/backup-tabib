using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    float HAxis,ZAxis;
    PlayerKnockback playerKnockback;
    private enum State
    {
        Roaming, 
        Attacking
    }
    
    private Vector2 roamPosition;

    private float timeRoaming = 0f;

    private State state;

    [SerializeField] private Transform thisGameObject;
    public UnityEvent<Vector2>OnMovementInput,OnPointerInput;
    public UnityEvent OnAttack;

   
    SpriteRenderer spriteRenderer;
    Animator animator;

    [SerializeField] private Transform player;

    BegalHealth begalHealth;

    public float chaseDistanceThreshold =0 , attackDistanceThreshold = 1.5f;

    [SerializeField] private float attackDelay = 1;
    private float passedTime =1;
    Color color;

    TargetDetector targetDetector;

    SpawnEnemy spawnEnemy;

    private bool isFading = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        state = State.Roaming;
        begalHealth=GetComponent<BegalHealth>();
        playerKnockback= GetComponent<PlayerKnockback>();
        targetDetector= GameObject.FindGameObjectWithTag("Detector").GetComponent<TargetDetector>(); 
       spawnEnemy =GetComponent<SpawnEnemy>();
          
    }

    private void Start()
    {
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        
        if(player == null)
        {
            OnMovementInput?.Invoke(Vector2.zero);
         
            return;
        } 
        float distance = Vector2.Distance(player.position,transform.position);
        Facing();

        if(distance < chaseDistanceThreshold)
        {
            OnPointerInput?.Invoke(player.position);
          
            
            if(distance<= attackDistanceThreshold )
            {
                OnMovementInput?.Invoke(Vector2.zero);
                spriteRenderer.enabled = true;

                if(passedTime >= attackDelay )
                {
                    passedTime = 0;
                    OnAttack?.Invoke();
                    AudioManager.Instance.PlaySFX("Attack Bandit");
                }
            }
            else
            {
                Vector2 direction = player.position - transform.position;
            
                animator.SetBool("Moving",true);
                OnMovementInput?.Invoke(direction.normalized);
            }
        }
        else
        {
            
        }
        
        if(passedTime<attackDelay)
        {
            passedTime += Time.deltaTime;
        }
       

      
    }
    

   


    public Vector2 GetRoamingPosition() 
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
   

    private void Facing ()
    {
        float distance = Vector2.Distance(player.position,transform.position);
    
        if( player.position.x < thisGameObject.position.x )
        {
            spriteRenderer.flipX = enabled;
        }
        if( player.position.x > thisGameObject.position.x )
        {
            spriteRenderer.flipX = false;
        }
    }

   

}

