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
            Debug.Log("Ray Hit = " + hit.collider.name);
            // Check if the raycast hits a collider
            if (hitting)
            {
                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
                Debug.Log("Collider touched!");
            }
        }
    }
}
