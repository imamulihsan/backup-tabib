using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using System.Threading.Tasks;

public class PlayerInteraction : MonoBehaviour
{
    // [Header("Animation")]
    // [SerializeField] RectTransform DialogPanelRect;
    // [SerializeField] float bottomPosY, middlePosY;
    // [SerializeField] float tweenDuration;
    // [SerializeField] CanvasGroup canvasGroup; //Dark panel canvas group
    NPCInteractable nPCInteractable;

    private Vector2 oldMovementInput;
    AgentMover agentMover;
    WeaponParent weaponParent;

    [SerializeField] private float currentSpeed=0;
    Animator animator;

    bool canPress;

    float delay =0.10f;
    public UnityEvent OnDialog,OnDoneDialog;
    Rigidbody2D rb;
    public void Awake()
    {
        nPCInteractable = GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCInteractable>();
        weaponParent = GetComponent<WeaponParent>();
        agentMover = GetComponent<AgentMover>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
   
    public void Update ()
    {
        float interactRange = 0.25f;
        Collider2D collider = Physics2D.OverlapCircle(transform.position,interactRange);
        if (collider != null && Input.GetKeyDown(KeyCode.E))
        {
            if (collider.CompareTag("NPC"))
            {
                if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    npcInteractable.Update();
                    PlayDialog();
                    animator.SetFloat("UpDown", 0f);
                    animator.SetFloat("RightLeft", 0f);
                    rb.velocity = Vector2.zero;
                    agentMover.dashLocked = true;
                }
            }
        }

        if (collider != null && Input.GetKeyDown(KeyCode.Q) && collider.CompareTag("NPC"))
        {
            if (nPCInteractable != null && !nPCInteractable.isTyping)
            {
                StartCoroutine(JustWait());
                agentMover.dashLocked = false;
            }
        }
    }

    public void PlayDialog()
    {
        // DialogPanelIntro();
        OnDialog?.Invoke ();
    }
    public async void EndDialog()
    {
        // await DialogPanelOutro();
        OnDoneDialog.Invoke();
    }
   
    public IEnumerator JustWait()
    {
        yield return new WaitForSeconds(delay);
        EndDialog();   
    }

    // void DialogPanelIntro()
    // {
    //     canvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
    //     DialogPanelRect.DOAnchorPosY(middlePosY,tweenDuration).SetUpdate(true);
    // }

    // async Task DialogPanelOutro()
    // {
    //     canvasGroup.DOFade(0, tweenDuration).SetUpdate(true);
    //     await DialogPanelRect.DOAnchorPosY(bottomPosY,tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    // }
}
