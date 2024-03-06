using UnityEngine;

public class TouchDetector : MonoBehaviour
{
    public bool hitting;
    public Camera cam;
    void Update()
    {
        // Check for touch input on both mobile devices and in the editor
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            // Get the touch position based on the input type
            Vector2 touchPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Input.mousePosition;

            // Perform a raycast from the touch position
            Ray ray = cam.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            // Draw the ray for visualization
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);


            hitting = Physics.Raycast(ray, out hit);
            // Check if the raycast hits a collider
            if (hitting)
            {
                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
                if (hit.transform.GetComponent<Prop>() != null && hit.transform.GetComponent<Prop>().canPick && hit.transform.GetComponent<Prop>().isMine && !GameManager.isPlaying)
                {
                    var prop = hit.transform.GetComponent<Prop>();
                    prop.canPick = false;
                    prop.props[prop.id].SetActive(false);
                    Debug.Log("Collider touched!");
                    switch (prop.id)
                    {
                        case 0:
                            GameManager.Instance.Knife();
                            break;
                        case 1:
                            GameManager.Instance.HandCuffs();
                            break;
                        case 2:
                            GameManager.Instance.Can();
                            break;
                        case 3:
                            GameManager.Instance.CiggeretPack();
                            break;
                    }
                }
                else if (hit.transform.CompareTag("gun") && hit.transform.GetComponent<Gun>() != null && hit.transform.GetComponent<Gun>().canPick)
                {
                    hit.transform.GetComponent<Gun>().Pick();
                }
            }
        }
    }
}
