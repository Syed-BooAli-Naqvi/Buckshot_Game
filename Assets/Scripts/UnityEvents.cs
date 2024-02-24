using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UnityEvents : MonoBehaviour
{
    public UnityEvent OnEnableEvent;
    public UnityEvent OnDisableEvent;

   

    private void OnEnable()
    {
        if (OnEnableEvent != null)
        {
            OnEnableEvent.Invoke();
        }
    }
    private void OnDisable()
    {
        if (OnDisableEvent != null)
        {
            OnDisableEvent.Invoke();
        }
    }
}
