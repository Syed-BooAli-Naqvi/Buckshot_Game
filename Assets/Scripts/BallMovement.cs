using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float duration,forwardSpeed;
    Transform t;
    private void Start()
    {
        t = transform;
        Move();
    }
    public void Move()
    {
        t.DOMove(new Vector3(t.position.x, t.position.y, t.position.z + forwardSpeed), duration).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(this.gameObject);
        });
    }
}
