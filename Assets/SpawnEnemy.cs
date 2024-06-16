using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnEnemy : MonoBehaviour
{

    [SerializeField] private Transform player;
    TargetDetector targetDetector;
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnFadeOut, OnFadeIn;
    
    bool doneFading = false;
    Animator weaponAnimator;

    [SerializeField]public EnemyArea enemyArea;
    [SerializeField]public EnemyArea2 enemyArea2;
     [SerializeField]public EnemyArea3 enemyArea3;

     [SerializeField]public EnemyArea4 enemyArea4;
      [SerializeField]public EnemyArea5 enemyArea5;

       [SerializeField]public EnemyArea6 enemyArea6;

        [SerializeField]public EnemyArea7 enemyArea7;
        [SerializeField]public EnemyArea8 enemyArea8;
         [SerializeField]public EnemyArea9 enemyArea9;
          [SerializeField]public EnemyArea10 enemyArea10;

           [SerializeField]public EnemyArea11 enemyArea11;

           [SerializeField]public EnemyArea12 enemyArea12;

           [SerializeField]public EnemyArea13 enemyArea13;

           [SerializeField]public EnemyArea14 enemyArea14;
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
       
        targetDetector = GameObject.FindGameObjectWithTag("Detector").GetComponent<TargetDetector>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Transform weaponParents = transform.Find("WeaponParent");

        
        

        weaponAnimator = weaponParents.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if (!isFading && enemyArea != null)
        {
            StartCoroutine(Fade());
        }

        if (!isFading && enemyArea2 != null)
        {
            StartCoroutine( Fade2());
        }

        if (!isFading && enemyArea3 != null)
        {
            StartCoroutine( Fade3());
        }

        if (!isFading && enemyArea4 != null)
        {
            StartCoroutine( Fade4());
        }

        if (!isFading && enemyArea5 != null)
        {
            StartCoroutine( Fade5());
        }

        if (!isFading && enemyArea6 != null)
        {
            StartCoroutine( Fade6());
        }

        if (!isFading && enemyArea7!= null)
        {
            StartCoroutine( Fade7());
        }

         if (!isFading && enemyArea8!= null)
        {
            StartCoroutine( Fade8());
        }

        if (!isFading && enemyArea9!= null)
        {
            StartCoroutine( Fade9());
        }

        if (!isFading && enemyArea10!= null)
        {
            StartCoroutine( Fade10());
        }

        if (!isFading && enemyArea11!= null)
        {
            StartCoroutine( Fade11());
        }

        if (!isFading && enemyArea12!= null)
        {
            StartCoroutine( Fade12());
        }

        if (!isFading && enemyArea13!= null)
        {
            StartCoroutine( Fade13());
        }

        if (!isFading && enemyArea14!= null)
        {
            StartCoroutine( Fade14());
        }

         
    }

    public IEnumerator Fade()
    {
        
        float distance = Vector2.Distance(player.position, transform.position);
        float wait = 0.3f;
       

        if (distance > enemyArea.Detector && !isFading && !isChangingPosition && doneFading == false && enemyArea != null )
        {
            
            isFading = true;
            doneFading = false;
            OnMovementInput?.Invoke(Vector2.zero);
            yield return new WaitForSeconds(wait);
            animator.SetBool("FadeOut", true);
            weaponAnimator.SetBool("Fading", true);
            yield return new WaitForSeconds(wait);
            OnFadeOut?.Invoke();

            // Change position for each enemy in the array
            enemyArea.ChangeEnemyPositionRandomly();
             
             yield return new WaitForSeconds(2);
            doneFading =true;
             
            isFading = false;
            isChangingPosition = true;
            doneFading =true;
        }

        if (distance < enemyArea.Detector && !isFading && isChangingPosition &&doneFading==true&& enemyArea != null )
        {
            wait = 0f;
            isFading = true;
             doneFading =true;
            animator.SetBool("FadeOut", false);
            weaponAnimator.SetBool("Fading", false);
            weaponAnimator.SetTrigger("Iddling");
            yield return new WaitForSeconds(wait);
            OnFadeIn?.Invoke();
            isFading = false;
            isChangingPosition = false;
             doneFading =false;
        }

        

    }


public IEnumerator Fade2(){

    float distance = Vector2.Distance(player.position, transform.position);
        float wait = 0.3f;
       
        if (distance > enemyArea2.Detector && !isFading && !isChangingPosition && doneFading == false &&enemyArea2 != null  )
        {
            isFading = true;
            doneFading =false;
            OnMovementInput?.Invoke(Vector2.zero);
            yield return new WaitForSeconds(wait);
            animator.SetBool("FadeOut", true);
            weaponAnimator.SetBool("Fading", true);
            
            yield return new WaitForSeconds(wait);
            OnFadeOut?.Invoke();
            enemyArea2.ChangeEnemyPositionRandomly();
            yield return new WaitForSeconds(2);
            doneFading =true;
            // Change position for each enemy in the array
            
             
            isFading = false;
            isChangingPosition = true;
        }

        if (distance < enemyArea2.Detector && !isFading && isChangingPosition && doneFading ==true && enemyArea2 != null )
        {
            wait = 0f;
            isFading = true;
            doneFading =true;
            animator.SetBool("FadeOut", false);
            weaponAnimator.SetBool("Fading", false);
            weaponAnimator.SetTrigger("Iddling");
            yield return new WaitForSeconds(wait);
            OnFadeIn?.Invoke();
            isFading = false;
            isChangingPosition = false;
            doneFading = false;
        }
    }

    public IEnumerator Fade3(){

    float distance = Vector2.Distance(player.position, transform.position);
        float wait = 0.3f;
       
        if (distance > enemyArea3.Detector && !isFading && !isChangingPosition && doneFading==false && enemyArea3 != null  )
        {
            isFading = true;
             doneFading = false;
            OnMovementInput?.Invoke(Vector2.zero);
            yield return new WaitForSeconds(wait);
            animator.SetBool("FadeOut", true);
            weaponAnimator.SetBool("Fading", true);
            yield return new WaitForSeconds(wait);
            OnFadeOut?.Invoke();
             enemyArea3.ChangeEnemyPositionRandomly();
             yield return new WaitForSeconds(2);
            doneFading = true;
            // Change position for each enemy in the array
           
             
            isFading = false;
            isChangingPosition = true;
        }

        if (distance < enemyArea3.Detector && !isFading && isChangingPosition && doneFading == true && enemyArea3 != null )
        {
            wait = 0f;
            isFading = true;
            doneFading =true;
            animator.SetBool("FadeOut", false);
            weaponAnimator.SetBool("Fading", false);
            weaponAnimator.SetTrigger("Iddling");
            yield return new WaitForSeconds(wait);
            OnFadeIn?.Invoke();
            isFading = false;
            isChangingPosition = false;
            doneFading =false;
        }
    }

    public IEnumerator Fade4(){

    float distance = Vector2.Distance(player.position, transform.position);
        float wait = 0.3f;
        if (distance > enemyArea4.Detector && !isFading && !isChangingPosition && doneFading ==false &&enemyArea4 != null  )
        {
            isFading = true;
            doneFading =false;
            OnMovementInput?.Invoke(Vector2.zero);
            yield return new WaitForSeconds(wait);
            animator.SetBool("FadeOut", true);
            weaponAnimator.SetBool("Fading", true);
            yield return new WaitForSeconds(wait);
            OnFadeOut?.Invoke();

            // Change position for each enemy in the array
            enemyArea4.ChangeEnemyPositionRandomly();
             yield return new WaitForSeconds(2);
            doneFading = true;
            isFading = false;
            isChangingPosition = true;
        }

        if (distance < enemyArea4.Detector && !isFading && isChangingPosition && doneFading ==true && enemyArea4 != null )
        {
            wait = 0f;
            isFading = true;
            doneFading = true;
            animator.SetBool("FadeOut", false);
            weaponAnimator.SetBool("Fading", false);
            weaponAnimator.SetTrigger("Iddling");
            yield return new WaitForSeconds(wait);
            OnFadeIn?.Invoke();
            isFading = false;
            isChangingPosition = false;
            doneFading =false;
        }
    }




     public IEnumerator Fade5(){

    float distance = Vector2.Distance(player.position, transform.position);
        float wait = 0.3f;
        if (distance > enemyArea5.Detector && !isFading && !isChangingPosition && doneFading ==false &&enemyArea5 != null  )
        {
            isFading = true;
            doneFading =false;
            OnMovementInput?.Invoke(Vector2.zero);
            yield return new WaitForSeconds(wait);
            animator.SetBool("FadeOut", true);
            weaponAnimator.SetBool("Fading", true);
            yield return new WaitForSeconds(wait);
            OnFadeOut?.Invoke();

            // Change position for each enemy in the array
            enemyArea5.ChangeEnemyPositionRandomly();
             yield return new WaitForSeconds(2);
            doneFading = true;
            isFading = false;
            isChangingPosition = true;
        }

        if (distance < enemyArea5.Detector && !isFading && isChangingPosition && doneFading ==true && enemyArea5 != null )
        {
            wait = 0f;
            isFading = true;
            doneFading = true;
            animator.SetBool("FadeOut", false);
            weaponAnimator.SetBool("Fading", false);
            weaponAnimator.SetTrigger("Iddling");
            yield return new WaitForSeconds(wait);
            OnFadeIn?.Invoke();
            isFading = false;
            isChangingPosition = false;
            doneFading =false;
        }
    }

    public IEnumerator Fade6(){

    float distance = Vector2.Distance(player.position, transform.position);
        float wait = 0.3f;
        if (distance > enemyArea6.Detector && !isFading && !isChangingPosition && doneFading ==false &&enemyArea6 != null  )
        {
            isFading = true;
            doneFading =false;
            OnMovementInput?.Invoke(Vector2.zero);
            yield return new WaitForSeconds(wait);
            animator.SetBool("FadeOut", true);
            weaponAnimator.SetBool("Fading", true);
            yield return new WaitForSeconds(wait);
            OnFadeOut?.Invoke();

            // Change position for each enemy in the array
            enemyArea6.ChangeEnemyPositionRandomly();
             yield return new WaitForSeconds(2);
            doneFading = true;
            isFading = false;
            isChangingPosition = true;
        }

        if (distance < enemyArea6.Detector && !isFading && isChangingPosition && doneFading ==true && enemyArea6 != null )
        {
            wait = 0f;
            isFading = true;
            doneFading = true;
            animator.SetBool("FadeOut", false);
            weaponAnimator.SetBool("Fading", false);
            weaponAnimator.SetTrigger("Iddling");
            yield return new WaitForSeconds(wait);
            OnFadeIn?.Invoke();
            isFading = false;
            isChangingPosition = false;
            doneFading =false;
        }

        
    }

    public IEnumerator Fade7(){

    float distance = Vector2.Distance(player.position, transform.position);
        float wait = 0.3f;
        if (distance > enemyArea7.Detector && !isFading && !isChangingPosition && doneFading ==false &&enemyArea7 != null  )
        {
            isFading = true;
            doneFading =false;
            OnMovementInput?.Invoke(Vector2.zero);
            yield return new WaitForSeconds(wait);
            animator.SetBool("FadeOut", true);
            weaponAnimator.SetBool("Fading", true);
            yield return new WaitForSeconds(wait);
            OnFadeOut?.Invoke();

            // Change position for each enemy in the array
            enemyArea7.ChangeEnemyPositionRandomly();
             yield return new WaitForSeconds(2);
            doneFading = true;
            isFading = false;
            isChangingPosition = true;
        }

        if (distance < enemyArea7.Detector && !isFading && isChangingPosition && doneFading ==true && enemyArea7!= null )
        {
            wait = 0f;
            isFading = true;
            doneFading = true;
            animator.SetBool("FadeOut", false);
            weaponAnimator.SetBool("Fading", false);
            weaponAnimator.SetTrigger("Iddling");
            yield return new WaitForSeconds(wait);
            OnFadeIn?.Invoke();
            isFading = false;
            isChangingPosition = false;
            doneFading =false;
        }

        
    }


    public IEnumerator Fade8(){

    float distance = Vector2.Distance(player.position, transform.position);
        float wait = 0.3f;
        if (distance > enemyArea8.Detector && !isFading && !isChangingPosition && doneFading ==false &&enemyArea8 != null  )
        {
            isFading = true;
            doneFading =false;
            OnMovementInput?.Invoke(Vector2.zero);
            yield return new WaitForSeconds(wait);
            animator.SetBool("FadeOut", true);
            weaponAnimator.SetBool("Fading", true);
            yield return new WaitForSeconds(wait);
            OnFadeOut?.Invoke();

            // Change position for each enemy in the array
            enemyArea8.ChangeEnemyPositionRandomly();
             yield return new WaitForSeconds(2);
            doneFading = true;
            isFading = false;
            isChangingPosition = true;
        }

        if (distance < enemyArea8.Detector && !isFading && isChangingPosition && doneFading ==true && enemyArea8!= null )
        {
            wait = 0f;
            isFading = true;
            doneFading = true;
            animator.SetBool("FadeOut", false);
            weaponAnimator.SetBool("Fading", false);
            weaponAnimator.SetTrigger("Iddling");
            yield return new WaitForSeconds(wait);
            OnFadeIn?.Invoke();
            isFading = false;
            isChangingPosition = false;
            doneFading =false;
        }

        
    }


    public IEnumerator Fade9(){

    float distance = Vector2.Distance(player.position, transform.position);
        float wait = 0.3f;
        if (distance > enemyArea9.Detector && !isFading && !isChangingPosition && doneFading ==false &&enemyArea9 != null  )
        {
            isFading = true;
            doneFading =false;
            OnMovementInput?.Invoke(Vector2.zero);
            yield return new WaitForSeconds(wait);
            animator.SetBool("FadeOut", true);
            weaponAnimator.SetBool("Fading", true);
            yield return new WaitForSeconds(wait);
            OnFadeOut?.Invoke();

            // Change position for each enemy in the array
            enemyArea9.ChangeEnemyPositionRandomly();
             yield return new WaitForSeconds(2);
            doneFading = true;
            isFading = false;
            isChangingPosition = true;
        }

        if (distance < enemyArea9.Detector && !isFading && isChangingPosition && doneFading ==true && enemyArea9!= null )
        {
            wait = 0f;
            isFading = true;
            doneFading = true;
            animator.SetBool("FadeOut", false);
            weaponAnimator.SetBool("Fading", false);
            weaponAnimator.SetTrigger("Iddling");
            yield return new WaitForSeconds(wait);
            OnFadeIn?.Invoke();
            isFading = false;
            isChangingPosition = false;
            doneFading =false;
        }

        
    }

    public IEnumerator Fade10(){

    float distance = Vector2.Distance(player.position, transform.position);
        float wait = 0.3f;
        if (distance > enemyArea10.Detector && !isFading && !isChangingPosition && doneFading ==false &&enemyArea10 != null  )
        {
            
        }

        if (distance < enemyArea10.Detector && !isFading && isChangingPosition && doneFading ==true && enemyArea10!= null )
        {
            
            yield return new WaitForSeconds(wait);
            
        }

        
    }

    public IEnumerator Fade11(){

    float distance = Vector2.Distance(player.position, transform.position);
        float wait = 0.3f;
        if (distance > enemyArea11.Detector && !isFading && !isChangingPosition && doneFading ==false &&enemyArea11 != null  )
        {
            isFading = true;
            doneFading =false;
            OnMovementInput?.Invoke(Vector2.zero);
            yield return new WaitForSeconds(wait);
            animator.SetBool("FadeOut", true);
            weaponAnimator.SetBool("Fading", true);
            yield return new WaitForSeconds(wait);
            OnFadeOut?.Invoke();

            // Change position for each enemy in the array
            enemyArea11.ChangeEnemyPositionRandomly();
             yield return new WaitForSeconds(2);
            doneFading = true;
            isFading = false;
            isChangingPosition = true;
        }

        if (distance < enemyArea11.Detector && !isFading && isChangingPosition && doneFading ==true && enemyArea11!= null )
        {
            wait = 0f;
            isFading = true;
            doneFading = true;
            animator.SetBool("FadeOut", false);
            weaponAnimator.SetBool("Fading", false);
            weaponAnimator.SetTrigger("Iddling");
            yield return new WaitForSeconds(wait);
            OnFadeIn?.Invoke();
            isFading = false;
            isChangingPosition = false;
            doneFading =false;
        }

        
    }

    public IEnumerator Fade12(){

    float distance = Vector2.Distance(player.position, transform.position);
        float wait = 0.3f;
        if (distance > enemyArea12.Detector && !isFading && !isChangingPosition && doneFading ==false &&enemyArea12 != null  )
        {
            isFading = true;
            doneFading =false;
            OnMovementInput?.Invoke(Vector2.zero);
            yield return new WaitForSeconds(wait);
            animator.SetBool("FadeOut", true);
            weaponAnimator.SetBool("Fading", true);
            yield return new WaitForSeconds(wait);
            OnFadeOut?.Invoke();

            // Change position for each enemy in the array
            enemyArea12.ChangeEnemyPositionRandomly();
             yield return new WaitForSeconds(2);
            doneFading = true;
            isFading = false;
            isChangingPosition = true;
        }

        if (distance < enemyArea12.Detector && !isFading && isChangingPosition && doneFading ==true && enemyArea12!= null )
        {
            wait = 0f;
            isFading = true;
            doneFading = true;
            animator.SetBool("FadeOut", false);
            weaponAnimator.SetBool("Fading", false);
            weaponAnimator.SetTrigger("Iddling");
            yield return new WaitForSeconds(wait);
            OnFadeIn?.Invoke();
            isFading = false;
            isChangingPosition = false;
            doneFading =false;
        }

        
    }

    public IEnumerator Fade13(){

    float distance = Vector2.Distance(player.position, transform.position);
        float wait = 0.3f;
        if (distance > enemyArea13.Detector && !isFading && !isChangingPosition && doneFading ==false &&enemyArea13 != null  )
        {
            
        }

        if (distance < enemyArea13.Detector && !isFading && isChangingPosition && doneFading ==true && enemyArea10!= null )
        {
            
            yield return new WaitForSeconds(wait);
           
        }

        
    }

     public IEnumerator Fade14(){

    float distance = Vector2.Distance(player.position, transform.position);
        float wait = 0.3f;
        if (distance > enemyArea14.Detector && !isFading && !isChangingPosition && doneFading ==false &&enemyArea14 != null  )
        {
            
        }

        if (distance < enemyArea14.Detector && !isFading && isChangingPosition && doneFading ==true && enemyArea14!= null )
        {
            
            yield return new WaitForSeconds(wait);
           
        }

        
    }

    
     
    
    
}

