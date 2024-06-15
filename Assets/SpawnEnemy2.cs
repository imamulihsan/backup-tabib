using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnEnemy2 : MonoBehaviour
{

    [SerializeField] private Transform player;
    TargetDetector targetDetector;
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnFadeOut, OnFadeIn;
    

    Animator weaponAnimator;

    EnemyArea enemyArea;
    [SerializeField] public GameObject thisGameObject;

    Rigidbody2D rb;
   

    bool enemyFound = true;
    Transform Enemy;
    public bool isFading = false;
    Animator animator;

    [SerializeField] public float randomRangeX = 5f;
    [SerializeField] public float randomRangeY = 5f;

    // Start is called before the first frame update
    bool isChangingPosition = false;
    void Start()
    {
        enemyArea = GameObject.FindGameObjectWithTag("EnemyThreshold2").GetComponent<EnemyArea>();
        targetDetector = GameObject.FindGameObjectWithTag("Detector").GetComponent<TargetDetector>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Transform weaponParents = transform.Find("WeaponParent");

        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy2");
        

        weaponAnimator = weaponParents.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Fade()
    {
        // Other code...
        float distance = Vector2.Distance(player.position, transform.position);
        float wait = 0.3f;

        if (distance > enemyArea.Detector && !isFading && !isChangingPosition)
        {
            isFading = true;
            OnMovementInput?.Invoke(Vector2.zero);
            yield return new WaitForSeconds(wait);
            animator.SetBool("FadeOut", true);
            weaponAnimator.SetBool("Fading", true);
            yield return new WaitForSeconds(wait);
            OnFadeOut?.Invoke();

            // Change position for each enemy in the array
           enemyArea.ChangeEnemyPositionRandomly();

            isFading = false;
            isChangingPosition = true;
        }

        if (distance < enemyArea.Detector && !isFading && isChangingPosition)
        {
            wait = 0f;
            isFading = true;
            animator.SetBool("FadeOut", false);
            weaponAnimator.SetBool("Fading", false);
            weaponAnimator.SetTrigger("Iddling");
            yield return new WaitForSeconds(wait);
            OnFadeIn?.Invoke();
            isFading = false;
            isChangingPosition = false;
        }
    }

}
