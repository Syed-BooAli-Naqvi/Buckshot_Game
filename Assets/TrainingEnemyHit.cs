using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingEnemyHit : MonoBehaviour
{
    public Image healthFill;
    public Animator animator;
    public Collider collider;
    private void Start()
    {
        animator.SetTrigger("GunIdle");
    }
    public void GiveDamage()
    {
        animator.SetTrigger("Hit");
        healthFill.fillAmount -= 0.2f;
        if (healthFill.fillAmount <= 0.1f)
        {
            animator.SetTrigger("Die");
            collider.enabled = false;
            Invoke(nameof(Destroy), 4);
            TrainingManager.Instance.AddTime();
            TrainingManager.Instance.AddEnemyCount();
        }
    }
    void Destroy()
    {
        Destroy(this.gameObject);
    }

    public Transform target;
    public Vector3 worldUp;
    void Update() 
    { 
        if (target != null)
        {
            Vector3 tar = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(tar); 
        } 
    }
}
