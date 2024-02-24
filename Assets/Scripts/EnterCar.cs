using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterCar : MonoBehaviour
{
    public UnityEvent ontriggerenter, ontriggerexit;
    public bool exit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || (other.GetComponent<RCC_CarControllerV3>() != null && other.GetComponent<RCC_CarControllerV3>().enabled))
        {
            GameManager.Instance.SetController(!exit);
            gameObject.SetActive(false);
            if (ontriggerenter != null)
            {
                ontriggerenter.Invoke();
            }
        }
    }
}
