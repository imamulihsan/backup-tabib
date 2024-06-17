using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class NPCInteractable03 : MonoBehaviour//, IDataPersistence
{
    // [SerializeField] private string id;

    // [ContextMenu("Generate guid for id")]
    // private void GenerateGuid()
    // {
    //     id = System.Guid.NewGuid().ToString();
    // }
    
    public GameObject dialoguePanel;

    public GameObject oneProgressPanel;

     public GameObject twoProgressPanel;
    public TextMeshProUGUI dialogueText;

    public GameObject QuestText;
    [SerializeField] Transform Player;
    [SerializeField] Transform thisNPC;
    PlayerInteraction playerInteraction;
    AgentMover agentMover;
    public string[] dialogue;
    private int index = 0;

    public float wordSpeed;
    public bool playerIsClose;
    public string itemTag;
    public string itemTag2;
    public string itemTag3;
    public int XpReward;

    Color defaultColor;
    SpriteRenderer spriteRenderer;

    Animator animator;
    public float delay = 0.1f;

    bool questInProgress =false;

    public bool questFinished;
    private bool xpRewardGiven = false;
    private bool itemQuestTaken = false;
    public bool canPress = false; 
    public bool isTyping = false;

    int itemNeeded = 0;

    PlayerExperience playerExperience;


    void Start()
    {
        dialogueText.text = "";
        agentMover= GetComponent<AgentMover>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        animator = GetComponent<Animator>();
        playerExperience = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
    }

    // public void LoadData(GameData data)
    // {
    //     data.questFinished.TryGetValue(id, out itemQuestTaken);
    //     if (itemQuestTaken)
    //     {
    //         thisNPC.gameObject.SetActive(false);
    //     }
    // }

    // public void SaveData(GameData data)
    // {
    //     if (data.questFinished.ContainsKey(id))
    //     {
    //         data.questFinished.Remove(id);
    //     }
    //     data.questFinished.Add(id, !itemQuestTaken);
    // }

    //   public void LoadData(GameData data)
    // {   
    //     this.questFinished = data.questFinished;   
    // }

    // public void SaveData(GameData data)
    // {
    //     if (gameObject.tag == itemTag && id == this.id && itemQuestTaken == true)
    //     {
    //         data.questFinished = this.questFinished;
    //     }
    // }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (!dialoguePanel.activeInHierarchy && questInProgress == false && questFinished == false)
            {
                 StopAllCoroutines();
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
           
                

                
            }
            else if (dialogueText.text == dialogue[index])
            {
                 
                NextLine();

            }



             if (!oneProgressPanel.activeInHierarchy && questInProgress == true && itemNeeded == 2)
            {
                 StopAllCoroutines();
                oneProgressPanel.SetActive(true);
 
            }

             if (!twoProgressPanel.activeInHierarchy && questInProgress == true && itemNeeded == 1)
            {
                 StopAllCoroutines();
                twoProgressPanel.SetActive(true);
 
            }
            
            

            if (!QuestText.activeInHierarchy && questFinished == true && itemNeeded ==0)
            {
                StopAllCoroutines();
            QuestText.SetActive(true);

            }
        }
        
        {
           

            if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy&& questFinished == false )
            {
                StopAllCoroutines();
                StartCoroutine(JustWait());
         
            }
             if (Input.GetKeyDown(KeyCode.Q) && oneProgressPanel.activeInHierarchy)
            {
                
                oneProgressPanel.SetActive(false);
         
            }

            if (Input.GetKeyDown(KeyCode.Q) && twoProgressPanel.activeInHierarchy)
            {
                twoProgressPanel.SetActive(false);
         
            }

            if (Input.GetKeyDown(KeyCode.Q) && QuestText.activeInHierarchy&& questFinished == true)
            {
                QuestText.SetActive(false);
            }
        }

        XPReward();

        

        
    }

    public void RemoveText()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        isTyping = true; 
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
            canPress = true;
        
        }
        isTyping = false;
    }

    public IEnumerator JustWait()
    {
        if(canPress == true) 
        yield return new WaitForSeconds(delay);
        RemoveText();
    }

    public IEnumerator QuestDone()
    {
          
        yield return new WaitForSeconds(delay);
        QuestText.SetActive(false);
            
    }

    public IEnumerator QuestInProgress()
    {
          
        yield return new WaitForSeconds(delay);
        oneProgressPanel.SetActive(false);
            
    }

    public void NextLine()
    {
        StopAllCoroutines();
        if (index < dialogue.Length - 1)
        {
           
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
            canPress = true;
            
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

        if ( other.gameObject.tag == itemTag &&!itemQuestTaken)
        {
            questInProgress = true;
            Destroy(other.gameObject);
            dialoguePanel.SetActive(false);
            oneProgressPanel.SetActive(true);
            itemNeeded =2;
          
           
        }

         if ( other.gameObject.tag == itemTag2 &&!itemQuestTaken && questInProgress == true && itemNeeded ==2 )
        {
            Destroy(other.gameObject);
            dialoguePanel.SetActive(false);
            oneProgressPanel.SetActive(false);
            twoProgressPanel.SetActive(true);
            QuestText.SetActive(false);
          itemNeeded =1;
           
            questInProgress =true;
        }
 
        if ( other.gameObject.tag == itemTag3 &&!itemQuestTaken && questInProgress == true && itemNeeded ==1 )
        {
            Destroy(other.gameObject);
             dialoguePanel.SetActive(false);
            oneProgressPanel.SetActive(false);
            twoProgressPanel.SetActive(false);
            QuestText.SetActive(true);

            itemNeeded =0;
            questFinished = true;
            itemQuestTaken = true;
            questInProgress =false;
             itemQuestTaken = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(Player.position.x  > thisNPC.position.x )
            {
                spriteRenderer.flipX = false;
            }
            if(Player.position.x  < thisNPC.position.x )
            {
                spriteRenderer.flipX = enabled;
            }
        }  
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            RemoveText();
            oneProgressPanel.SetActive(false);
            dialoguePanel.SetActive(false);
            QuestText.SetActive(false);
            spriteRenderer.color = defaultColor;
            animator.speed = 1f;
        }
    }

    private void XPReward()
    {
        if (questFinished && !xpRewardGiven)
        {
            playerExperience.currentXP += XpReward;
            xpRewardGiven = true; 
        }
    }



   
    
}