using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrder : MonoBehaviour
{

    Transform player;
    Transform position;
    SpriteRenderer spriteRenderer;
    bool onTop = false;
    // Start is called before the first frame update
    void Start()
    {
        player =GameObject.FindGameObjectWithTag("Player").transform;
        position = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(player.transform.position.y > transform.position.y )
        {
           onTop = true;
        }

        if(onTop == true)
        {
            spriteRenderer.sortingOrder = 4;
        }



        if(player.transform.position.y < transform.position.y )
        {
           onTop= false;
        }

         if(onTop == false)
        {
            spriteRenderer.sortingOrder = 2;
        }
    }
}
