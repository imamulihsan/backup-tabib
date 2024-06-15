using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class NPCInteractable : MonoBehaviour//, IDataPersistence
{
    // [SerializeField] private string id;

    // [ContextMenu("Generate guid for id")]
    // private void GenerateGuid()
    // {
    //     id = System.Guid.NewGuid().ToString();
    // }
    
    public GameObject dialoguePanel;
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
    public int XpReward;

    Color defaultColor;
    SpriteRenderer spriteRenderer;

    Animator animator;
    public float delay = 0.25f;

    public bool questFinished;
    private bool xpRewardGiven = false;
    private bool itemQuestTaken = false;
    public bool canPress = false; 
    public bool isTyping = false;

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
            if (!dialoguePanel.activeInHierarchy && questFinished == false)
            {
                 StopAllCoroutines();
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
           
                

                
            }
            else if (dialogueText.text == dialogue[index])
            {
                 
                NextLine();

            }

            if (!QuestText.activeInHierarchy && questFinished == true)
            {
                StopAllCoroutines();
            QuestText.SetActive(true);

            }
        }
        if (!isTyping)
        {
            if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy&& questFinished == false )
            {
                StopAllCoroutines();
                StartCoroutine(JustWait());
         
            }

            if (Input.GetKeyDown(KeyCode.Q) && QuestText.activeInHierarchy&& questFinished == true)
            {
                StopAllCoroutines();
                StartCoroutine(QuestDone());
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
            Destroy(other.gameObject);
            dialoguePanel.SetActive(false);
            QuestText.SetActive(true);
            questFinished = true;
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