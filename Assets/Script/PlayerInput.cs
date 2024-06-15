using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector2>OnMovementInput,OnPointerInput;
    public UnityEvent OnAttack,OnDash,OnInteract,OnRecipeBook;

    [SerializeField] private InputActionReference movement, attack , pointerPosition,dash,interact,recipeBook;

    private void Update()
    {
        OnMovementInput?.Invoke(movement.action.ReadValue<Vector2>().normalized);
        OnPointerInput?.Invoke(GetPointerInput());
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos= pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnEnable()
    {
        attack.action.performed += PerformAttack;
        dash.action.performed += PerformDash;
        interact.action.performed += PerformInteract;
        recipeBook.action.performed += PerformRecipeBook;
    }

    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
        dash.action.performed -= PerformDash;
        interact.action.performed -= PerformInteract;
        recipeBook.action.performed -= PerformRecipeBook;
    }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        OnAttack?.Invoke();
        AudioManager.Instance.PlaySFX("Attack Tabib");
    }

    private void PerformDash(InputAction.CallbackContext obj)
    {
        OnDash?.Invoke();
        AudioManager.Instance.PlaySFX("Dash");
    }

    public void PerformInteract(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke();
    }

    public void PerformRecipeBook(InputAction.CallbackContext obj)
    {
        OnRecipeBook?.Invoke();
    }

}

