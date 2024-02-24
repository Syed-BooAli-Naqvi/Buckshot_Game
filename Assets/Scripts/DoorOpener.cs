using UnityEngine;
using UnityEngine.UI;

public class DoorOpener : MonoBehaviour 
{
    public Button HandBTN;
    public Vector3 rotationpos;
    public GameObject Door;
    public float speed;
    public AudioSource DoorSound;

    public void opendoor()
    {
        //HandBTN.gameObject.SetActive(false);
        DoorSound.Play();
        Debug.LogError("Opening Door");
        GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
              //HandBTN.onClick.RemoveAllListeners();
              //HandBTN.gameObject.SetActive(true);
              //HandBTN.onClick.AddListener(opendoor);
            opendoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //HandBTN.gameObject.SetActive(false);
            //HandBTN.onClick.RemoveAllListeners();
        }
    }
}