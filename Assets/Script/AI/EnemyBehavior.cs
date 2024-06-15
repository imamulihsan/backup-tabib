using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private List<SteeringBehaviour> steeringBehaviours;

    [SerializeField]
    private List<Detector> detectors;

    [SerializeField]
    private AIData aiData;

    BegalHealth begalHealth;

    [SerializeField]
    private float detectionDelay = 0.05f, aiUpdateDelay = 0.06f, attackDelay = 1f;

    [SerializeField]
    private float attackDistance = 0.5f;

    //Inputs sent from the Enemy AI to the Enemy controller
    public UnityEvent OnAttackPressed;
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;

    [SerializeField]
    private Vector2 movementInput;

    Rigidbody2D rb;

    AgentMover agentMover;

    

    [SerializeField]
    private ContextSolver movementDirectionSolver;

    bool following = false;

    bool canChase = false;

    Animator animator;
    
    EnemyArea enemyArea;

    private void Start()
    {
        //Detecting Player and Obstacles around
        InvokeRepeating("PerformDetection", 0, detectionDelay);
       begalHealth = GetComponent<BegalHealth>();
        rb=GetComponent<Rigidbody2D>();
        agentMover = GetComponent<AgentMover>();
       enemyArea= GameObject.FindGameObjectWithTag("EnemyThreshold").GetComponent<EnemyArea>();
       animator = GetComponent<Animator>();
    }

    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }
    }

    private void Update()
    {
        //Enemy AI movement based on Target availability
        if (aiData.currentTarget != null)
        {
            //Looking at the Target
            OnPointerInput?.Invoke(aiData.currentTarget.position);
            if (following == false)
            {
                following = true;
                StartCoroutine(ChaseAndAttack());
            }
        }
        else if (aiData.GetTargetsCount() > 0)
        {
            //Target acquisition logic
            aiData.currentTarget = aiData.targets[0];
        }
        //Moving the Agent
        OnMovementInput?.Invoke(movementInput);
    }

    private IEnumerator ChaseAndAttack()
    {
        
        if (aiData.currentTarget == null)
        {
            
            movementInput = Vector2.zero;
            following = false;
              animator.SetBool("Moving",false);
            yield break;
        }
        else
        {
            float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);

            if (distance < attackDistance && canChase)
            {
                //Attack logic
                movementInput = Vector2.zero;
                OnAttackPressed?.Invoke();
                yield return new WaitForSeconds(attackDelay);
                StartCoroutine(ChaseAndAttack());
            }
            else
            {
                //Chase logic
                movementInput = movementDirectionSolver.GetDirectionToMove(steeringBehaviours, aiData);
                animator.SetBool("Moving",true);
                yield return new WaitForSeconds(aiUpdateDelay);
                StartCoroutine(ChaseAndAttack());
            }

        }
        

    }

   
}