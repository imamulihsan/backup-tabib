using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour{
    public UnityEvent OnAnimationEventTrigerred,OnAttackPerformed;

    public void TriggerEvent(){
        OnAnimationEventTrigerred?.Invoke();
    }

    public void TriggerAttack(){
        OnAttackPerformed?.Invoke();
    }
}
