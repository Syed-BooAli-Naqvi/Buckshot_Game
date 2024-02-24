using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnableDisableEvents : MonoBehaviour
{
    public UnityEvent enableEvent, disableEvent;
    void OnEnable() 
    {
        if (enableEvent != null)
        {
            enableEvent.Invoke();
        }
    }
    void OnDisable()
    {
        if (disableEvent != null)
        {
            disableEvent.Invoke();
        }
    }
}
