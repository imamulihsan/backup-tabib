using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyArea3 : MonoBehaviour
{
  bool canChase = false;

 TargetDetector targetDetector;
 Agent agent;

 Animator animator;
 EnemyAI enemyAI;
   public UnityEvent OnPlayerComingWithReference,OnPlayerLeavingWithReference;

 SpriteRenderer spriteRenderer;

 CircleCollider2D circleCollider2D;
 SpawnEnemy spawnEnemy;
  [SerializeField]public float randomRangeX = 5f;
    [SerializeField]public float randomRangeY = 5f;

    public Transform[] Enemy;
    
  public Transform enemyThreshold;
    float wait = 0.5f;

    public float Detector = 10f;
 
     void Start()
    {   
         // Finding all EnemyAI components and storing them in the thisEnemy array
       
        targetDetector = GameObject.FindGameObjectWithTag("Detector").GetComponent<TargetDetector>();
        agent = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Agent>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        enemyThreshold = GetComponent<Transform>();

        // Find all enemies and store their transforms in the array
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy3");
        
        Enemy = new Transform[enemyObjects.Length];
        for (int i = 0; i < enemyObjects.Length; i++)
        {
            Enemy[i] = enemyObjects[i].transform;

            if (i == 0) 
            {
               enemyAI = enemyObjects[i].GetComponent<EnemyAI>();
                animator = enemyObjects[i].GetComponent<Animator>();
                spriteRenderer = enemyObjects[i].GetComponentInChildren<SpriteRenderer>();
                spawnEnemy = enemyObjects[i].GetComponent<SpawnEnemy>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

     public void OnTriggerEnter2D (Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
                targetDetector.targetDetectionRange =10f;
                foreach (var enemy in Enemy)
            {
                
                    enemyAI.attackDistanceThreshold = 1.5f;
                    enemyAI.chaseDistanceThreshold = 1.5f;
                
            }
                canChase = true;
                Detector = 12f;
                OnPlayerComingWithReference?.Invoke();
        }

       
    }

    public void OnTriggerStay2D (Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
                targetDetector.targetDetectionRange =10f;
               foreach (var enemy in Enemy)
            {
                
                    enemyAI.attackDistanceThreshold = 1.5f;
                    enemyAI.chaseDistanceThreshold = 1.5f;
                
            }
                 canChase = true;
                 Detector = 12f;
        }
        
    }


      public void OnTriggerExit2D (Collider2D other)
    {
        if(other.CompareTag("Player"))
        {

           
   
               targetDetector.targetDetectionRange =0f;
               Detector = 0f;
                foreach (var enemy in Enemy)
            {
                
                    enemyAI.attackDistanceThreshold = 1.5f;
                    enemyAI.chaseDistanceThreshold = 0f;
                
            }
                 
                 
                 OnPlayerLeavingWithReference?.Invoke();

               
             
        } 
    }
 public void ChangeEnemyPositionRandomly()
    {
         foreach (Transform enemy in Enemy)
            {
                Vector2 randomPosition = new Vector2(
                    Random.Range(enemyThreshold.transform.position.x - randomRangeX +4 , enemyThreshold.transform.position.x + randomRangeX),
                    Random.Range(enemyThreshold.transform.position.y - randomRangeY +4, enemyThreshold.transform.position.y + randomRangeY )
                );

                enemy.transform.position = randomPosition;
            }
    }
    
}

   


