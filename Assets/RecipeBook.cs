using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBook : MonoBehaviour
{
    public GameObject recipeBook;
    public LookingAtRecipeBook player;
    // public JenisPenyakit btnNext;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<LookingAtRecipeBook>();
      
    }

    // Update is called once per frame
   public void Update()
    {
       player.Update(); 
    }
}
