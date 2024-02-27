using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Transform targetTransform; // Target transform to move towards
    public float moveSpeed = 5f; // Speed of movement

    void Update()
    {
        if (targetTransform != null)
        {
            // Calculate the direction towards the target
            Vector3 direction = (targetTransform.position - transform.localPosition).normalized;

            // Calculate the distance to the target
            float distance = Vector3.Distance(transform.localPosition, targetTransform.position);

            // Move towards the target
            transform.position += direction * Mathf.Min(moveSpeed * Time.deltaTime, distance);
        }
    }
    public bool canMove;

    public void Move()
    {
        canMove = true;
        Invoke(nameof(Destroy), 6);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
