using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class LookingAtRecipeBook : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] RectTransform RecipeBookRect;
    [SerializeField] float leftPosX, middlePosX;
    [SerializeField] float tweenDuration;
   
    public GameObject Canvas;
    bool canClosed = false;
    public GameObject recipeBook;

    // public GameObject Fever ,Cold,Cough,SoreThroat,Furuncle,Constipation,TineaVersicolor,Smallpox;
    
    void Awake()
    {
       
    }

    // Update is called once per frame
  public async void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && canClosed == false)
        {
            if (!recipeBook.activeInHierarchy )
            {
                RecipeBookIntro();
                recipeBook.SetActive(true);
                // Fever.SetActive(true);
                //  Cold.SetActive(false);
                //  Cough.SetActive(false);
                // SoreThroat.SetActive(false);
                // Furuncle.SetActive(false);
                // Constipation.SetActive(false);
                // TineaVersicolor.SetActive(false);
                // Smallpox.SetActive(false);
                canClosed = true;
   
            }
        }

        if(recipeBook.activeInHierarchy && canClosed == true )
        {
            if (Input.GetKeyUp(KeyCode.Tab) )
            {
                await RecipeBookOutro();
                recipeBook.SetActive(false);
                canClosed = false;
   
            }
        
        }
    }

    void RecipeBookIntro()
    {
        // canvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
        RecipeBookRect.DOAnchorPosX(middlePosX,tweenDuration).SetUpdate(true);
    }

    async Task RecipeBookOutro()
    {
        // canvasGroup.DOFade(0, tweenDuration).SetUpdate(true);
        await RecipeBookRect.DOAnchorPosX(leftPosX,tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }
}
