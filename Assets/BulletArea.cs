using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletArea : MonoBehaviour
{
    public List<Bullets> bullets = new List<Bullets>();

    [System.Serializable]
    public struct Bullets
    {
        public GameObject emptyBullet, fullBullet;
    }
}
