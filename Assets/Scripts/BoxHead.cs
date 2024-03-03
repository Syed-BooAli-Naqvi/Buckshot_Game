using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHead : MonoBehaviour
{
    public Animator anim;
    public Transform[] props;
    public List<Vector3> propStartPos;
    public Transform topPosition;

    private void OnEnable()
    {
    }
    public IEnumerator GetPropToTopPosition(int random)
    {
        propStartPos.Clear();
        for (int i = 0; i < props.Length; i++)
        {
            propStartPos.Add(props[i].position);
        }
        props[random].position = propStartPos[random];
        props[random].gameObject.SetActive(true);
        //props[random]
        bool isAtTop = false;
        DOTween.To(() => props[random].position, x => props[random].position = x, topPosition.position, 1.5f).OnComplete(()=> 
        {
            isAtTop = true;
        });
        yield return new WaitUntil(() => isAtTop);
    }
}