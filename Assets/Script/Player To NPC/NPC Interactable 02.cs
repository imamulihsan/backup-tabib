using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

public class NPCInteractable02 : MonoBehaviour
{
    
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

      public GameObject QuestText;
    [SerializeField] Transform Player;
    [SerializeField] Transform thisNPC;
    AgentMover agentMover;
    public string[] dialogue;
    private int index = 0;

    public float wordSpeed;
    public bool playerIsClose;

    Color defaultColor;
   SpriteRenderer spriteRenderer;

   Animator animator;
   public float delay = 0.5f;

   public bool questFinished;

   PlayerExperience playerExperience;
   private bool xpRewardGiven = false;


    void Start()
    {
        dialogueText.text = "";
        agentMover= GetComponent<AgentMover>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        animator = GetComponent<Animator>();
         playerExperience = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (!dialoguePanel.activeInHierarchy && questFinished == false)
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
           
                

                
            }
            else if (dialogueText.text == dialogue[index])
            {
                NextLine();
             
            }

            if (!QuestText.activeInHierarchy && questFinished == true)
            {
               
            
            QuestText.SetActive(true);
                

                
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy&& questFinished == false)
        {
            StartCoroutine(JustWait());
        }

         if (Input.GetKeyDown(KeyCode.Q) && QuestText.activeInHierarchy&& questFinished == true)
        {
             StartCoroutine(QuestDone());
           
           
        }
XPReward();

        

        
    }

    public void RemoveText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        
        }
    }

    public IEnumerator JustWait()
    {
            yield return new WaitForSeconds(delay);
            RemoveText();
    }

 public IEnumerator QuestDone()
    {
            yield return new WaitForSeconds(delay);
            QuestText.SetActive(false);
            
    }
    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
            
        }
        else
        {
            RemoveText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            spriteRenderer.color = new Color(1f, 1f,1f);
            animator.speed = 0.5f;
        }

          if (other.CompareTag("Potion"))
        {
            Destroy(other.gameObject);
            dialoguePanel.SetActive(false);
            questFinished = true;
          
           
           
           
        }

        
       
        
    }

    private void OnTriggerStay2D(Collider2D other){
        if (other.CompareTag("Player")){
                if(Player.position.x  > thisNPC.position.x ){
                spriteRenderer.flipX = false;
             }
                if(Player.position.x  < thisNPC.position.x ){
                spriteRenderer.flipX = enabled;
             }
        }

        if (other.CompareTag("Potion"))
        {
            Destroy(other.gameObject);
        }


        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            RemoveText();
            spriteRenderer.color = defaultColor;
            animator.speed = 1f;
        }
    }

    private void XPReward(){
      if (questFinished && !xpRewardGiven) {
        playerExperience.currentXP += 200;
        xpRewardGiven = true; // Set the flag to true to indicate that the reward has been given
    }
    }



   
    
}