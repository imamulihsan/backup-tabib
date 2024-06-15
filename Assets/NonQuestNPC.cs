using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class NonQuestNPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;

    [SerializeField] Transform Player;
    [SerializeField] Transform thisNPC;

    private PlayerInteraction playerInteraction;
    private AgentMover agentMover;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Animator playerAnimator;
    private PlayerExperience playerExperience;

    private int index = 0;
    private Color defaultColor;
    private bool playerIsClose;
    private bool canPress = false;
    private bool isTyping = false;

    public float wordSpeed;
    public float delay = 0.25f;

    public UnityEvent OnBegin, OnDone;

    void Start()
    {
        dialogueText.text = "";
        agentMover = GameObject.FindGameObjectWithTag("Player").GetComponent<AgentMover>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        animator = GetComponent<Animator>();
        playerExperience = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void Update()
    {
        Dialog();
    }

    public void Dialog()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
                OnDialog();
            }
            else if (dialogueText.text == dialogue[index])
            {
                NextLine();
            }
        }

        // Ensure Q key only works if not typing
        if (Input.GetKeyDown(KeyCode.Q) && playerIsClose )
        {
            StopAllCoroutines();
            dialoguePanel.SetActive(false);
            DoneDialog();
            
        }
    }

    public void RemoveText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        isTyping = true; // Set isTyping to true when starting to type
        dialogueText.text = ""; // Clear the text before starting to type
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
            canPress = true;
        }
        isTyping = false; // Set isTyping to false after typing is complete
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            StartCoroutine(Typing());
        }
        else
        {
            RemoveText();
            DoneDialog();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            spriteRenderer.color = new Color(1f, 1f, 1f);
            animator.speed = 0.5f;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Player.position.x > thisNPC.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else if (Player.position.x < thisNPC.position.x)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            spriteRenderer.color = defaultColor;
            animator.speed = 1f;
        }
    }

    public void OnDialog()
    {
        playerAnimator.SetFloat("UpDown", 0f);
        playerAnimator.SetFloat("RightLeft", 0f);
        agentMover.acceleration = 0;
        OnBegin?.Invoke();
    }

    public void DoneDialog()
    {
        agentMover.acceleration = 50;
        OnDone?.Invoke();
    }

    public IEnumerator JustWait()
    {
        yield return new WaitForSeconds(delay);
        DoneDialog();
      
    }
}
