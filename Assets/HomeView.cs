using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HomeView : MonoBehaviour
{
    SpriteRenderer player;
    public GameObject thisHouse;
    SpriteRenderer sp;

    void Start()
    {
         player= GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
         sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > thisHouse.transform.position.y )
        {
            sp.sortingOrder = 10; 
        }
    }
}
