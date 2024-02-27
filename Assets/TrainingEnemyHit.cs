using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingEnemyHit : MonoBehaviour
{
    public Image healthFill;
    public Animator animator;
    public Collider collider;
    public FieldOfView fov;
    private void Start()
    {
        fov = GetComponent<FieldOfView>();
        animator.SetTrigger("GunIdle");
    }
    public void GiveDamage()
    {
        animator.SetTrigger("Hit");
        if (fov.canSeePlayer)
        {
            animator.SetTrigger("Fire");
            isHiting = true;
        }
        healthFill.fillAmount -= 0.5f;
        if (healthFill.fillAmount <= 0.1f)
        {
            animator.SetTrigger("Die");
            collider.enabled = false;
            Invoke(nameof(Destroy), 4);
            TrainingManager.Instance.AddTime();
            TrainingManager.Instance.AddEnemyCount();
            fov.enabled = false;
            fov.canSeePlayer = false;
        }
    }
    void Destroy()
    {
        Destroy(this.gameObject);
    }

    public Transform target;
    public Vector3 OffSet;
    public bool isHiting;
    void Update() 
    { 
        if (target != null)
        {
            Vector3 tar = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(tar); 
        }
        if (!isHiting)
        {
            if (fov.canSeePlayer)
            {
                animator.SetTrigger("Fire");
                isHiting = true;
                StartCoroutine(StartFiring());
            }
        }
        else
        {
            if (!fov.canSeePlayer)
            {
                animator.SetTrigger("DontFire");
                isHiting = false;
            }
        }   
    }

    public IEnumerator StartFiring()
    {
        while (isHiting)
        {
            MoveObject moveObject = Instantiate(TrainingManager.Instance.bullet);
            moveObject.transform.position = transform.position + OffSet;
            moveObject.targetTransform = fov.playerRef.transform;
            moveObject.Move();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
