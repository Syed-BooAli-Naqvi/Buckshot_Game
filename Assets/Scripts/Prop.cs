using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public bool isMine;
    public bool canPick;
    public bool test;
    public int id;
    public List<GameObject> props = new List<GameObject>();
    public Transform toPos;

    private void OnValidate()
    {
        if (test)
        {
            test = false;
            toPos = transform;
            props.Clear();
            for (int i = 0; i < transform.childCount; i++)
            {
                props.Add(transform.GetChild(i).gameObject);
            }
        }
    }
}
