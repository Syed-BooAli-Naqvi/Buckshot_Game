using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OntriggerObject : MonoBehaviour
{

    public UnityEvent ontriggerenter, ontriggerexit;
    public UnityEvent onticketpurhase;
    public bool playerLookAt=true;
    public string tag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tag)
        { 
            if (ontriggerenter != null)
            {
                ontriggerenter.Invoke();
            }
            //isLookingAtTarget = playerLookAt;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ontriggerexit != null)
        {
            ontriggerexit.Invoke();
        }
    }
    bool isLookingAtTarget;
    public float rotationSpeed = 5f; // Adjust rotation speed as needed
    public float lookThreshold = 5f; // Adjust the threshold angle as needed
    public void Update()
    {
        if (isLookingAtTarget)
        {
            // Calculate the direction to the target
            Vector3 directionToTarget = transform.position - GameManager.Instance.player.transform.position;

            // Calculate the rotation only around the x-axis
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            Vector3 eulerRotation = lookRotation.eulerAngles;
            eulerRotation.y = 0; // Keep the y and z axis rotation to zero
            eulerRotation.z = 0;
            Quaternion finalRotation = Quaternion.Euler(eulerRotation);

            // Rotate the player towards the target
            GameManager.Instance.player.transform.rotation = Quaternion.Slerp(GameManager.Instance.player.transform.rotation, finalRotation, rotationSpeed * Time.deltaTime);

            // Check if the player is looking at the target within the threshold angle
            float angleDifference = Quaternion.Angle(GameManager.Instance.player.transform.rotation, finalRotation);
            if (angleDifference <= lookThreshold)
            {
                isLookingAtTarget = false;
                Debug.Log("Player is looking at the target.");
                // You can add additional actions here when the player looks at the target
            }
        }
    }
    public void btnclickinvoke()
    {
        ontriggerenter.Invoke();
    }

    public void Aftercompletingticket()
    {
        StartCoroutine(completepurchase());
    }
    IEnumerator completepurchase()
    {
        yield return new WaitForSeconds(5f);
        onticketpurhase?.Invoke();

    }

}
